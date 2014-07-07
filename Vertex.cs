using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    class Vertex
    {
        public List<Edge> edges { get; set; }
        public String Label { get; set; }
        public int ID { get; set; }

        public Vertex()
        {
            edges = new List<Edge>();
            Label = "";
            ID = -1;
        }

        public Vertex(String name,int ID)
        {
            edges = new List<Edge>();
            Label = name;
            this.ID = ID;
        }

        public void addEdge(Vertex source, Vertex target, int weight)
        {
            Edge e = new Edge(source, target,weight);
            edges.Add(e);
        }

        public void printEdges()
        {
            foreach (Edge e in edges)
            {
                Console.WriteLine("Edge to : {0}", e.target.ID);
            }
        }
    }
}
