using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeksForGeeks
{
    class UnionFind
    {
        public int Count { get; set; }
        public int[] parent;
        public int[] size;

        public UnionFind(int Cnt)
        {
            Count = Cnt;
            parent = new int[Count];
            size = new int[Count];

            for (int i = 0; i < Count; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }

        public int find(int p)
        {
            if (parent[p] == p)
            {
                return p;
            }
            else return find(parent[p]);
        }

        public void merge(int p, int q)
        {
            int r1 = find(p);
            int r2 = find(q);
            if (r1 == r2) return;
            if (size[r1] >= size[r2])
            {
                parent[r2] = r1;
                size[r1] += size[r2];
            }
            else
            {
                parent[r1] = r2;
                size[r2] += size[r1];
            }
        }

        public bool inSameComp(int p, int q)
        {
            return (find(p) == find(q));
        }
    
    }
}
