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

            grammarBuilder.Append(new SemanticResultKey("firstNumber", numbers));
            grammarBuilder.Append(i18n.MathCommand_Plus);
            grammarBuilder.Append(new SemanticResultKey("secondNumber", numbers));

            GrammarBuilder = grammarBuilder;
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            var firstNumber = int.Parse(e.Result.Semantics["firstNumber"].Value.ToString());
            var secondNumber = int.Parse(e.Result.Semantics["secondNumber"].Value.ToString());

            Lisa.Speak((firstNumber + secondNumber).ToString());
        }
    }
}
