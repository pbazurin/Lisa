using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System.Diagnostics;

namespace Lisa.Commands
{
    public class OpenGoogleCommand : Command
    {
        public OpenGoogleCommand()
        {
            GrammarBuilder = new GrammarBuilder(i18n.GoogleSearchCommand_Google);
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            Process.Start("http://google.com");
        }
    }
}
