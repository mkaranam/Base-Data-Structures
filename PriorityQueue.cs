using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    class PriorityQueue<T>
    {
        public int Size { get; set; }
        public data<T>[] queue { get; set; }

        public void put(T data, int priority)
        {
            if (queue == null)
            {
                queue = new data<T>[10];    //default size 10
            }
            data<T> d = new data<T>(data, priority);
            queue[Size++] = d;
            if (Size == queue.Length)
            {
                data<T>[] q1 = new data<T>[Size*2];
                queue.CopyTo(q1, 0);
                queue = q1;
            }
            for (int i = (Size-1) / 2; i >= 0; i--) maxHeapify(i);
        }

        public T getNext()
        {
            T data = queue[0].d;
            swap(Size - 1, 0);
            Size--;
            maxHeapify(0);
            if (Size < queue.Length / 4)
            {
                data<T>[] q1 = new data<T>[queue.Length / 2];
                for (int i = 0; i < Size; i++) q1[i] = queue[i];
                queue = q1;
            }
            return data;
        }

        private void maxHeapify(int n)
        {
            int left = 2 * n + 1;
            int right = 2 * n + 2;
            if (queue == null || Size == 0) return;
            if (left < Size && right < Size)
            {
                if (queue[n].priority > queue[left].priority && queue[n].priority > queue[right].priority) return;

                if (queue[left].priority <= queue[right].priority)
                {
                    swap(n, right);
                    maxHeapify( right);
                }
                else
                {
                    swap(n, left);
                    maxHeapify(left);
                }
            }
            if (left < Size && queue[n].priority <= queue[left].priority)
            {
                swap(left, n);
                maxHeapify( left);
            }
            if (right < Size && queue[n].priority <= queue[right].priority)
            {
                swap(right, n);
                maxHeapify(right);
            }
        }

        private void swap(int i, int j){
            data<T> tmp = queue[i];
            queue[i] = queue[j];
            queue[j] = tmp;
        }

        public String toString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<Size;i++){
                sb.Append(queue[i].d +" ");
            }
            return sb.ToString();
        }
    }

    class data<T>
    {
        public T d { get; set; }
        public int priority { get; set; }

        public data(T data, int priority)
        {
            this.d = data;
            this.priority = priority;
        }
    }
}
