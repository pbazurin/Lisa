using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
using System;
using System.Globalization;

namespace Lisa.Commands
{
    public abstract class Command
    {
        public Grammar Grammar { get; set; }

        public SpeechSynthesizer Synthesizer { get; set; }

        public virtual void Do(SpeechRecognizedEventArgs e)
        {
            throw new NotSupportedException();
        }
    }
}
