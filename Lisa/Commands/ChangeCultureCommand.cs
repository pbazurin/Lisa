using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System.Globalization;

namespace Lisa.Commands
{
    public class ChangeCultureCommand : Command
    {
        public ChangeCultureCommand()
        {
            var cultures = new Choices();

            cultures.Add(i18n.ChangeCultureCommand_English);
            cultures.Add(i18n.ChangeCultureCommand_Russian);

            var grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(i18n.ChangeCultureCommand_Language);
            grammarBuilder.Append(cultures);

            Grammar = new Grammar(grammarBuilder);
        }

        public override bool Match(SpeechRecognizedEventArgs e)
        {
            return e.Result.Text.Contains(i18n.ChangeCultureCommand_Language);
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            var newCultureName = e.Result.Text.Replace(i18n.ChangeCultureCommand_Language, string.Empty).Trim();
            var newCulture = new CultureInfo(newCultureName == i18n.ChangeCultureCommand_English ? "en-US" : "ru-RU");

            Lisa.SetCulture(newCulture);

            var currentCultureName = newCulture.Name == "ru-RU"
                ? i18n.ChangeCultureCommand_Russian
                : i18n.ChangeCultureCommand_English;

            Lisa.Speak(string.Format(i18n.ChangeCultureCommand_CurrentLanguage, currentCultureName));
        }
    }
}
