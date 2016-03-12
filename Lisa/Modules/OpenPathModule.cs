using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Lisa.Modules
{
    public class OpenPathModule : AbstractModule
    {
        private string _currentPath = "";

        public override void Init(SpeechRecognitionEngine recognizer)
        {
            ReloadGrammar(recognizer);

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        public void ReloadGrammar(SpeechRecognitionEngine recognizer)
        {
            var loadedGrammar = recognizer.Grammars.FirstOrDefault(g => g.Name == this.GetGrammarName());
            if (loadedGrammar != null)
            {
                recognizer.UnloadGrammar(loadedGrammar);
            }

            var grammarBuilder = new GrammarBuilder();

            grammarBuilder.Append(i18n.OpenPathModule_Open);

            var availableDestinations = new Choices();
            availableDestinations.Add(i18n.OpenPathModule_MyComputer);

            if (string.IsNullOrEmpty(_currentPath))
            {
                var allDrives = DriveInfo.GetDrives();

                foreach (var drive in allDrives.Where(d => d.IsReady))
                {
                    availableDestinations.Add(drive.VolumeLabel);
                }
            }

            grammarBuilder.Append(new SemanticResultKey("destination", availableDestinations));

            recognizer.LoadGrammar(new Grammar(grammarBuilder)
            {
                Name = this.GetGrammarName()
            });
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!e.Result.IsValid(this.GetGrammarName()))
            {
                return;
            }

            var currentDestination = e.Result.Semantics["destination"].Value;

            if (Equals(currentDestination, i18n.OpenPathModule_MyComputer))
            {
                Process.Start("::{20d04fe0-3aea-1069-a2d8-08002b30309d}");
            } else
            {
                Process.Start(currentDestination + ":");
                _currentPath += currentDestination + ":";
            }

            ReloadGrammar((SpeechRecognitionEngine)sender);
        }
    }
}
