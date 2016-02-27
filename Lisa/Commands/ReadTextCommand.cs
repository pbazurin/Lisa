﻿using Lisa.Helpers;
using Lisa.Resources;
using Microsoft.Speech.Recognition;

namespace Lisa.Commands
{
    public class ReadTextCommand : Command
    {
        public override void Init(SpeechRecognitionEngine recognizer)
        {
            recognizer.LoadGrammar(new Grammar(new GrammarBuilder(i18n.ReadTextCommand_Read))
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

            Lisa.Say(@"Предвижу всё: вас оскорбит 
Печальной тайны объясненье. 
Какое горькое презренье 
Ваш гордый взгляд изобразит! 
Чего хочу? с какою целью 
Открою душу вам свою? 
Какому злобному веселью, 
Быть может, повод подаю! 

Случайно вас когда-то встретя, 
В вас искру нежности заметя, 
Я ей поверить не посмел: 
Привычке милой не дал ходу; 
Свою постылую свободу 
Я потерять не захотел. 
Еще одно нас разлучило... 
Несчастной жертвой Ленской пал... 
Ото всего, что сердцу мило, 
Тогда я сердце оторвал; 
Чужой для всех, ничем не связан, 
Я думал: вольность и покой 
Замена счастью. Боже мой! 
Как я ошибся, как наказан! 

Нет, поминутно видеть вас, 
Повсюду следовать за вами, 
Улыбку уст, движенье глаз 
Ловить влюбленными глазами, 
Внимать вам долго, понимать 
Душой всё ваше совершенство, 
Пред вами в муках замирать, 
Бледнеть и гаснуть... вот блаженство! 

И я лишен того: для вас 
Тащусь повсюду наудачу; 
Мне дорог день, мне дорог час: 
А я в напрасной скуке трачу 
Судьбой отсчитанные дни. 
И так уж тягостны они. 
Я знаю: век уж мой измерен; 
Но чтоб продлилась жизнь моя, 
Я утром должен быть уверен, 
Что с вами днем увижусь я... 

Боюсь: в мольбе моей смиренной 
Увидит ваш суровый взор 
Затеи хитрости презренной - 
И слышу гневный ваш укор. 
Когда б вы знали, как ужасно 
Томиться жаждою любви, 
Пылать - и разумом всечасно 
Смирять волнение в крови; 
Желать обнять у вас колени, 
И, зарыдав, у ваших ног 
Излить мольбы, признанья, пени, 
Всё, всё, что выразить бы мог. 
А между тем притворным хладом 
Вооружать и речь и взор, 
Вести спокойный разговор, 
Глядеть на вас веселым взглядом!.. ");
        }
    }
}
