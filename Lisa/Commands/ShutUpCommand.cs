using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class ShutUpCommand : Command
    {
        public ShutUpCommand()
        {
            GrammarBuilder = new GrammarBuilder(i18n.ShutUpCommand_ShutUp);
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            Lisa.StopSpeaking();
        }
    }
}
