using System;

namespace Scheduler
{
    class Menu
    {
        private DoublyLinkedList<string> menu = new DoublyLinkedList<string>();
        DoublyNode<string> current = null, temp = null;

        public void AddMenuItem(string _title)
        {
            menu.Add(_title);
            if (current == null)
                current = menu.Head;
        }

        public static void AlertText(ConsoleColor _textColor, ConsoleColor _backgroundColor, string _text)
        {
            Console.BackgroundColor = _backgroundColor;
            Console.ForegroundColor = _textColor;
            Console.WriteLine("\n{0, -90}", ' ');
            Console.WriteLine("{0, -90}", _text);
            Console.WriteLine("{0, -90}", ' ');
            Console.ResetColor();
        }

        public void Print()
        {
            Console.Clear();
            temp = menu.Head;
            while (temp != null)
            {
                Console.WriteLine((current == temp) ? "==>{0}" : "{0}", temp.Data);
                temp = temp.Next;
            }

            AlertText(ConsoleColor.Black, ConsoleColor.Red, "Для перемещения по меню используйте стрелочки," +
                "для выбора выделеного элемента нажмите Enter");
        }

        public void ScrollingUp()
        {
            if (menu.IsEmpty || menu.Count == 1)
                return;
            else
            {
                if (current.Previous == null)
                    current = menu.Tail;
                else current = current.Previous;
            }
        }

        public void ScrollingDown()
        {
            if (menu.IsEmpty || menu.Count == 1)
                return;
            else
            {
                if (current.Next == null)
                    current = menu.Head;
                else current = current.Next;
            }
        }

        public int GetPos()
        {
            int i = 0;
            temp = menu.Head;
            while (temp != current)
            {
                ++i;
                temp = temp.Next;
            }
            return i + 1;
        }
    }
}
