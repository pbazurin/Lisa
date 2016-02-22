using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class HelloCommand : Command
    {
        public HelloCommand()
        {
            Grammar = new Grammar(new GrammarBuilder("Привет"));
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text != "Привет")
            {
                return;
            }

            Synthesizer.SpeakAsync("Привет");
        }
    }
}
