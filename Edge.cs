using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    class Edge
    {
        public int weight { get; set; }
        public Vertex target { get; set; }
        public Vertex source { get; set; }

        public Edge(Vertex source, Vertex target, int weight)
        {
            this.source = source;
            this.weight = weight;
            this.target = target;
        }
    
    }

}
