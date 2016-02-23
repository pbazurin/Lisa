using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class ShutUpCommand : Command
    {
        public ShutUpCommand()
        {
            Grammar = new Grammar(new GrammarBuilder(i18n.ShutUpCommand_ShutUp));
        }

        public override bool Match(SpeechRecognizedEventArgs e)
        {
            return e.Result.Text == i18n.ShutUpCommand_ShutUp;
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            Lisa.StopListening();
            Lisa.StartListening();
        }
    }
}
