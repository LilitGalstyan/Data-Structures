using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    public enum PriorityOrder
    {
        Min,
        Max
    }

    public class PriorityQueue<T> : IEnumerable<T>, ICollection, IEnumerable where T : IComparable
    {
        private readonly PriorityOrder order;
        private readonly IList<T> data;

        public PriorityQueue(PriorityOrder order)
        {
            this.data = new List<T>();
            this.order = order;
        }

        public PriorityQueue(IEnumerable<T> data, PriorityOrder order)
        {
            this.data = data as IList<T>;
            if (this.data == null)
            {
                this.data = new List<T>(data);
            }

            this.order = order;
            this.Count = this.data.Count;
            if (this.order == PriorityOrder.Max)
            {
                Heap.BuildMaxHeap(this.data);
            }
            else
            {
                Heap.BuildMinHeap(this.data);
            }
        }

        public PriorityQueue(int initialCapacity, PriorityOrder order)
        {
            this.data = new List<T>(initialCapacity);
            this.order = order;
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public bool Contains(T item)
        {
            if (this.order == PriorityOrder.Max)
            {
                return Heap.MaxContains(this.data, item, 0, this.Count);
            }
            else
            {
                return Heap.MinContains(this.data, item, 0, this.Count);
            }
        }

        public T Dequeue()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }

            if (this.order == PriorityOrder.Max)
            {
                return Heap.ExtractMax(this.data, this.Count--);
            }
            else
            {
                return Heap.ExtractMin(this.data, this.Count--);
            }
        }

        public void Enqueue(T item, T minItem, T maxItem)
        {
            if (this.order == PriorityOrder.Max)
            {
                Heap.MaxInsert(this.data, item, maxItem, this.Count++);
            }
            else
            {
                Heap.MinInsert(this.data, item, minItem, this.Count++);
            }
        }

        public T Peek()
        {
            return this.data[0];
        }

        public void TrimExcess()
        {
            while (this.Count < this.data.Count)
            {
                this.data.RemoveAt(this.data.Count - 1);
            }
        }

        public T[] ToArray()
        {
            return this.data.ToArray();
        }

        public int Count { get; private set; } = 0;

        public object SyncRoot
        {
            get
            {
                return this.data;
            }
        }

        public bool IsSynchronized => false;
        public int Size => this.data.Count;
        public void CopyTo(Array array, int index)
        {
            this.data.CopyTo((T[])array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
        
    }
}
