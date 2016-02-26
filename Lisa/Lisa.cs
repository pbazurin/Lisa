using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Lisa
{
    public static class Lisa
    {
        private static SpeechRecognitionEngine _recognizer;
        private static SpeechSynthesizer _synthesizer;

        private static bool _isListening;

        private static string _lastSpeech;
        private static DateTime _lastSpeechTimestamp;

        public const float AcceptableConfidence = 0.6F;

        static Lisa()
        {
            var defaultCulture = new CultureInfo("ru-RU");

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = defaultCulture;

            RefreshVoice();

            _lastSpeech = string.Empty;
            _lastSpeechTimestamp = DateTime.Now;
        }

        public static void SetCulture(CultureInfo newCulture)
        {
            var wasListening = _isListening;

            if (_isListening)
            {
                StopListening();
            }

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = newCulture;

            RefreshVoice();

            if (wasListening && !_isListening)
            {
                StartListening();
            }
        }

        public static void StartListening()
        {
            if (_isListening)
            {
                return;
            }

            _isListening = true;

            _recognizer = new SpeechRecognitionEngine(Thread.CurrentThread.CurrentCulture);
            _recognizer.SetInputToDefaultAudioDevice();

            var commands = Repository.GetAllCommands();
            commands.ForEach(c => c.Init(_recognizer));

            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        public static void StopListening()
        {
            _isListening = false;

            _recognizer.RecognizeAsyncStop();
        }

        public static void Speak(string textToSpeech)
        {
            _lastSpeech = textToSpeech;
            _synthesizer.SpeakAsync(textToSpeech);
        }

        public static void StopSpeaking()
        {
            _synthesizer.SpeakAsyncCancelAll();
        }

        public static bool IsSaying(string speech)
        {
            var words = speech.Split(' ');

            var currentCompare = Thread.CurrentThread.CurrentCulture.CompareInfo;
            var isSameWordsAsSpoken = words.All(w => currentCompare.IndexOf(_lastSpeech, w, CompareOptions.IgnoreCase) != -1);
            var isOwnSpeech = isSameWordsAsSpoken
                && (_synthesizer.State == SynthesizerState.Speaking
                || new TimeSpan(DateTime.Now.Ticks - _lastSpeechTimestamp.Ticks).TotalSeconds < 1);

            return isOwnSpeech;
        }

        private static void RefreshVoice()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.SetOutputToDefaultAudioDevice();
            _synthesizer.Rate = -2;
            _synthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, Thread.CurrentThread.CurrentCulture);

            _synthesizer.StateChanged += OnStateChanged;
        }

        private static void OnStateChanged(object sender, StateChangedEventArgs e)
        {
            if (e.State == SynthesizerState.Ready && e.PreviousState == SynthesizerState.Speaking)
            {
                _lastSpeechTimestamp = DateTime.Now;
            }
        }
    }
}
