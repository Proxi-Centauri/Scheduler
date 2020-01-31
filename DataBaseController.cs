using System;
using MySql.Data.MySqlClient;

namespace Scheduler
{
    class DataBaseController
    {
        public static MySqlConnection conn = null;
        public static string dataBaseName = null;
        public static void Connect()
        {
            if (conn != null) return;

            Console.WriteLine("Подключение к базе данных");
            Console.Write("Введите название хоста: ");
            string server = Console.ReadLine();
            Console.Write("Введите имя пользователя: ");
            string userId = Console.ReadLine();
            Console.Write("Введите пароль пользователя: ");
            string password = Console.ReadLine();
            Console.Write("Введите название базы данных: ");
            string dataBase = Console.ReadLine();
            dataBaseName = dataBase;

            string temp = @"server=" + server + ";user=" + userId + @";password=" + password + ";database=" + dataBase + ";";
            try
            {
                conn = new MySqlConnection(temp);
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

        public static MySqlDataReader GetTable(string _command)
        {
            MySqlCommand command = new MySqlCommand(_command, conn);
            MySqlDataReader reader = command.ExecuteReader();
            return reader;
        }

        public static MySqlCommand SendingCommand(string _command)
        {
            MySqlCommand command = new MySqlCommand(_command, conn);
            return command;
        }

        public static void Disconnect()
        {
            conn.Close();
        }
    }
}
