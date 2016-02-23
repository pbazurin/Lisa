﻿using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class HelloCommand : Command
    {
        public HelloCommand()
        {
            Grammar = new Grammar(new GrammarBuilder(i18n.HelloCommand_Hello));
        }

        public override bool Match(SpeechRecognizedEventArgs e)
        {
            return e.Result.Text == i18n.HelloCommand_Hello;
        }

        public override void Do(SpeechRecognizedEventArgs e)
        {
            Lisa.Speak(i18n.HelloCommand_Hello);
        }
    }
}
