using Microsoft.Speech.Recognition;
using System;

namespace Lisa.Commands
{
    public abstract class Command
    {
        public virtual void Init(SpeechRecognitionEngine recognizer)
        {
            throw new NotSupportedException();
        }
    }
}
