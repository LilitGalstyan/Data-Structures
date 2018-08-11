using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    class Stack<T> : IEnumerable<T>
    {
        LinkedList<T> list = new LinkedList<T>();

        public void Push(T value)
        {
            list.AddLast(value);
        }

        public T Pop()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Stack is Empty.");
            }

            T value = list.Last.Value;
            list.RemoveLast();
            Console.WriteLine($"Pop - {value}");
            return value;
        }

        public T Peek()
        {
            if (list.Count == 0)
            {
                throw new InvalidOperationException("Stack is Empty.");
            }
            Console.WriteLine($"Peek - {list.Last.Value}");
            return list.Last.Value;
        }

        public void Clear()
        {
            list.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)list).GetEnumerator();
        }
       
    }
}
