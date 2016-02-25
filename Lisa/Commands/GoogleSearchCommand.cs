using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class GoogleSearchCommand : Command
    {
        public GoogleSearchCommand()
        {
            var grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(i18n.GoogleSearchCommand_Google);
            //grammarBuilder.AppendDictation();

            GrammarBuilder = grammarBuilder;
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            
        }
    }
}
