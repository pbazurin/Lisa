using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System.Globalization;

namespace Lisa.Commands
{
    public class ChangeCultureCommand : Command
    {
        public override void Init(SpeechRecognitionEngine recognizer)
        {
            var cultures = new Choices();

            cultures.Add(i18n.ChangeCultureCommand_English);
            cultures.Add(i18n.ChangeCultureCommand_Russian);

            var grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(i18n.ChangeCultureCommand_Language);
            grammarBuilder.Append(new SemanticResultKey("cultureName", cultures));

            recognizer.LoadGrammar(new Grammar(grammarBuilder)
            {
                Name = this.GetGrammarName()
            });

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!e.Result.IsValid(this.GetGrammarName()))
            {
                return;
            }

            var newCultureName = e.Result.Semantics["cultureName"].Value.ToString();

            if (newCultureName == i18n.ChangeCultureCommand_Russian)
            {
                Lisa.SetCulture(new CultureInfo("ru-RU"));
                Lisa.Speak(string.Format(i18n.ChangeCultureCommand_CurrentLanguage, i18n.ChangeCultureCommand_Russian));
            }
            else
            {
                Lisa.SetCulture(new CultureInfo("en-US"));
                Lisa.Speak(string.Format(i18n.ChangeCultureCommand_CurrentLanguage, i18n.ChangeCultureCommand_English));
            }
        }
    }
}
