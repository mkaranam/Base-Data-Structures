using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    class Sorting
    {
        #region Bubble Sort
        /// <summary>
        /// Bubble Sort
        /// </summary>
        /// <param name="inArray">Input array</param>
        public void bubblesort(params int[] inArray)
        {
            if (inArray == null || inArray.Length == 0) return;
            for (int i = 0; i < inArray.Length; i++)
            {
                for (int j = i + 1; j < inArray.Length; j++)
                {
                    if(inArray[i] > inArray[j]) swap(inArray, i, j);
                }
            }
            return;
        }
        #endregion 

        #region Quick Sort
        /// <summary>
        /// Quick Sort
        /// </summary>
        /// <param name="inArray">Input Array</param>
        public void quickSort(params int[] inArray)
        {
            if (inArray == null || inArray.Length == 0) return;
            qsort(inArray, 0, inArray.Length - 1);
        }

        private void qsort(int[] inArray, int low, int high)
        {
            if (high > low)
            {
                int tmp = partition(inArray, low, high);
                qsort(inArray, low, tmp-1);
                qsort(inArray, tmp + 1, high);
            }
        }

        private int partition(int[] inArray, int low, int high)
        {
            int pivot = high;
            int fHigh = low;
            for (int i = low; i < high; i++)
            {
                if (inArray[i] < inArray[pivot])    swap(inArray, i, fHigh++);
            }
            swap(inArray, pivot, fHigh);
            return fHigh;
        }
        #endregion

        #region Heap Sort
        /// <summary>
        /// Heap Sort
        /// </summary>
        /// <param name="inArray">Input Array</param>
        public void heapSort(params int[] inArray)
        {
            if (inArray == null || inArray.Length == 0) return;
            for (int i = inArray.Length/2; i >=0; i--)
            {
                maxHeapify(inArray,i,inArray.Length);
            }

            for (int i = 0; i < inArray.Length; i++)
            {
                //swap i with the last element
                swap(inArray, 0, (inArray.Length - 1 - i));
                maxHeapify(inArray, 0, inArray.Length - 1 - i);
            }
        }

        /// <summary>
        /// Max Heapify
        /// </summary>
        /// <param name="inArray">Input Array</param>
        /// <param name="k">Current node under consideration</param>
        /// <param name="length">Effective length of the array</param>
        private void maxHeapify(int[] inArray, int pivot,int length)
        {
            if (inArray == null || inArray.Length == 0) return;
            int left = 2*pivot+1;
            int right = 2*pivot+2;
            if (left < length && right < length)
            {
                if (inArray[pivot] > inArray[left] && inArray[pivot] > inArray[right]) return;

                if (inArray[left] <= inArray[right])
                {
                    swap(inArray,pivot, right);
                    maxHeapify(inArray,right,length);
                }
                else
                {
                    swap(inArray,pivot, left);
                    maxHeapify(inArray,left,length);
                }
            }
            if (left < length && inArray[pivot] <= inArray[left])
            {
                swap(inArray,left, pivot);
                maxHeapify(inArray,left,length);
            }
            if (right < length && inArray[pivot] <= inArray[right])
            {
                swap(inArray,right, pivot);
                maxHeapify(inArray,right,length);
            }
        }
        #endregion

        #region Insertion Sort
        /// <summary>
        /// Insertion Sort
        /// </summary>
        /// <param name="inArray">Input Array</param>
        public void insertionSort(params int[] inArray)
        {
            if (inArray == null || inArray.Length == 0) return;
            for (int i = 1; i < inArray.Length; i++)
            {
                int j = i;
                while (inArray[j] < inArray[j - 1])
                {
                    swap(inArray, j, j - 1);
                    j--;
                    if (j < 1) break;
                }
            }
        }
        #endregion

        #region Merge Sort
        /// <summary>
        /// Merge Sort
        /// </summary>
        /// <param name="inArray">Input Array</param>
        public void mergeSort(int[] inArray)
        {
            if (inArray == null || inArray.Length <= 1) return;
            int[] left = new int[inArray.Length / 2];
            int[] right = new int[inArray.Length / 2];
            Array.ConstrainedCopy(inArray, 0, left, 0, inArray.Length / 2-1);
            Array.ConstrainedCopy(inArray, (inArray.Length/2), right, 0, inArray.Length / 2);
            mergeSort(left);
            mergeSort(right);
            inArray = merge(left, right);
        }

        public int[] merge(int[] a, int[] b)
        {
            if (a == null || a.Length == 0) return b;
            if (b == null || b.Length == 0) return a;
            int[] c = new int[a.Length + b.Length];
            int cnt=0;
            int cnt1=0;
            int cnt2=0;
            while (cnt1 < a.Length && cnt2 < b.Length)
            {
                if (a[cnt1] <= b[cnt2]) c[cnt++] = a[cnt1++];
                else c[cnt++] = b[cnt2++];
            }

            if (cnt1 < a.Length)    while (cnt1 < a.Length) c[cnt++] = a[cnt1++];

            if (cnt2 < b.Length) while (cnt2 < b.Length) c[cnt++] = b[cnt2++];

            return c;
        } 
        #endregion

        #region Counting Sort
        /// <summary>
        /// Counting Sort
        /// </summary>
        /// <param name="inArray">Unsorted Input Array</param>
        /// <param name="k">Max element number</param>
        public int[] countingSort(int[] inArray,int k)
        {
            int[] b = new int[inArray.Length];
            int[] c = new int[k];

            for (int i = 0; i < k; i++) c[i] = 0;
            for (int i = 0; i < inArray.Length; i++) c[inArray[i]]++;
            for (int i = 1; i < k; i++) c[i] = c[i - 1] + c[i];
            for (int i = inArray.Length - 1; i >= 0; i--)
            {
                b[c[inArray[i]] - 1] = inArray[i];
                c[inArray[i]]--;
            }
            return b;
        }
        #endregion


        #region utility functions
        private void swap(int[] inArray, int i, int j)
        {
            int tmp = inArray[i];
            inArray[i] = inArray[j];
            inArray[j] = tmp;
        }
        #endregion 

    }

}
