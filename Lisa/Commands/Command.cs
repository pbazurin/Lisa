using Microsoft.Speech.Recognition;
using System;

namespace Lisa.Commands
{
    public abstract class Command
    {
        public GrammarBuilder GrammarBuilder { get; set; }

        public virtual Grammar Grammar
        {
            get
            {
                return new Grammar(GrammarBuilder)
                {
                    Name = GetType().ToString()
                };
            }
        }

        public virtual void Do(SpeechRecognizedEventArgs e)
        {
            throw new NotSupportedException();
        }
    }
}
