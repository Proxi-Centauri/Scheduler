using System;
using MySql.Data.MySqlClient;

namespace Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();

            menu.AddMenuItem("Загрузка списка из БД");
            menu.AddMenuItem("Добавить задачу");
            menu.AddMenuItem("Вывести задачи");
            menu.AddMenuItem("Удалить задачу");
            menu.AddMenuItem("Редактирование задачи");
            menu.AddMenuItem("Создание и сохранение таблицы");
            menu.AddMenuItem("Выход");

            Scheduler scheduler = new Scheduler();

            while (true)
            {
                menu.Print();

                ConsoleKeyInfo key = new ConsoleKeyInfo();
                key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        menu.ScrollingUp();
                        break;

                    case ConsoleKey.DownArrow:
                        menu.ScrollingDown();
                        break;

                    case ConsoleKey.Enter:
                    {
                            switch (menu.GetPos())
                            {
                                case 1:
                                    try
                                    {
                                        Console.Clear();
                                        DataBaseController controller = new DataBaseController();
                                        DataBaseController.Connect();
                                        Console.Write("Введите название таблицы: ");
                                        string nameTables = Console.ReadLine();
                                        string com = "SELECT time, header, text FROM " + nameTables + ";";
                                        MySqlDataReader reader = DataBaseController.GetTable(com);
                                        while (reader.Read())
                                        {
                                            CTask _task = new CTask(reader.GetDateTime(0), reader.GetString(1), reader.GetString(2));
                                            scheduler.AddTask(_task);
                                        }
                                        DataBaseController.Disconnect();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Clear();
                                        DataBaseController.conn = null;
                                        Console.WriteLine("Возникла ошибка при подключении к базе данных!!!\nПричина: " + ex.Message);
                                        Console.ReadKey();
                                    }
                                    break;
                                case 2:
                                    Console.Clear();
                                    try
                                    {
                                        scheduler.AddTask();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        Console.ReadKey();
                                    }
                                    break;
                                case 3:
                                    Console.Clear();
                                    if (scheduler.GetCountElement() == 0)
                                    {
                                        Console.WriteLine("Список пуст!");
                                        Console.ReadKey();
                                        break;
                                    }
                                    scheduler.PrintAllTask();
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    Console.Clear();
                                    if (scheduler.GetCountElement() == 0)
                                    {
                                        Console.WriteLine("Список пуст!");
                                        Console.ReadKey();
                                        break;
                                    }
                                    Console.WriteLine("Введите индекс элемента: ");
                                    int index = int.Parse(Console.ReadLine());
                                    scheduler.RemoveTask(index);
                                    break;
                                case 5:
                                    try
                                    {
                                        while (true)
                                        {
                                            Console.Clear();
                                            if(scheduler.GetCountElement() == 0)
                                            {
                                                Console.WriteLine("Список пуст!");
                                                Console.ReadKey();
                                                break;
                                            }
                                            Console.Write("Введите номер записи для редактирования: ");
                                            if ((index = int.Parse(Console.ReadLine())) > scheduler.GetCountElement() || index < 1)
                                                continue;
                                            scheduler.EditItem(index);
                                            break;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        Console.ReadKey();
                                    }
                                    break;
                                case 6:
                                    Console.Clear();
                                    Menu.AlertText(ConsoleColor.Black, ConsoleColor.Red, "Для сохранения таблицы" +
                                        "у вас должна быть запущена база данных");
                                    Console.WriteLine("Вы хотите продолжить подключение к базе данных? (Нажмите Y or N)");
                                    key = Console.ReadKey(true);
                                    if (key.Key == ConsoleKey.N)
                                        break;
                                    try
                                    {
                                        DataBaseController.conn = null;
                                        DataBaseController.Connect();
                                        Console.Write("Введите название таблицы: ");
                                        string nameTable = Console.ReadLine();
                                        string command = "CREATE TABLE `" + nameTable + "` (" +
     "`id` int NOT NULL AUTO_INCREMENT," +
     "`time` datetime NOT NULL," +
     "`header` varchar(100) NOT NULL," +
     "`text` varchar(500) NOT NULL," +
     "PRIMARY KEY(`id`)" +
    ")AUTO_INCREMENT = 1;";
                                        MySqlCommand comd = DataBaseController.SendingCommand(command);
                                        comd.ExecuteNonQuery();
                                        DoublyNode<CTask> temp = scheduler.getHead();
                                        while(temp != null)
                                        {
                                            command = "INSERT INTO " + nameTable + " (time, header, text) VALUES('" +
                                                temp.Data.Time.Year + "-" + temp.Data.Time.Month + "-" +
                                                temp.Data.Time.Day + " " + temp.Data.Time.Hour + ":" +
                                                temp.Data.Time.Minute + ":" + temp.Data.Time.Second + "', '" + temp.Data.Header + "', '" + temp.Data.Text +
                                                "');";
                                            comd = DataBaseController.SendingCommand(command);
                                            comd.ExecuteNonQuery();
                                            temp = temp.Next;
                                        }
                                        DataBaseController.Disconnect();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Возникла ошибка при сохранении данных!!!\nПричина: " + ex.Message);
                                    }
                                    break;
                                case 7:
                                    return;
                            }
                        break;
                    }
                }
            }
        }
    }
}