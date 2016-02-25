using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class HelloCommand : Command
    {
        public HelloCommand()
        {
            GrammarBuilder = new GrammarBuilder(i18n.HelloCommand_Hello);
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            Lisa.Speak(i18n.HelloCommand_Hello);
        }
    }
}
