using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;
using System.Linq;

namespace Lisa
{
    public static class Lisa
    {
        public static void Listen()
        {
            var culture = new CultureInfo("ru-RU");

            using (var synth = new SpeechSynthesizer())
            using (var recognizer = new SpeechRecognitionEngine(culture))
            {
                synth.SetOutputToDefaultAudioDevice();
                synth.Rate = -2;

                recognizer.SetInputToDefaultAudioDevice();

                var commands = Repository.GetAllCommands();
                commands.ForEach(c => c.Synthesizer = synth);

                commands.ForEach(c => recognizer.LoadGrammar(c.Grammar));

                recognizer.SpeechRecognized += OnSpeechRecognized;
                recognizer.SpeechRecognitionRejected += OnSpeechRejected;

                recognizer.RecognizeAsync(RecognizeMode.Multiple);

                while (true) { }
            }
        }

        public static void OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
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

            Repository.GetAllCommands().ForEach(c => c.Do(e));
        }

        public static void OnSpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {

        }
    }
}
