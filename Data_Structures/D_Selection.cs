using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structures
{
    class D_Selection
    {
        public static int DSelection(int[] arr, int element)
        {
            int median = TheMedianFromArray(arr);
            int pivot = Partitioning(arr, median);

            if (element == pivot + 1)
            {
                return arr[pivot];
            }

            else if (element > pivot + 1)
            {
                return DSelection(arr.Skip(pivot + 1).ToArray(), element - pivot - 1);
            }

            else
            {
                return DSelection(arr.Take(pivot).ToArray(), element);
            }
        }

        private static int TheMedianFromArray(int[] arr)
        {
            if (arr.Length <= 5)
            {
                Array.Sort(arr);
                return arr[arr.Length / 2];
            }

            int[] mediansOfArray = new int[(arr.Length + 4) / 5];
            int[] tempArray = new int[5];

            for (int i = 0, k = 0; i < arr.Length; i += 5, k++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (arr.Length - i < 5)
                    {
                        tempArray = arr.Skip(i).ToArray();
                        break;
                    }
                    else
                    {
                        tempArray[j] = arr[j + i];
                    }

                    Array.Sort(tempArray);
                    mediansOfArray[k] = tempArray[tempArray.Length / 2];
                }
            }
            return TheMedianFromArray(mediansOfArray);
        }

        private static int Partitioning(int[] arr, int pivot)
        {
            int temp = 0;
            int i;

            for (i = 0; i < arr.Length; i++)
            {
                if (arr[i] == pivot)
                {
                    temp = arr[i];
                    arr[i] = arr[0];
                    arr[0] = temp;
                    break;
                }
            }

            i = 0;
            for (int j = 1; j < arr.Length; j++)
            {
                if (arr[0] > arr[j])
                {
                    i++;
                    temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                }
            }

            temp = arr[i];
            arr[i] = arr[0];
            arr[0] = temp;

            return i;
        }
    }
}
