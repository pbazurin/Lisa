using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Lisa
{
    public static class Lisa
    {
        private static bool _isListening = false;
        private static SpeechRecognitionEngine _recognizer = null;
        private static SpeechSynthesizer _synthesizer = null;
        private static Thread _listenThread = null;

        private static object _sync = new object();

        private static bool IsListening
        {
            get
            {
                lock (_sync)
                {
                    return _isListening;
                }
            }
            set
            {
                lock (_sync)
                {
                    _isListening = value;
                }
            }
        }

        static Lisa()
        {
            var defaultCulture = new CultureInfo("ru-RU");

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = defaultCulture;

            RefreshVoice();
        }

        public static void SetCulture(CultureInfo newCulture)
        {
            var wasListening = IsListening;

            if (IsListening)
            {
                StopListening();
            }

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = newCulture;

            RefreshVoice();

            if (wasListening && !IsListening)
            {
                StartListening();
            }
        }

        public static void StartListening()
        {
            if (IsListening)
            {
                return;
            }

            IsListening = true;

            _recognizer = new SpeechRecognitionEngine(Thread.CurrentThread.CurrentCulture);
            _recognizer.SetInputToDefaultAudioDevice();

            var commands = Repository.GetAllCommands();
            commands.ForEach(c => _recognizer.LoadGrammar(c.Grammar));

            _recognizer.SpeechRecognized += OnSpeechRecognized;
            _recognizer.SpeechRecognitionRejected += OnSpeechRejected;

            _listenThread = new Thread(new ThreadStart(() =>
            {
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);

                while (true) { }
            }));
            _listenThread.IsBackground = true;
            _listenThread.Start();
        }

        public static void StopListening()
        {
            IsListening = false;

            _recognizer.RecognizeAsyncStop();
            _listenThread.Abort();
        }

        public static void Speak(string textToSpeech)
        {
            _synthesizer.SpeakAsync(textToSpeech);
        }

        private static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence < 0.70)
            {
                return;
            }

            var recognizer = sender as SpeechRecognitionEngine;
            if (recognizer == null)
            {
                return;
            }

            Repository.GetAllCommands().First(c => c.Match(e)).Do(e);
        }

        private static void OnSpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {

        }

        private static void RefreshVoice()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.SetOutputToDefaultAudioDevice();
            _synthesizer.Rate = -2;
            _synthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, Thread.CurrentThread.CurrentCulture);
        }
    }
}
