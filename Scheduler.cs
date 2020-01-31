using System;

namespace Scheduler
{
    class Scheduler
    {
        private DoublyLinkedList<CTask> tasks = new DoublyLinkedList<CTask>();
        
        public void AddTask()
        {
            tasks.Add(new CTask());
            
        }

        public DoublyNode<CTask> getHead()
        {
            return tasks.Head;
        }
        public void AddTask(CTask _task)
        {
            tasks.Add(_task);
        }

        public void RemoveTask(int _index)
        {
            tasks.Remove(_index);
        }

        public void PrintAllTask()
        {
            DoublyNode<CTask> temp = tasks.Head;
            while(temp != null)
            {
                temp.Data.PrintTask();
                temp = temp.Next;
            }
        }

        public int GetCountElement()
        {
            return tasks.Count;
        }

        public void EditItem(int _index)
        {
            DoublyNode<CTask> current = tasks.Head;

            if (_index > tasks.Count || _index < 1)
                return;

            for (int i = 1; i < _index; ++i)
                current = current.Next;
            
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            CTask.filds fild = CTask.filds.Дата;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Редактирование записи");
                current.Data.PrintTask();
                Console.WriteLine("Редактирование {0}", fild.ToString());
                Menu.AlertText(ConsoleColor.Black, ConsoleColor.Red, "Для выбора необходимого поля " +
                    "используйте стрелочки и Enter, для сохранения нажмите S");
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        fild = (fild == CTask.filds.Дата) ? CTask.filds.Текст : --fild;
                        
                        break;
                    case ConsoleKey.DownArrow:
                        fild = (fild == CTask.filds.Текст) ? CTask.filds.Дата : ++fild;
                        break;
                    case ConsoleKey.Enter:
                        switch (fild)
                        {
                            case CTask.filds.Дата:
                                Console.Write("Введите дату(пример дд:мм:гггг): ");
                                string temp = Console.ReadLine();
                                string[] tempStr = temp.Split(':');
                                int day = int.Parse(tempStr[0]);
                                int month = int.Parse(tempStr[1]);
                                int year = int.Parse(tempStr[2]);
                                current.Data.Time = new DateTime(year, month, day, current.Data.Time.Hour,
                                    current.Data.Time.Month,current.Data.Time.Second);
                                break;
                            case CTask.filds.Время:
                                Console.WriteLine("Введите время(пример чч:мм:сс): ");
                                temp = Console.ReadLine();
                                tempStr = temp.Split(':');
                                int hours = int.Parse(tempStr[0]);
                                int minutes = int.Parse(tempStr[1]);
                                int seconds = int.Parse(tempStr[2]);
                                current.Data.Time = new DateTime(current.Data.Time.Year, current.Data.Time.Month, current.Data.Time.Day,
                                    hours, minutes, seconds);
                                break;
                            case CTask.filds.Заголовок:
                                Console.WriteLine("Введите краткий заголовок для задачи");
                                current.Data.Header = Console.ReadLine();
                                break;
                            case CTask.filds.Текст:
                                Console.WriteLine("Введите подробное описание задачи");
                                current.Data.Text = Console.ReadLine();
                                break;
                        }
                        break;
                    case ConsoleKey.S:
                        return;
                }
            }
        } 
    }
}