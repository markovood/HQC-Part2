using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComparingSortAlgorithms
{
    public class SortAlgorithmComparison
    {
        public static void Main()
        {
            Stopwatch sw = new Stopwatch();

            IComparable[] intArrRandom = { 2, 8, 14, 25, -5, 0, 7, 11, 85, 16, -17 };
            IComparable[] intArrSorted = { -17, -5, 0, 2, 7, 8, 11, 14, 16, 25, 85 };
            IComparable[] intArrSortedReversed = { 85, 25, 16, 14, 11, 8, 7, 2, 0, -5, -17 };

            IComparable[] doubleArrRandom = { 2.1, 8.5, 14.0, 25.2, -5.5, 0.1, 7.3, 1.1, 85.5, 1.6, -1.71 };
            IComparable[] doubleArrSorted = { -5.5, -1.71, 0.1, 1.1, 1.6, 2.1, 7.3, 8.5, 14.0, 25.2, 85.5 };
            IComparable[] doubleArrSortedReversed = { 85.5, 25.2, 14.0, 8.5, 7.3, 2.1, 1.6, 1.1, 0.1, -1.71, -5.5 };

            IComparable[] strArrRandom = { "a", "x", "W", "A", "brei", "ua" };
            IComparable[] strArrSorted = { "a", "A", "brei", "ua", "W", "x" };
            IComparable[] strArrSortedReversed = { "x", "W", "ua", "brei", "A", "a" };

            sw.Start();
            InsertionSort(intArrRandom); // 0.00034ms
            //SelectionSort(intArrRandom); // 0.00042ms
            //Quicksort(intArrRandom, 0, intArrRandom.Length - 1); // 0.00047ms

            //InsertionSort(doubleArrRandom); // 0.00034ms
            //SelectionSort(doubleArrRandom); // 0.00045ms
            //Quicksort(doubleArrRandom, 0, doubleArrRandom.Length - 1); // 0.00047ms

            //InsertionSort(strArrRandom); // 0.00076ms
            //SelectionSort(strArrRandom); // 0.00085ms
            //Quicksort(strArrRandom, 0, strArrRandom.Length - 1); // 0.00090ms
            Console.WriteLine(sw.Elapsed);
            sw.Reset();


        }

        private static void InsertionSort(IComparable[] array)
        {
            IComparable temp;
            int j;
            for (int i = 1; i < array.Length; i++)
            {
                temp = array[i];
                j = i - 1;

                while (j >= 0 && array[j].CompareTo(temp) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = temp;
            }
        }

        private static void SelectionSort(IComparable[] array)
        {
            IComparable temp;
            int min;

            for (int i = 0; i < array.Length - 1; i++)
            {
                min = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j].CompareTo(array[min]) < 0)
                    {
                        min = j;
                    }
                }

                if (min != i)
                {
                    temp = array[i];
                    array[i] = array[min];
                    array[min] = temp;
                }
            }
        }

        private static void Quicksort(IComparable[] array, int left, int right)
        {
            int i = left, j = right;
            IComparable pivot = array[(left + right) / 2];

            while (i <= j)
            {
                while (array[i].CompareTo(pivot) < 0)
                {
                    i++;
                }

                while (array[j].CompareTo(pivot) > 0)
                {
                    j--;
                }

                if (i <= j)
                {
                    // Swap
                    IComparable tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;

                    i++;
                    j--;
                }
            }

            // Recursive calls
            if (left < j)
            {
                Quicksort(array, left, j);
            }

            if (i < right)
            {
                Quicksort(array, i, right);
            }
        }
    }
}
