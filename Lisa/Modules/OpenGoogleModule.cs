using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System.Diagnostics;

namespace Lisa.Modules
{
    public class OpenGoogleModule : AbstractModule
    {
        public override void Init(SpeechRecognitionEngine recognizer)
        {
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder(i18n.GoogleSearchModule_Google)) {
                Name = this.GetGrammarName()
            });

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!e.Result.IsValid(this.GetGrammarName()))
            {
                return;
            }

            Process.Start("http://google.com");
        }
    }
}
