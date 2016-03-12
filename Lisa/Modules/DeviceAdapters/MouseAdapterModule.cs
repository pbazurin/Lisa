using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Lisa.Modules
{
    public class MouseAdapterModule : AbstractModule
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        private enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        private static int _step = 48;
        private const int MinimalStep = 1;
        private const int MaximalStep = 512;


        private Dictionary<string, Action> MouseAdapterActions;

        private static bool _isAdapterActive = false;

        public override void Init(SpeechRecognitionEngine recognizer)
        {
            MouseAdapterActions = new Dictionary<string, Action>
            {
                { i18n.MouseAdapterModule_MoveUp,        MoveUp        },
                { i18n.MouseAdapterModule_MoveDown,      MoveDown      },
                { i18n.MouseAdapterModule_MoveLeft,      MoveLeft      },
                { i18n.MouseAdapterModule_MoveRight,     MoveRight     },

                { i18n.MouseAdapterModule_Hold,          Hold          },
                { i18n.MouseAdapterModule_Release,       Release       },
                { i18n.MouseAdapterModule_Click,         Click         },
                { i18n.MouseAdapterModule_DoubleClick,   DoubleClick   },

                { i18n.MouseAdapterModule_IncreaseStep,  IncreaseStep  },
                { i18n.MouseAdapterModule_DecreaseStep,  DecreaseStep  }
            };

            if (_isAdapterActive)
            {
                ActivateAdapter(recognizer);
            }
            else
            {
                DeactivateAdapter(recognizer);
            }

            recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!e.Result.IsValid(this.GetGrammarName()))
            {
                return;
            }

            if (e.Result.Text == i18n.MouseAdapterModule_TurnOnMouseAdapter)
            {
                ActivateAdapter((SpeechRecognitionEngine)sender);
                return;
            }

            if (e.Result.Text == i18n.MouseAdapterModule_TurnOffMouseAdapter)
            {
                DeactivateAdapter((SpeechRecognitionEngine)sender);
                return;
            }

            if (MouseAdapterActions.ContainsKey(e.Result.Text))
            {
                MouseAdapterActions[e.Result.Text]();
            }
        }

        private void ActivateAdapter(SpeechRecognitionEngine recognizer)
        {
            var grammarName = this.GetGrammarName();
            var loadedGrammar = recognizer.Grammars.FirstOrDefault(g => g.Name == grammarName);

            if (!_isAdapterActive || loadedGrammar == null)
            {
                if (loadedGrammar != null)
                {
                    recognizer.UnloadGrammar(loadedGrammar);
                }

                var grammarBuilder = new GrammarBuilder();

                var choises = new Choices(MouseAdapterActions.Keys.ToArray());

                choises.Add(i18n.MouseAdapterModule_TurnOffMouseAdapter);

                grammarBuilder.Append(choises);

                recognizer.LoadGrammar(new Grammar(grammarBuilder)
                {
                    Name = grammarName
                });

                _isAdapterActive = true;
            }

            if (loadedGrammar != null)
            {
                Lisa.Say(i18n.MouseAdapterModule_MouseAdapterIsTurnedOn);
            }
        }

        private void DeactivateAdapter(SpeechRecognitionEngine recognizer)
        {
            var grammarName = this.GetGrammarName();
            var loadedGrammar = recognizer.Grammars.FirstOrDefault(g => g.Name == grammarName);

            if (_isAdapterActive || loadedGrammar == null)
            {
                if (loadedGrammar != null)
                {
                    recognizer.UnloadGrammar(loadedGrammar);
                }

                var grammarBuilder = new GrammarBuilder(i18n.MouseAdapterModule_TurnOnMouseAdapter);

                recognizer.LoadGrammar(new Grammar(grammarBuilder)
                {
                    Name = grammarName
                });

                _isAdapterActive = false;
            }

            if(loadedGrammar != null)
            {
                Lisa.Say(i18n.MouseAdapterModule_MouseAdapterIsTurnedOff);
            }
        }

        private static void MoveUp()
        {
            mouse_event((int)MouseEventFlags.MOVE, 0, -_step, 0, 0);
        }

        private static void MoveDown()
        {
            mouse_event((int)MouseEventFlags.MOVE, 0, _step, 0, 0);
        }

        private static void MoveLeft()
        {
            mouse_event((int)MouseEventFlags.MOVE, -_step, 0, 0, 0);
        }

        private static void MoveRight()
        {
            mouse_event((int)MouseEventFlags.MOVE, _step, 0, 0, 0);
        }

        private static void Click()
        {
            mouse_event((int)(MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP), _step, 0, 0, 0);
        }

        private static void DoubleClick()
        {
            mouse_event((int)(MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP), _step, 0, 0, 0);
            Thread.Sleep(150);
            mouse_event((int)(MouseEventFlags.LEFTDOWN | MouseEventFlags.LEFTUP), _step, 0, 0, 0);
        }

        private static void Hold()
        {
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
        }

        private static void Release()
        {
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        private static void IncreaseStep()
        {
            if (_step >= MaximalStep)
            {
                Lisa.Say(i18n.MouseAdapterModule_StepIsMaximal);
                return;
            }

            _step *= 2;
        }

        private static void DecreaseStep()
        {
            if (_step <= MinimalStep)
            {
                Lisa.Say(i18n.MouseAdapterModule_StepIsMinimal);
                return;
            }

            _step /= 2;
        }
    }
}
