using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    class Graph
    {
        private enum Type {BFS=1,connectedComponents, findPath,Bipartite,DFS,findCycles,articulationVertex,topologicalSort,StronglyConencted,isStrongConn };
        private enum EdgeType { TREE=1,BACK,FORWARD,CROSS,UNCLASSFIED };
        public int Count { get; set; }
        public static bool isDirected { get; set; }
        Vertex[] vertices;
        private Stack<int> sOrder;
        private bool[] isDiscovered;
        private bool[] isProcessed;
        private int[] parent;
        private int[] color;
        private int[] entry_Time;
        private int[] exit_Time;
        private int[] reachable_Ancestor;
        private int[] low;
        private bool[] inTree;
        private int[] dist;
        private int connComp;
        private int[] scc;
        private UnionFind UF;
        private bool finished;
        private bool foundBack;
        private int isBip;
        public bool isBipartite { 
            get
            {
                if (isBip == 0)
                {
                    initSearch();
                    isBip = 2;

                    for (int i = 0; i < Count; i++)
                    {
                        if (isDiscovered[i] == false)
                        {
                            color[i] = 1;
                            StringBuilder sb = new StringBuilder();
                            BFS(i, sb, Type.Bipartite);
                        }
                    }
                }
                if (isBip == 1) return false;
                else return true;
            }
        }

        public void insertEdge(int i, int j, int weight)
        {
            if (i > Count || j > Count)
            {
                return;
            }
            vertices[i - 1].addEdge(vertices[i - 1], vertices[j - 1], weight);
            if (isDirected) return;
            vertices[j - 1].addEdge(vertices[j - 1], vertices[i - 1], weight);
        }

        public Graph()
        {
            Count = 10;
            isDirected = false;
            vertices = new Vertex[Count];  //Set default as 10
            for (int i = 0; i < Count; i++) vertices[i] = new Vertex("",(i+1));
        }

        public Graph(int Count, bool directed)
        {
            this.Count = Count;
            isDirected = directed;
            vertices = new Vertex[Count];
            for (int i = 0; i < Count; i++) vertices[i] = new Vertex("", (i + 1));
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Size: " + Count + "\n");
            for (int i = 0; i < Count; i++)
            {
                sb.Append("Vertex[" + vertices[i].ID + "]: " + vertices[i].Label + " has edges: ");
                foreach(Edge e in vertices[i].edges){
                    sb.Append(e.target.ID + "[" + e.weight +"] " );
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public String BFS()
        {
            initSearch();
            StringBuilder sb = new StringBuilder();
            return BFS(0,sb,Type.BFS);
        }

        private String BFS(int start, StringBuilder sb,Type t)
        {
            Queue<Vertex> q = new Queue<Vertex>();
            q.Enqueue(vertices[start]);
            isDiscovered[start] = true;
            while(q.Count != 0)
            {
                Vertex v = q.Dequeue();
                process_Vertex_early(sb, v,t);
                isProcessed[v.ID - 1] = true;
                foreach (Edge e in v.edges)
                {
                    if (isProcessed[e.target.ID - 1] == false || isDirected) process_Edge(e, v, t);
                    
                    if (isDiscovered[e.target.ID - 1] == false)
                    {
                        q.Enqueue(e.target);
                        isDiscovered[e.target.ID - 1] = true;
                        parent[e.target.ID - 1] = v.ID - 1;
                    }
                }
                process_Vertex_late(sb,v,t);
            }
            return sb.ToString();
        }

        public void connectedComp()
        {
            initSearch();
            int cnt = 0;
            Console.WriteLine("\n");
            for (int i = 0; i < Count; i++)
            {
                if (isDiscovered[i] == false)
                {
                    cnt++;
                    StringBuilder sb = new StringBuilder();
                    Console.WriteLine("Connected component [" +cnt + "]: " + BFS(i,sb,Type.connectedComponents));
                }
            }
        }

        private void initSearch()
        {
            isDiscovered = new bool[Count];
            isProcessed = new bool[Count];
            parent = new int[Count];
            color = new int[Count];
            entry_Time = new int[Count];
            exit_Time = new int[Count];
            reachable_Ancestor = new int[Count];
            low = new int[Count];
            scc = new int[Count];
            inTree = new bool[Count];
            dist = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                isDiscovered[i] = isProcessed[i] = inTree[i] = false;
                parent[i] = color[i] = scc[i] = -1;
                entry_Time[i] = exit_Time[i] = 0;
                reachable_Ancestor[i] = low[i] = i;
                dist[i] = Int32.MaxValue;
            }
            finished = foundBack = false;
            connComp = 0;
            UF = new UnionFind(Count);
        }

        private void process_Vertex_early(StringBuilder sb, Vertex v,Type t)
        {
            switch (t){
                case Type.BFS: 
                    sb.Append(v.ID + " ");
                    break;
                case Type.connectedComponents:
                    sb.Append(v.ID + " ");
                    break;
                case Type.StronglyConencted:
                    sOrder.Push(v.ID);
                    break;
                default:
                    break;
            }
        }

        private void process_Vertex_late(StringBuilder sb, Vertex v,Type t)
        {
            int internalID = v.ID - 1;
            switch (t){
                case Type.DFS:
                    sb.Append((internalID+1) + " ");
                    break;
                case Type.articulationVertex:
                    if (internalID == 0)
                    {
                        //check for root-articulation vertex
                        if (vertices[internalID].edges.Count > 1) sb.Append("Root articulation vertex: " + v.ID +"\n");
                        break;
                    }
                    else if (reachable_Ancestor[internalID] == internalID)
                    {
                        //Its parent is an articulation vertex
                        sb.Append("Parent articulation vertex: " + (parent[internalID]+1) + "\n"); 
                        //Its is a bridge cut node
                        if (vertices[internalID].edges.Count > 1) sb.Append("Parent articulation vertex: " + (v.ID) + "\n"); 
                    }   //Its parent is an articulation vertex
                    if (reachable_Ancestor[internalID] == parent[internalID] && parent[internalID] != 0) sb.Append("Parent articulation vertex: " + (parent[internalID]+1) + "\n");

                    if (entry_Time[reachable_Ancestor[internalID]] < entry_Time[reachable_Ancestor[parent[internalID]]])
                    {
                        reachable_Ancestor[parent[internalID]] = reachable_Ancestor[internalID];
                    }
                    break;
                case Type.topologicalSort:
                    sOrder.Push(v.ID);
                    break;
                case Type.StronglyConencted:
                    if (low[internalID] == internalID && foundBack)
                    {
                        //Found a strongly connected component
                        connComp++;
                        sb.Clear();
                        int ID;
                        scc[internalID] = connComp;
                        while ((ID = sOrder.Pop()) != internalID)
                        {
                            scc[ID - 1] = connComp;
                            sb.Append(ID +" ");
                            if (sOrder.Count == 0) break;
                        }
                        Console.WriteLine("Strongly connected component {0}: {1}", connComp, sb.ToString());
                    }
                    if (internalID == 0) break;
                    if (entry_Time[low[internalID]] < entry_Time[low[parent[internalID]]])
                    {
                        low[parent[internalID]] = low[internalID];
                    }
                    break;
                default:
                    break;
            }
        }

        private void process_Edge(Edge e, Vertex v, Type t)
        {
            switch (t)
            {
                case Type.BFS:
                    break;
                case Type.Bipartite:
                    if (isBip == 1) break;
                    if (color[v.ID - 1] == color[e.target.ID - 1])
                    {
                        isBip = 1;
                        Console.WriteLine("Graph is not bipartite due to the following vertices: {0} and {1}",v.ID, e.target.ID);
                        break;
                    }
                    else if (color[e.target.ID - 1] == 0)
                    {
                        color[e.target.ID - 1] = complement(color[v.ID - 1]);
                        break;
                    }
                    break;
                case Type.findCycles:
                    if (EdgeClassification(v.ID-1,e.target.ID-1)==EdgeType.BACK)
                    {
                        finished = true;
                        StringBuilder sb = new StringBuilder();
                        Console.WriteLine("Warning: Found a cycle from {0} to {1}", e.target.ID, v.ID);
                        FP(e.target.ID-1,v.ID-1,sb);
                        Console.WriteLine("Cycle: {0}",sb.ToString());
                    }
                    break;
                case Type.articulationVertex:
                    if (EdgeClassification(v.ID - 1, e.target.ID - 1) == EdgeType.BACK)
                    {
                        if (entry_Time[e.target.ID - 1] < entry_Time[reachable_Ancestor[v.ID - 1]])
                        {
                            reachable_Ancestor[v.ID - 1] = e.target.ID - 1;
                        }
                    }
                    break;
                case Type.topologicalSort:
                    if (EdgeClassification(v.ID - 1, e.target.ID - 1) == EdgeType.BACK)
                    {
                        sOrder.Clear();
                        finished = true;
                    }
                    break;
                case Type.StronglyConencted:
                    if (EdgeClassification(v.ID - 1, e.target.ID - 1) == EdgeType.BACK)
                    {
                        if (entry_Time[e.target.ID - 1] < entry_Time[low[v.ID - 1]])
                        {
                            low[v.ID - 1] = e.target.ID - 1;
                            foundBack = true;
                        }
                    }
                    else if (EdgeClassification(v.ID - 1, e.target.ID - 1) == EdgeType.CROSS)
                    {
                        if (scc[e.target.ID - 1] == -1)
                        {
                            if (entry_Time[e.target.ID - 1] < entry_Time[low[v.ID - 1]])
                            {
                                low[v.ID - 1] = e.target.ID - 1;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void findPath(int i, int j)
        {
            StringBuilder sb = new StringBuilder();
            initSearch();
            BFS(i - 1, sb, Type.findPath);
            Console.WriteLine("\n");
            if (isProcessed[j - 1] == false)
            {
                Console.WriteLine("Not in conencted components, no path exists.");
                return;
            }

            FP(i - 1, j - 1, sb);
            Console.WriteLine("Path: {0}",sb.ToString());
        }

        private void FP(int start, int end, StringBuilder sb)
        {
            if (start == end || end == -1)  sb.Append((start+1));
            else
            {
                FP(start, parent[end], sb);
                sb.Append(" " + (end + 1));
            }
        }

        private int complement(int i)
        {
            if (i == 1) return 2;
            else if (i == 2) return 1;
            else return 0;
        }

        public void printGraph()
        {
            Console.WriteLine("\n");
            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine("Vertex: {0}", (i + 1));
                vertices[i].printEdges();
            }
        }

        public String DFS()
        {
            initSearch();
            StringBuilder sb = new StringBuilder();
            DFS(vertices[0], sb,Type.DFS,0);
            return sb.ToString();   
        }

        private void DFS(Vertex v,StringBuilder sb,Type t,int Time)
        {
            isDiscovered[v.ID - 1] = true;
            Time = Time + 1;
            entry_Time[v.ID - 1] = Time;

            if (finished) return;

            process_Vertex_early(sb,v,t);

            foreach (Edge e in v.edges)
            {
                Vertex tar = e.target;
                if (!isDiscovered[tar.ID - 1])
                {
                    parent[tar.ID - 1] = v.ID - 1;
                    process_Edge(e,v,t);
                    DFS(tar, sb, t, Time);
                    if (finished) return;
                }
                if (!isProcessed[tar.ID - 1] || isDirected)
                {
                    process_Edge(e, v, t);
                }
                if (finished) return;
            }
            process_Vertex_late(sb, v, t);

            Time = Time + 1;
            exit_Time[v.ID - 1] = Time;
            isProcessed[v.ID - 1] = true;
        }

        public void findCycles()
        {
            initSearch();
            Console.WriteLine("\n");
            for (int i = 0; i < Count; i++)
            {
                if (isDiscovered[i] == false && !finished)
                {
                    StringBuilder sb = new StringBuilder();
                    DFS(vertices[i], sb, Type.findCycles, 0);

                    
                }
            }
            if (!finished) Console.WriteLine("No cycles found.");
        }

        public void getArticulationVertices()
        {
            StringBuilder sb = new StringBuilder();
            initSearch();
            Console.WriteLine("\n");
            for (int i = 0; i < Count; i++)
            {
                if (isDiscovered[i] == false && !finished)
                {
                    sb.Clear();
                    DFS(vertices[i], sb, Type.articulationVertex, 0);
                }
            }
            
            if (sb.Length > 0) Console.WriteLine("Articulation vertices are: {0}",sb.ToString());
            else Console.WriteLine("The graph doesn't have any articulation vertices...");
        }

        private EdgeType EdgeClassification(int x, int y)
        {
            if (parent[y] == x) return EdgeType.TREE;
            if (!isDirected)
            {
                if (parent[x] != y) return EdgeType.BACK;
            }
            else
            {
                if (isDiscovered[y] && !isProcessed[y]) return EdgeType.BACK;
                if (isProcessed[y] && (entry_Time[y] > entry_Time[x])) return EdgeType.FORWARD;
                if (isProcessed[y] && (entry_Time[y] < entry_Time[x])) return EdgeType.CROSS;
            }
            return EdgeType.UNCLASSFIED;

        }

        public void topologicalSort()
        {
            sOrder = new Stack<int>();
            initSearch();
            Console.WriteLine("\n");
            for (int i = 0; i < Count; i++)
            {
                if (isDiscovered[i] == false && !finished)
                {
                    DFS(vertices[i], null, Type.topologicalSort, 0);
                }
            }

            if (sOrder.Count > 0)
            {
                Console.WriteLine("Topological Sort stating at vertex 1 is: ");
                while (sOrder.Count > 0) Console.Write("{0} ",sOrder.Pop());
                
            }
            else Console.WriteLine("Warning: The graph is not a DAG.");
        }

        public void getStronglyConnectedComp()
        {
            sOrder = new Stack<int>();
            initSearch();
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("\n");
            for (int i = 0; i < Count; i++)
            {
                if (isDiscovered[i] == false && !finished)
                {
                    DFS(vertices[i], sb, Type.StronglyConencted, 0);
                }
            }
            if (connComp == 0) Console.WriteLine("There are no strongly connected components for the graph!");
        }

        public void isStronglyConnected()
        {
            initSearch();
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("\n");
            for (int i = 0; i < Count; i++) DFS(vertices[i], sb, Type.isStrongConn, 0);
            if (!areAllDisc())
            {
                Console.WriteLine("The graph is not strongly connected (cannot reach all vertices from 1)!");
                return;
            }
            Graph g = new Graph(Count,isDirected);
            reverseG(g);
            g.DFS();
            if (!g.areAllDisc())
            {
                Console.WriteLine("The graph is not strongly connected (not all vertices can reach 1)!");
                return;
            }
            Console.WriteLine("The graph is strongly connected!");
        }

        public bool areAllDisc()
        {
            for (int i = 0; i < Count; i++)
            {
                if (isDiscovered[i] == false)
                {
                    return false;
                }
            }
            return true;
        }

        private void reverseG(Graph g)
        {
            for (int i = 0; i < Count; i++)
            {
                foreach (Edge e in vertices[i].edges) g.insertEdge(e.target.ID, vertices[i].ID, 0);
            }
        }

        public void PRIM()
        {
            initSearch();
            int v,w,distance,totWeight;
            v = totWeight = 0;
            dist[v] = 0;
            StringBuilder sb = new StringBuilder();
            while (!inTree[v])
            {
                inTree[v] = true;
                totWeight += dist[v];
                sb.Append((v + 1) + "|" + dist[v] + ", ");
                foreach (Edge e in vertices[v].edges)
                {
                    w = e.weight;
                    if (dist[e.target.ID - 1] > w && !inTree[e.target.ID-1])
                    {
                        dist[e.target.ID - 1] = w;
                    }
                }
                distance = Int32.MaxValue;
                for (int i = 0; i < Count; i++)
                {
                    if (!inTree[i] && distance > dist[i])
                    {
                        v = i;
                        distance = dist[i];
                    }
                }
            }
            sb.Remove(sb.Length-2,2);
            Console.WriteLine("MSP using PRIM's algorithm: {{{0}}} with total weight of {1}", sb.ToString(), totWeight);

        }

        public void Kruskals()
        {
            initSearch();
            int totWeight=0;
            StringBuilder sb = new StringBuilder();
            PriorityQueue<Edge> pq = new PriorityQueue<Edge>();
            for (int i = 0; i < Count; i++)
            {
                foreach (Edge e in vertices[i].edges)
                {
                    pq.put(e, (e.weight*-1));
                }
            }
            while (pq.Size > 0)
            {
                Edge e = pq.getNext();
                if (!UF.inSameComp(e.source.ID - 1, e.target.ID - 1))
                {
                    UF.merge(e.source.ID - 1, e.target.ID - 1);
                    sb.Append(e.source.ID + "->" + e.target.ID + "(" + e.weight + "), ");
                    totWeight += e.weight;
                }
            }
            sb.Remove(sb.Length - 2, 2);
            Console.WriteLine("MSP using Kruskal's algorithm: {{{0}}} with total weight of {1}", sb.ToString(), totWeight);
        }

        public void ShortestPath(int source, int target)
        {
            initSearch();
            int v, w, distance, totWeight;
            v = totWeight = source-1;
            dist[v] = 0;
            StringBuilder sb = new StringBuilder();
            while (!inTree[v])
            {
                inTree[v] = true;
                foreach (Edge e in vertices[v].edges)
                {
                    w = e.weight;
                    if ((dist[e.target.ID - 1] > (w + dist[v])))
                    {
                        dist[e.target.ID - 1] = (w + dist[v]);
                        parent[e.target.ID - 1] = v;
                    }
                }
                distance = Int32.MaxValue;
                for (int i = 0; i < Count; i++)
                {
                    if (!inTree[i] && distance > dist[i])
                    {
                        v = i;
                        distance = dist[i];
                    }
                }
            }
            FP(source - 1, target - 1, sb);
            Console.WriteLine("Shortest path between {0} and {1} using Djikstra's algorithm: {{{2}}} with total weight of {3}", (source), (target),sb.ToString(), dist[target-1]);
        }
    
        public bool hasPath(int source, int target)
        {
            if (source > Count || source < 0) return false;
            if (target > Count || target < 0) return false;

            Queue<Vertex> q = new Queue<Vertex>();
            bool[] isDiscovered = new bool[Count];
            for (int i = 0; i < Count; i++)
            {
                isDiscovered[i] = false;
            }
            q.Enqueue(vertices[source-1]);
            isDiscovered[source-1] = true;
            while (q.Count > 0)
            {
                Vertex v = q.Dequeue();
                //Console.WriteLine(v.ID);
                foreach (Edge e in v.edges)
                {
                    //Console.WriteLine(v.ID +"|" + e.target.ID);
                    if (e.target.ID == target)
                    {
                        return true;
                    }
                    if (!isDiscovered[e.target.ID - 1])
                    {
                        q.Enqueue(e.target);
                        isDiscovered[e.target.ID - 1] = true;
                    }
                }
            }

            return false;
        }
    
    }
}
