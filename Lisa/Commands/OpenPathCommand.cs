using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System.Diagnostics;

namespace Lisa.Commands
{
    public class OpenPathCommand : Command
    {
        public override void Init(SpeechRecognitionEngine recognizer)
        {
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder(i18n.OpenPathCommand_Open))
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

            Process.Start("::{20d04fe0-3aea-1069-a2d8-08002b30309d}");
        }
    }
}
