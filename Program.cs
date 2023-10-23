using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;

namespace Ezhednevnik
{


    internal class Program
    {
        private static List<DateTime> dates;
        private static List<List<(string, string, string)>> notesByDate;
        private static int datanow;
        private static int notenowind;

        public static void Main(string[] args)
        {
            Console.WriteLine(">>>>>>>>> ЕЖЕДНЕВНИК <<<<<<<<<<");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            TUTZAMETKI();
            Console.WriteLine(" !! как этим пользоваться: !! ");
            Console.WriteLine();
            Console.WriteLine("- Как только Вы откроете заметки - сможете листать даты стрелками вправо-влево");
            Console.WriteLine("- Сами заметки можно листать стрелками вверх-вниз");
            Console.WriteLine("- Чтобы увидеть полную информацию о выбранной заметке нажмите ENTER, когда она будет подсвечена цветом");
            Console.WriteLine("- Чтобы создать новую заметку на той дате, где вы находитесь в программе, нажмите клавишу 'Z'");
            Console.WriteLine("- Чтобы пролистать даты после крайней заметки, нажмите клавишу 'D' | выйти из этих дат можно клавишей Esc");
            Console.WriteLine("- Чтобы пролистать даты перед первой заметкой, нажмите клавишу 'S' | выйти из этих дат можно клавишей Esc");
            Console.WriteLine("- Если захочешь ещё раз почитать эту инструкцию, то, уже находясь в меню заметок, ");
            Console.WriteLine(" нажми клавишу 'H' | выйти из инструкции можно клавишей Esc");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Нажмите ENTER, чтобы открыть свои заметки или Esc, чтобы выйти из программы");
            Console.WriteLine();
            Console.WriteLine("P.S. после открытия заметок, вы можете выйти из программы с помощью клавиши Esc");
            Console.WriteLine("P.S.S. из программы можно выйти также нажатием нуля, примите это как данность");
            do
            {
                ConsoleKeyInfo info;
                info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)

                {
                    Console.Clear();
                    ArrowsMenu();
                    break;
                }

                if (info.Key == ConsoleKey.Escape || info.Key == ConsoleKey.D0)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Вы завершили работу с программой");
                    Console.WriteLine("----------------------------------");
                    break;
                }
            } while (true);

        }

        public static void ArrowsMenu()
        {
            datanow = 0;
            notenowind = 0;
            ConsoleKeyInfo info;
            do
            {
                Console.Clear();
                ShowDate();
                ShowNotes();
                info = Console.ReadKey();
                switch (info.Key)
                {
                    case ConsoleKey.LeftArrow:
                        MovingDates(-1);
                        break;
                    case ConsoleKey.RightArrow:
                        MovingDates(1);
                        break;
                    case ConsoleKey.UpArrow:
                        MovingNotNextNote();
                        break;
                    case ConsoleKey.DownArrow:
                        MoveToNextNote();
                        break;
                    case ConsoleKey.Enter:
                        OpenNote();
                        break;
                    case ConsoleKey.Z:
                        AddNewNote();
                        break;
                    case ConsoleKey.D:
                        IdontKnow_But_Pust_Budet();
                        break;
                    case ConsoleKey.S:
                        IdontKnow_But_Pust_Budet2();
                        break;
                    case ConsoleKey.H:
                        HelpMePlease();
                        break;
                }

            } while (info.Key != ConsoleKey.Escape && info.KeyChar != '0');
        }

        public static void IdontKnow_But_Pust_Budet()
        {
            Console.Clear();
            ConsoleKeyInfo keyInfo;
            DateTime date = DateTime.Today;
            date = date.AddDays(12);
            do
            {
                Console.WriteLine("Дата: " + date.ToString("dd.MM.yyyy"));

    
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Console.Clear();
                    date = date.AddDays(1);
                }

                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Console.Clear();
                    date = date.AddDays(-1);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("такой кнопки не знаю, ты остановился на этой дате:");
                }

                
            } while (keyInfo.Key != ConsoleKey.Escape);
        }
        public static void TUTZAMETKI()
        {
            dates = new List<DateTime>
            {
                DateTime.Today.AddDays(6),
                DateTime.Today.AddDays(8),
                DateTime.Today.AddDays(11)
            };
            notesByDate = new List<List<(string, string, string)>>();
            for (int i = 0; i < dates.Count; i++)
            {
                notesByDate.Add(new List<(string, string, string)>());
            }

            notesByDate[0].Add(("1. Забрать гитару", " Подпилить лады, поставить новые струны", "31.10.2023."));
            notesByDate[0].Add(("2. Сделать практос по шарпам", "Написать ежедневник и запушить на гитхаб", "01.11.2023."));
            notesByDate[0].Add(("3. Дописать конспект", "Конспект по философии, приложение 1, пункт 1-3", "02.11.2023"));

            notesByDate[1].Add(("1. Купить джек-провод", "В музторге скидка 10%, бегом заказывать KLOTZ", "03.11.2023."));
            notesByDate[1].Add(("2. Встретиться с друзьями", "Забрать заказ с подарком и взять его с собой", "04.11.2023."));
            notesByDate[1].Add(("3. Приготовить покушать", "Зайти после пар в магазин и купить что-то съедобное", "06.11.2023"));

            notesByDate[2].Add(("1. Послушать лекцию по квантовой физике", "квантовая гипотеза Планка, фотоны, выписать основное в тетрадь", "07.11.2023"));
            notesByDate[2].Add(("2. Выучить табы на 'Master Of Puppets'", "найти табы и купить пластыри...", "10.11.2023"));
        
        }

       
        public static void HelpMePlease()
        {
            Console.Clear();
            Console.WriteLine(">>>>>>>>> ЕЖЕДНЕВНИК <<<<<<<<<<");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" инструкция: ");
            Console.WriteLine();
            Console.WriteLine("- Как только Вы откроете заметки - сможете листать даты стрелками вправо-влево");
            Console.WriteLine("- Сами заметки можно листать стрелками вверх-вниз");
            Console.WriteLine("- Чтобы увидеть полную информацию о выбранной заметке нажмите ENTER, когда она будет подсвечена цветом");
            Console.WriteLine("- Чтобы создать новую заметку на той дате, где вы находитесь в программе, нажмите клавишу 'Z'");
            Console.WriteLine("- Чтобы пролистать даты после крайней заметки, нажмите клавишу 'D' | выйти из этих дат можно клавишей Esc");
            Console.WriteLine("- Чтобы пролистать даты перед первой заметкой, нажмите клавишу 'S' | выйти из этих дат можно клавишей Esc");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Нажмите ENTER, чтобы вернуться к заметкам или Esc, чтобы завершить работу с программой");
            Console.WriteLine();
            Console.WriteLine("P.S. после открытия заметок, вы можете выйти из программы с помощью клавиши Esc");
            Console.WriteLine("P.S.S. из программы можно выйти также нажатием нуля, примите это как данность");
             do
            {
                ConsoleKeyInfo info;
                info = Console.ReadKey();
                if (info.Key == ConsoleKey.Enter)

                {
                    Console.Clear();
                    ArrowsMenu();
                    break;
                }

                if (info.Key == ConsoleKey.Escape || info.Key == ConsoleKey.D0)
                {
                    Console.Clear();
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Вы завершили работу с программой");
                    Console.WriteLine("----------------------------------");
                    break;
                }
            } while (true);
        }

        public static void IdontKnow_But_Pust_Budet2()
        {
            Console.Clear();
            ConsoleKeyInfo keyInfo;
            DateTime date = DateTime.Today;
            date = date.AddDays(5);
            do
            {
                Console.WriteLine("Дата: " + date.ToString("dd.MM.yyyy"));


                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    Console.Clear();
                    date = date.AddDays(1);
                }

                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    Console.Clear();
                    date = date.AddDays(-1);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("такой кнопки не знаю, ты остановился на этой дате:");
                }

            } while (keyInfo.Key != ConsoleKey.Escape);
        }

        public static void ShowDate()
        {
            Console.WriteLine("Выбрана дата: " + dates[datanow].ToString("d"));
           
        }

        public static void ShowNotes()
        {
            Console.WriteLine("TO DO:");

            List<(string, string, string)> zametka = notesByDate[datanow];


            for (int i = 0; i < zametka.Count; i++)
            {
                if (i == notenowind)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("-> " + zametka[i].Item1);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(zametka[i].Item1);
                }
            }
        }

        public static void MovingDates(int direction)
        {
            datanow = datanow + direction;
            if (datanow < 0)
            {
                datanow = dates.Count - 1;
            }
            else if (datanow >= dates.Count)
            {
                datanow = 0;
            }
          
            notenowind = 0;
        
        }

        public static void MovingNotNextNote()
        {
            List<(string, string, string)> againzametka = notesByDate[datanow];
            notenowind = (notenowind - 1 + againzametka.Count) % againzametka.Count;
        }

        public static void MoveToNextNote()
        {
            List<(string, string, string)> elsezametka = notesByDate[datanow];
            notenowind = (notenowind + 1) % elsezametka.Count;
        }

        public static void OpenNote()
        {
            Console.Clear();
            Console.WriteLine(">>> ИНФОРМАЦИЯ О ЗАМЕТКЕ <<< ");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("Текущая дата для заметки: " + dates[datanow].ToString("d"));
            Console.WriteLine("");
            List<(string, string, string)> openagainzameta = notesByDate[datanow];
            string title = openagainzameta[notenowind].Item1;
            string desc = openagainzameta[notenowind].Item2;
            string dateofnote = openagainzameta[notenowind].Item3;
            Console.WriteLine($"Ваша заметка: {title}");
            Console.WriteLine($"Описание заметки: {desc}");
            Console.WriteLine($"Дата выполнения заметки (дедлайн): {dateofnote}");
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.ReadKey();
        }
        public static void AddNewNote()
        {
            Console.Clear();
            Console.WriteLine(">>> СОЗДАНИЕ НОВОЙ ЗАМЕТКИ <<<");
            Console.WriteLine("Заметка ставится на дату: " + dates[datanow].ToString("d"));
            Console.WriteLine("Для удобства: укажите номер по порядку для вашей заметки, когда будете вписывать её название");
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Введите название новой заметки:");
            string title = Console.ReadLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Введите описание новой заметки:");
            string description = Console.ReadLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Введите дату, когда должна быть сделана эта заметка (для вашего удобства: лучше в формате число.месяц.год.)");
            string datanew = Console.ReadLine();
            List<(string, string, string)> newzametka = notesByDate[datanow];
            newzametka.Add((title, description, datanew));
            Console.WriteLine("Вы успешно добавили новую заметку! :) ");
            Console.ReadKey();
        }
    }
}