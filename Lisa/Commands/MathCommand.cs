using Microsoft.Speech.Recognition;
using System;

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

        public override void Do(SpeechRecognizedEventArgs e)
        {
            if (!e.Result.Text.Contains("плюс"))
            {
                return;
            }

            var words = e.Result.Text.Split(' ');
            var result = Int32.Parse(words[0]) + Int32.Parse(words[2]);

            Synthesizer.SpeakAsync(result.ToString());
        }
    }
}
