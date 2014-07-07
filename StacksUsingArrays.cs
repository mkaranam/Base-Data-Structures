using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    public class StacksUsingArrays<T>
    {
        T[] st { get; set; }
        int Top;
        public int Count { get; set; }
        private int Size;

        public StacksUsingArrays() 
        { 
            Top = -1;
            st = new T[10];
            Size = 10;
            Count = 0;
            for (int i = 0; i < 10; i++) st[i] = default(T);
        }

        public StacksUsingArrays(int Size)
        {
            Top = -1;
            st = new T[Size];
            this.Size = Size;
            Count = 0;
            for (int i = 0; i < Size; i++) st[i] = default(T);
        }

        public T peek()
        {
            if (Top < 0) throw new System.NullReferenceException();
            return st[Top];
        }

        public T pop()
        {
            if (Top < 0) throw new System.NullReferenceException();
            T data = st[Top];
            Top--;
            Count--;
            return data;
        }

        public void push(T data)
        {
            Top++;
            if (Top == Size)
            {
                Top--;
                throw new System.OverflowException();
            }
            Count++;
            st[Top] = data;
        }

        override public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= Top;i++ )
            {
                sb.Append(st[i] + " ");
            }
            return sb.ToString();
        }

        public T ElementAt(int k)
        {
            if (Count == 0) throw new System.NullReferenceException();
            if (k < 0 || k > Count) throw new System.ArgumentOutOfRangeException();
            return st[k - 1];
            
        }
    }
}
