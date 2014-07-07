using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    class Queue<T>
    {
        public Node<T> first {get; set;}
        public Node<T> last  {get; set;}
        public int Count { get; set; }

        public void enQueue(T data)
        {
            if (first == null)
            {
                first = new Node<T>();
                first.data = data;
                last = first;
            }
            else
            {
                Node<T> n = new Node<T>();
                n.data = data;
                last.next = n;
                last = last.next;
            }
            Count++;
        }

        public T peek()
        {
            return first.data;
        }

        public T deQueue()
        {
            T data = first.data;
            if(first.next != null) first = first.next;
            else first = last = null;
            Count--;
            return data;
        }

        public String toString()
        {
            Node<T> n = first;
            StringBuilder sb = new StringBuilder();
            while (n != null)
            {
                sb.Append(n.data + " ");
                n = n.next;
            }
            return sb.ToString();
        }

        internal void Enqueue(T data)
        {
            enQueue(data);
        }

        internal T Dequeue()
        {
            return deQueue();
        }
    }
}
