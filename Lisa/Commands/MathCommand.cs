using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class MathCommand : Command
    {
        public MathCommand()
        {
            var numbers = new Choices();

            numbers.Add("1");
            numbers.Add("2");
            numbers.Add("3");
            numbers.Add("4");

            var grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(numbers);
            grammarBuilder.Append("плюс");
            grammarBuilder.Append(numbers);

            Grammar = new Grammar(grammarBuilder);
        }

        public override bool Match(SpeechRecognizedEventArgs e)
        {
            return e.Result.Text.Contains("плюс");
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            var words = e.Result.Text.Split(' ');
            var result = int.Parse(words[0]) + int.Parse(words[2]);

            Lisa.Speak(result.ToString());
        }
    }
}
