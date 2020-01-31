namespace Scheduler
{
    class DoublyLinkedList<T>
    {
        public int Count { get; private set; }
        public bool IsEmpty { get { return Count == 0; } }
        public DoublyNode<T> Head { get; private set; }
        public DoublyNode<T> Tail { get; private set; }

        // добавление элемента
        public void Add(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);

            if (Head == null)
                Head = node;
            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }
            Tail = node;
            Count++;
        }
        // удаление
        public bool Remove(int _index)
        {
            DoublyNode<T> current = Head;

            if (_index > Count || _index < 1)
                return false;

                // поиск удаляемого узла
            for (int i = 1; i < _index; ++i)
                current = current.Next;

            if (current != null)
            {
                // если узел не последний
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    Tail = current.Previous;
                }

                // если узел не первый
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    Head = current.Next;
                }
                Count--;
                return true;
            }
            return false;
        }
    }
}