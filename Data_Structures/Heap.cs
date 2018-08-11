using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    public static class Heap
    {
        public static void Sort<T>(IList<T> data) where T : IComparable
        {
            var heapSize = data.Count;
            BuildMaxHeap(data);
            for (int i = heapSize - 1; i > -1; i--)
            {
                Swap(data, 0, i);
                heapSize--;
                MaxHeapify(data, 0, heapSize);
            }

        }
        public static void BuildMaxHeap<T>(IList<T> data) where T : IComparable
        {
            var heapSize = data.Count;
            for (int index = (heapSize / 2) - 1; index > -1; index--)
            {
                MaxHeapify(data, index, heapSize);
            }
        }

        public static void BuildMinHeap<T>(IList<T> data) where T : IComparable
        {
            var heapSize = data.Count;
            for (int index = (heapSize / 2) - 1; index > -1; index--)
            {
                MinHeapify(data, index, heapSize);
            }
        }

        public static void MaxHeapify<T>(IList<T> data, int index, int heapSize) where T : IComparable
        {
            var largest = index;
            var left = HeapLeft(index);
            var right = HeapRight(index);

            if (left < heapSize && (data[left] != null && data[left].CompareTo(data[index]) > 0))
            {
                largest = left;
            }

            if (right < heapSize && (data[right] != null && data[right].CompareTo(data[largest]) > 0))
            {
                largest = right;
            }

            if (largest != index)
            {
                var tempRef = data[index];
                data[index] = data[largest];
                data[largest] = tempRef;

                MaxHeapify(data, largest, heapSize);
            }
        }

        public static void MinHeapify<T>(IList<T> data, int index, int heapSize) where T : IComparable
        {
            var smallest = index;
            var left = HeapLeft(index);
            var right = HeapRight(index);

            if (left < heapSize && (data[left] == null || data[left].CompareTo(data[index]) < 0))
            {
                smallest = left;
            }

            if (right < heapSize && (data[right] == null || data[right].CompareTo(data[smallest]) < 0))
            {
                smallest = right;
            }

            if (smallest != index)
            {
                var tempRef = data[index];
                data[index] = data[smallest];
                data[smallest] = tempRef;

                MinHeapify(data, smallest, heapSize);
            }
        }

        public static T ExtractMax<T>(IList<T> data, int heapSize) where T : IComparable
        {
            heapSize--;
            if (heapSize < 0)
            {
                throw new IndexOutOfRangeException();
            }

            T max = data[0];
            data[0] = data[heapSize];
            if (heapSize > 0)
            {
                MaxHeapify(data, 0, heapSize);
            }

            return max;
        }

        public static T ExtractMin<T>(IList<T> data, int heapSize) where T : IComparable
        {
            heapSize--;
            if (heapSize < 0)
            {
                throw new IndexOutOfRangeException();
            }

            T max = data[0];
            data[0] = data[heapSize];
            if (heapSize > 0)
            {
                MinHeapify(data, 0, heapSize);
            }

            return max;
        }

        public static void MaxIncrease<T>(IList<T> data, int index, T item) where T : IComparable
        {
            if (null == item || item.CompareTo(data[index]) < 0)
            {
                throw new ArgumentException("new item is smaller than the current item", "item");
            }

            data[index] = item;
            var parent = HeapParent(index);

            while (index > 0 && (data[parent] == null || data[parent].CompareTo(data[index]) < 0))
            {
                var tempRef = data[index];
                data[index] = data[parent];
                data[parent] = tempRef;
                index = parent;
                parent = HeapParent(index);
            }
        }

        public static void MinDecrease<T>(IList<T> data, int index, T item) where T : IComparable
        {
            if (null == item || item.CompareTo(data[index]) > 0)
            {
                throw new ArgumentException("new item is greater than the current item", "item");
            }

            data[index] = item;
            var parent = HeapParent(index);

            while (index > 0 && (data[index] == null || data[index].CompareTo(data[parent]) < 0))
            {
                var tempRef = data[index];
                data[index] = data[parent];
                data[parent] = tempRef;
                index = parent;
                parent = HeapParent(index);
            }
        }

        public static void MaxInsert<T>(IList<T> data, T item, T minOfT, int heapSize) where T : IComparable
        {
            heapSize++;
            if (heapSize - 1 < data.Count)
            {
                data[heapSize - 1] = minOfT;
            }
            else
            {
                data.Add(minOfT);
            }

            MaxIncrease(data, heapSize - 1, item);
        }

        public static void MinInsert<T>(IList<T> data, T item, T maxOfT, int heapSize) where T : IComparable
        {
            heapSize++;
            if (heapSize - 1 < data.Count)
            {
                data[heapSize - 1] = maxOfT;
            }
            else
            {
                data.Add(maxOfT);
            }

            MinDecrease(data, heapSize - 1, item);
        }

        public static bool MaxContains<T>(IList<T> data, T item, int index, int heapSize) where T : IComparable
        {
            if (index >= heapSize)
            {
                return false;
            }

            if (index == 0)
            {
                if (data[index] == null)
                {
                    if (item == null)
                    {
                        return true;
                    }
                }
                else
                {
                    var rootComp = data[index].CompareTo(item);
                    if (rootComp == 0)
                    {
                        return true;
                    }

                    if (rootComp < 0)
                    {
                        return false;
                    }
                }
            }

            var left = HeapLeft(index);
            var leftComp = 0;
            if (left < heapSize)
            {
                if (data[left] == null)
                {
                    if (item == null)
                    {
                        return true;
                    }
                }
                else
                {
                    leftComp = data[left].CompareTo(item);
                    if (leftComp == 0)
                    {
                        return true;
                    }
                }
            }

            var right = HeapRight(index);
            var rightComp = 0;

            if (right < heapSize)
            {
                if (data[right] == null)
                {
                    if (item == null)
                    {
                        return true;
                    }
                }
                else
                {
                    rightComp = data[right].CompareTo(item);
                    if (rightComp == 0)
                    {
                        return true;
                    }
                }
            }

            if (leftComp < 0 && rightComp < 0)
            {
                return false;
            }

            var leftResult = false;

            if (leftComp > 0)
            {
                leftResult = MaxContains(data, item, left, heapSize);
            }

            if (leftResult)
            {
                return true;
            }

            var rightResult = false;

            if (rightComp > 0)
            {
                rightResult = MaxContains(data, item, right, heapSize);
            }
            return rightResult;
        }

        public static bool MinContains<T>(IList<T> data, T item, int index, int heapSize) where T : IComparable
        {
            if (index >= heapSize)
            {
                return false;
            }

            if (index == 0)
            {
                if (data[index] == null)
                {
                    if (item == null)
                    {
                        return true;
                    }
                }
                else
                {
                    var rootComp = data[index].CompareTo(item);
                    if (rootComp == 0)
                    {
                        return true;
                    }

                    if (rootComp > 0)
                    {
                        return false;
                    }
                }
            }

            var left = HeapLeft(index);
            var leftComp = 0;
            if (left < heapSize)
            {
                if (data[left] == null)
                {
                    if (item == null)
                    {
                        return true;
                    }
                }
                else
                {
                    leftComp = data[left].CompareTo(item);
                    if (leftComp == 0)
                    {
                        return true;
                    }
                }
            }

            var right = HeapRight(index);
            var rightComp = 0;

            if (right < heapSize)
            {
                if (data[right] == null)
                {
                    if (item == null)
                    {
                        return true;
                    }
                }
                else
                {
                    rightComp = data[right].CompareTo(item);
                    if (rightComp == 0)
                    {
                        return true;
                    }
                }
            }

            if (leftComp > 0 && rightComp > 0)
            {
                return false;
            }

            var leftResult = false;

            if (leftComp < 0)
            {
                leftResult = MinContains(data, item, left, heapSize);
            }

            if (leftResult)
            {
                return true;
            }

            var rightResult = false;

            if (rightComp < 0)
            {
                rightResult = MinContains(data, item, right, heapSize);
            }
            return rightResult;
        }

        private static void Swap<T>(IList<T> data, int x, int y)
        {
            var temp = data[x];
            data[x] = data[y];
            data[y] = temp;
        }

        private static int HeapParent(int i)
        {
            return i >> 1;
        }

        private static int HeapLeft(int i)
        {
            return (i << 1) + 1;
        }

        private static int HeapRight(int i)
        {
            return (i << 1) + 2;
        }
    }
}
