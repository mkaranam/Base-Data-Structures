using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    public class Stack1<T>
    {
        public Node<T> Top { get; set; }
        public int Size { get; set; }

        public T peek()
        {
            return Top.data;
        }

        public T pop()
        {
            T data = Top.data;
            Top = Top.next;
            Size--;
            return data;
        }

        public void push(T data)
        {
            Node<T> n = new Node<T>();
            n.data = data;
            n.next = Top;
            Top = n;
            Size++;
        }

        public String toString()
        {
            Node<T> n = Top;
            StringBuilder sb = new StringBuilder();
            while (n != null)
            {
                sb.Append(n.data + " ");
                n = n.next;
            }
            return sb.ToString();
        }

    }
}
