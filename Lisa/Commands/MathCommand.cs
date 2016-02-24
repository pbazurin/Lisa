using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class MathCommand : Command
    {
        public MathCommand()
        {
            var numbers = new Choices();

            for(var i = 1; i < 20; i++)
            {
                numbers.Add(i.ToString());
            }

            var grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(numbers);
            grammarBuilder.Append(i18n.MathCommand_Plus);
            grammarBuilder.Append(numbers);

            Grammar = new Grammar(grammarBuilder);
        }

        public override bool Match(SpeechRecognizedEventArgs e)
        {
            return e.Result.Text.Contains(i18n.MathCommand_Plus);
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            var words = e.Result.Text.Split(' ');
            var result = int.Parse(words[0]) + int.Parse(words[2]);

            Lisa.Speak(result.ToString());
        }
    }
}
