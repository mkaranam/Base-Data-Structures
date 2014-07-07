using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    public class LinkList<T>
    {
        public Node<T> head { get; set; }
        public Node<T> tail { get; set; }
        public int Size { get; set; }

        public void putLast(T data)
        {
            Node<T> n = new Node<T>();
            n.data = data;
            if (head == null) head = tail = n;
            else
            {
                tail.next = n;
                tail = tail.next;
            }
            Size++;
        }

        public void putFirst(T data)
        {
            Node<T> n = new Node<T>();
            n.data = data;
            if (head == null) head = tail = n;
            else
            {
                n.next = head;
                head = n;
            }
            Size++;
        }

        public void put(T data, int target)
        {
            if (target > Size) return;
            Node<T> n = new Node<T>();
            n.data = data;
            Node<T> cur = head;
            for (int i = 1; i < target-1; i++) cur = cur.next;
            n.next = cur.next;
            cur.next = n;
            Size++;
        }

        public T removeFirst()
        {
            T data = head.data;
            head = head.next;
            Size--;
            return data;
        }

        public T removeLast()
        {
            T data;
            if(Size == 1) { 
                data = head.data;
                head = null;
                Size--;
                return data;
            }
            Node<T> n = head;
            while (n.next.next != null) n = n.next;
            data = n.next.data;
            n.next = null;
            Size--;
            return data;
        }

        public T remove(int target)
        {
            Node<T> cur = head;
            for (int i = 1; i < target-1; i++) cur = cur.next;
            T data = cur.next.data;
            cur.next = cur.next.next;
            Size--;
            return data;
        }

        public String toString()
        {
            Node<T> n = head;
            StringBuilder sb = new StringBuilder();
            while (n != null)
            {
                sb.Append(n.data + " ");
                n = n.next;
            }
            return sb.ToString();
        }

        public Node<T> findK(int k)
        {
            if(k < 0 || k > Size) return null;
            Node<T> current = head;
            for (int i = 0; i < k - 1; i++)
            {
                current = current.next;
            }
            return current;
        }
    }
}
