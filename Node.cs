using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    public class Node<T>
    {
        public T data { get; set; }
        public T min { get; set; }
        public Node<T> next { get; set; }

    }
}
