using Microsoft.Speech.Recognition;
using System;

namespace Lisa.Modules
{
    public abstract class AbstractModule
    {
        public virtual void Init(SpeechRecognitionEngine recognizer)
        {
            throw new NotSupportedException();
        }
    }
}
