using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    public class Queue<T>
    {
        LinkedList<T> item = new LinkedList<T>();

        public void Enqueue(T value)
        {
            item.AddFirst(value);
        }

        public T Dequeue()
        {
            if (item.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            T last = item.Last.Value;
            item.RemoveLast();

            return last;
        }

        public T Peek()
        {
            if (item.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            return item.Last.Value;
        }

        public int Count
        {
            get { return item.Count; }
        }
    }
}
