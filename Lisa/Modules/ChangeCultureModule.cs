using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System.Globalization;

namespace Lisa.Modules
{
    public class ChangeCultureModule : AbstractModule
    {
        public override void Init(SpeechRecognitionEngine recognizer)
        {
            var cultures = new Choices();

            cultures.Add(i18n.ChangeCultureModule_English);
            cultures.Add(i18n.ChangeCultureModule_Russian);

            var grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(i18n.ChangeCultureModule_Language);
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

            if (newCultureName == i18n.ChangeCultureModule_Russian)
            {
                Lisa.Culture = new CultureInfo("ru-RU");
                Lisa.Say(string.Format(i18n.ChangeCultureModule_CurrentLanguage, i18n.ChangeCultureModule_Russian));
            }
            else
            {
                Lisa.Culture = new CultureInfo("en-US");
                Lisa.Say(string.Format(i18n.ChangeCultureModule_CurrentLanguage, i18n.ChangeCultureModule_English));
            }
        }
    }
}
