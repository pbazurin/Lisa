using Lisa.Helpers;
using Microsoft.Speech.Recognition;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lisa.Modules
{
    public class KeyboardAdapterModule : AbstractModule
    {
        private static Dictionary<string, string> EnglishPhoneticAlphabet = new Dictionary<string, string>
        {
            { "Alpha",      "a"},
            { "Bravo",      "b"},
            { "Charlie",    "c"},
            { "Delta",      "d"},
            { "Echo",       "e"},
            { "Foxtrot",    "f"},
            { "Golf",       "g"},
            { "Hotel",      "h"},
            { "India",      "i"},
            { "Juliett",    "j"},
            { "Kilo",       "k"},
            { "Lima",       "l"},
            { "Mike",       "m"},
            { "November",   "n"},
            { "Oscar",      "o"},
            { "Papa",       "p"},
            { "Quebec",     "q"},
            { "Romeo",      "r"},
            { "Sierra",     "s"},
            { "Tango",      "t"},
            { "Uniform",    "u"},
            { "Victor",     "v"},
            { "Whiskey",    "w"},
            { "X-ray",      "x"},
            { "Yankee",     "y"},
            { "Zulu",       "z"},

            { "Dash",       "-"},
            { "Underscore", "_"},
            { "Dot",        "."},
            { "Comma",      ","},
            { "Space",      " "},

            { "Zero",       "0" },
            { "One",        "1" },
            { "Two",        "2" },
            { "Three",      "3" },
            { "Four",       "4" },
            { "Five",       "5" },
            { "Six",        "6" },
            { "Seven",      "7" },
            { "Eight",      "8" },
            { "Nine",       "9" },

            { "Enter",       "{Enter}" },
            { "BACKSPACE",   "{BS}" }
        };

        private static Dictionary<string, string> RussianPhoneticAlphabet = new Dictionary<string, string>
        {
            { "Анна",         "а"},
            { "Борис",        "б"},
            { "Василий",      "в"},
            { "Григорий",     "г"},
            { "Дмитрий",      "д"},
            { "Елена",        "е"},
            { "Женя",         "ж"},
            { "Зинаида",      "з"},
            { "Иван",         "и"},
            { "Йот",          "й"},
            { "Константин",   "к"},
            { "Леонид",       "л"},
            { "Михаил",       "м"},
            { "Николай",      "н"},
            { "Ольга",        "о"},
            { "Павел",        "п"},
            { "Роман",        "р"},
            { "Семен",        "с"},
            { "Татьяна",      "т"},
            { "Ульяна",       "у"},
            { "Федор",        "ф"},
            { "Харитон",      "х"},
            { "Цапля",        "ц"},
            { "Человек",      "ч"},
            { "Шура",         "ш"},
            { "Щука",         "щ"},
            { "Твердый знак", "ъ"},
            { "Эры",          "ы"},
            { "Мягкий знак",  "ь"},
            { "Эхо",          "э"},
            { "Юрий",         "ю"},
            { "Яков",         "я"},

            { "Тире",         "-"},
            { "Подчеркивание","_"},
            { "Точка",        "."},
            { "Запятая",      ","},
            { "Пробел",       " "},

            { "Ноль",         "0" },
            { "Один",         "1" },
            { "Два",          "2" },
            { "Три",          "3" },
            { "Четыре",       "4" },
            { "Пять",         "5" },
            { "Шесть",        "6" },
            { "Семь",         "7" },
            { "Восемь",       "8" },
            { "Девять",       "9" },

            { "Энтер",       "{Enter}" },
            { "Удалить",     "{BS}" }
        };

        public override void Init(SpeechRecognitionEngine recognizer)
        {
            var grammarBuilder = new GrammarBuilder();

            var choises = new Choices(GetCurrentPhoneticAlphabet().Keys.ToArray());

            grammarBuilder.Append(choises);

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

            SendKeys.Send(GetCurrentPhoneticAlphabet()[e.Result.Text]);
        }

        private Dictionary<string, string> GetCurrentPhoneticAlphabet()
        {
            return Lisa.Culture.Name == "ru-RU" ? RussianPhoneticAlphabet : EnglishPhoneticAlphabet;
        }
    }
}
