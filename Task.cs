using System;

namespace Scheduler
{
    class CTask
    {
        private string taskHeader;
        private string taskText;

        public DateTime Time { set; get; } = new DateTime();
        public string Header { set { taskHeader = value; } get { return taskHeader; } }
        public string Text { set { taskText = value; } get { return taskText; } }

        [Flags]
        public enum filds
        {
            Дата = 0,
            Время = 1,
            Заголовок = 2,
            Текст = 3
        }

        public CTask()
        {
            Console.Clear();
            Console.WriteLine("Введите дату(пример дд:мм:гггг): ");
            string temp = Console.ReadLine();
            string[] tempStr = temp.Split(':');
            int day = int.Parse(tempStr[0]);
            int month = int.Parse(tempStr[1]);
            int year = int.Parse(tempStr[2]);

            Console.WriteLine("Введите время(пример чч:мм:сс): ");
            temp = Console.ReadLine();
            tempStr = temp.Split(':');
            int hours = int.Parse(tempStr[0]);
            int minutes = int.Parse(tempStr[1]);
            int seconds = int.Parse(tempStr[2]);

            Time = new DateTime(year, month, day, hours, minutes, seconds);

            Console.WriteLine("Введите краткий заголовок для задачи");
            taskHeader = Console.ReadLine();
            Console.WriteLine("Введите подробное описание задачи");
            taskText = Console.ReadLine();
        }

        public CTask(DateTime _time, string _header, string _text)
        {
            Time = _time;
            Header = _header;
            Text = _text;
        }

        public void PrintTask()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("     {0}", Time);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   {0}", Header);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0}\n", Text);
            Console.ResetColor();
        }
    }
}