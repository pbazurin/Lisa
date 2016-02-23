using Microsoft.Speech.Recognition;
using System;

namespace Lisa.Commands
{
    public abstract class Command
    {
        public Grammar Grammar { get; set; }

        public virtual bool Match(SpeechRecognizedEventArgs e)
        {
            throw new NotSupportedException();
        }

        public virtual void Do(SpeechRecognizedEventArgs e)
        {
            throw new NotSupportedException();
        }
    }
}
