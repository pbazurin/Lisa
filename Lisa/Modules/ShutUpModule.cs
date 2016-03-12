using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Modules
{
    public class ShutUpModule : AbstractModule
    {
        public override void Init(SpeechRecognitionEngine recognizer)
        {
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder(i18n.ShutUpModule_ShutUp))
            {
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

            Lisa.StopSpeaking();
        }
    }
}
