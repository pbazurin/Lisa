using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class HelloCommand : Command
    {
        public override void Init(SpeechRecognitionEngine recognizer)
        {
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder(i18n.HelloCommand_Hello))
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

            Lisa.Say(i18n.HelloCommand_Hello);
        }
    }
}
