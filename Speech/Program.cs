using System;
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System.Globalization;

namespace Speach
{
    class Program
    {
        static void Main(string[] args)
        {
            var culture = new CultureInfo("ru-RU");

            using (var synth = new SpeechSynthesizer())
            using (var recognizer = new SpeechRecognitionEngine(culture))
            {
                synth.SetOutputToDefaultAudioDevice();
                recognizer.SetInputToDefaultAudioDevice();

                var choises = new Choices();
                choises.Add(new string[] { "привет", "пока", "как дела" });
                var gb = new GrammarBuilder
                {
                    Culture = culture
                };
                gb.Append(choises);
                var g = new Grammar(gb);
                recognizer.LoadGrammar(g);

                recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>((object sender, SpeechRecognizedEventArgs e) =>
              {
                  switch (e.Result.Text)
                  {
                      case "привет":
                          synth.Speak("привет");
                          break;
                      case "пока":
                          synth.Speak("пока");
                          Environment.Exit(0);
                          break;
                      case "как дела":
                          synth.Speak("отлично");
                          break;
                  }

                  recognizer.Recognize();
              });

                recognizer.SpeechRecognitionRejected +=
              new EventHandler<SpeechRecognitionRejectedEventArgs>((object sender, SpeechRecognitionRejectedEventArgs e) =>
              {
                  synth.Speak("Не поняла");
                  recognizer.Recognize();
              });

                recognizer.Recognize();
            }
        }
    }
}
