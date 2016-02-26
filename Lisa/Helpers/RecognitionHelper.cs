using Lisa.Commands;
using Microsoft.Speech.Recognition;

namespace Lisa.Helpers
{
    public static class RecognitionHelper
    {
        public static bool IsValid(this RecognitionResult result, string grammarName)
        {
            return result.Confidence > Lisa.AcceptableConfidence
                && !Lisa.IsSaying(result.Text)
                && result.Grammar.Name == grammarName;
        }

        public static string GetGrammarName(this Command command)
        {
            return command.GetType().ToString();
        }
    }
}
