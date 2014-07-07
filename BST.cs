using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeeksForGeeks
{
    class BST
    {
        private BSTNode root;
        public int size { get; set; }

        public BST()
        {
            root = new BSTNode();
        }

        public void put(int d)
        {
            if (size == 0)
            {
                root.data = d;
                size++;
                return;
            }
            put(root, d);
        }

        private void put(BSTNode n, int d)
        {
            if (n == null) return;
            if (d <= n.data)
            {
                //Use left child
                if (n.leftChild == null)
                {
                    BSTNode b = new BSTNode();
                    b.data = d;
                    n.leftChild = b;
                    b.parent = n;
                    size++;
                    return;
                }
                else
                {
                    put(n.leftChild, d);
                }
            }
            else
            {
                //Use left child
                if (n.rightChild == null)
                {
                    BSTNode b = new BSTNode();
                    b.data = d;
                    n.rightChild = b;
                    b.parent = n;
                    size++;
                    return;
                }
                else
                {
                    put(n.rightChild, d);
                }
            }
        }

        public bool remove(int d)
        {
            BSTNode n = find(d);
            if (n == null) return false;
            BSTNode rN = getLeaf(n.rightChild, true);
            if (rN == null) n = null;
            else
            {
                n.data = rN.data;
                rN = null;
            }
            size--;
            return true;
        }

        public BSTNode find(int d)
        {
            return find(root, d);
        }

        private BSTNode find(BSTNode n, int d){
            if (n == null) return null;
            if (n.data == d) return n;
            if (d < n.data) return find(n.leftChild, d);
            return find(n.rightChild, d);
        }

        private BSTNode getLeaf(BSTNode n,Boolean isleft)
        {
            if (n == null) return null;
            if (isleft)
            {
                if (n.leftChild == null) return n;
                return getLeaf(n.leftChild,isleft);
            }
            else
            {
                if (n.rightChild == null) return n;
                return getLeaf(n.rightChild, isleft);
            }
        }

        public String inOrder()
        {
            StringBuilder sb = new StringBuilder();
            inOrder(root, sb);
            sb.Remove(sb.Length-2,2);
            return sb.ToString();
        }
        private void inOrder(BSTNode n, StringBuilder sb)
        {
            if (n == null) return;
            inOrder(n.leftChild, sb);
            sb.Append(n.data + ", ");
            inOrder(n.rightChild, sb);
        }

        public String preOrder()
        {
            StringBuilder sb = new StringBuilder();
            preOrder(root, sb);
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }
        private void preOrder(BSTNode n, StringBuilder sb)
        {
            if (n == null) return;
            sb.Append(n.data + ", ");
            preOrder(n.leftChild, sb);
            preOrder(n.rightChild, sb);
        }

        public String postOrder()
        {
            StringBuilder sb = new StringBuilder();
            postOrder(root, sb);
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }
        private void postOrder(BSTNode n, StringBuilder sb)
        {
            if (n == null) return;
            postOrder(n.leftChild, sb);
            postOrder(n.rightChild, sb);
            sb.Append(n.data + ", ");
        }

        public String BFS()
        {
            Queue<BSTNode> q = new Queue<BSTNode>();
            q.enQueue(root);
            StringBuilder sb = new StringBuilder();
            while(q.Count != 0){
                BSTNode n = q.deQueue();
                sb.Append(n.data + ", ");
                if(n.leftChild!=null) q.enQueue(n.leftChild);
                if(n.rightChild!=null) q.enQueue(n.rightChild);
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        public String DFS()
        {
            StringBuilder sb = new StringBuilder();
            DFS(root, sb);
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }
        private void DFS(BSTNode n, StringBuilder sb)
        {
            if (n == null) return;
            DFS(n.leftChild, sb);
            sb.Append(n.data + ", ");
            DFS(n.rightChild, sb);
        }

        public bool isBalnced()
        {
            int height=0;
            if (chkBalanced(root, ref height))
            {
                Console.WriteLine("Tree is balanced");
                return true;
            }
            Console.WriteLine("Tree is not balanced");
            return false;
        }
        private bool chkBalanced(BSTNode node, ref int height)
        {
            if (node == null) return true;
            if (node.leftChild == null && node.rightChild == null)
            {
                height = 1;
                return true;
            }
            int leftHeight, rightHeight;
            leftHeight = rightHeight = 0;
            bool chkleft = chkBalanced(node.leftChild, ref leftHeight);
            if (!chkleft) return false;
            bool chkright = chkBalanced(node.rightChild, ref rightHeight);
            if (!chkright) return false;
            if (Math.Abs(leftHeight - rightHeight) > 1) return false;
            height = Math.Max(leftHeight, rightHeight) + 1;
            return true;
        }

        public static BST createTree(int[] input)
        {
            BST tree = new BST();
            CT(tree, input, 0, input.Length-1);
            return tree;
        }

        private static void CT(BST tree, int[] input, int low, int high)
        {
            if (high < low) return;
            if (low == high)
            {
                tree.put(input[low]);
                return;
            }
            int middle = (high + low) / 2;
            tree.put(input[middle]);
            CT(tree, input, low, middle - 1);
            CT(tree, input, middle + 1, high);
        }

        public List<List<BSTNode>> LevelOrder()
        {
            List<List<BSTNode>> levels = new List<List<BSTNode>>();
            List<BSTNode> cur = new List<BSTNode>();
            levels.Add(cur);
            cur.Add(this.root);
            while (cur.Count > 0)
            {
                List<BSTNode> newlevel = new List<BSTNode>();
                foreach (BSTNode node in cur)
                {
                    if(node.leftChild != null) newlevel.Add(node.leftChild);
                    if(node.rightChild != null) newlevel.Add(node.rightChild);
                }
                levels.Add(newlevel);
                cur = newlevel;
            }
            return levels;
        }

        public bool isBinarySearchTree()
        {
            if (iSBST(root, -1, true))
            {
                Console.WriteLine("Is a BST");
                return true;
            }
            Console.WriteLine("Is not a BST");
            return false;
        }

        private bool iSBST(BSTNode node, int val, bool isMax)
        {
            if (node == null) return true;
            if (node.leftChild == null && node.rightChild == null) return true;

            if (node.leftChild!=null && node.data < node.leftChild.data) return false;
            else if (node.rightChild!=null && node.data > node.rightChild.data) return false;

            if (isMax)
            {
                if (val > 0 && node.rightChild != null && node.rightChild.data > val) return false;
            }
            else
            {
                if (val > 0 && node.leftChild != null && node.leftChild.data < val) return false;
            }

            return iSBST(node.leftChild, node.data, true) && iSBST(node.rightChild, node.data, false);

        }

        public void putRandom(int d)
        {
            if (size == 0)
            {
                root.data = d;
                size++;
                return;
            }
            putRandom(root, d);
        }

        private void putRandom(BSTNode n, int d)
        {
            if (n == null) return;
            if (size%2 == 1)
            {
                //Use left child
                if (n.leftChild == null)
                {
                    BSTNode b = new BSTNode();
                    b.data = d;
                    n.leftChild = b;
                    size++;
                    return;
                }
                else
                {
                    putRandom(n.leftChild, d);
                }
            }
            else
            {
                //Use left child
                if (n.rightChild == null)
                {
                    BSTNode b = new BSTNode();
                    b.data = d;
                    n.rightChild = b;
                    size++;
                    return;
                }
                else
                {
                    putRandom(n.rightChild, d);
                }
            }
        }

        public void nextSuccessor(int d)
        {
            BSTNode node = find(d);
            node = nextSuccessor(node);
            if (node != null) Console.WriteLine("Next successor for {0} is {1}", d, node.data);
            else Console.WriteLine("This is the last node");
         }

        private BSTNode nextSuccessor(BSTNode node)
        {
            BSTNode next = null;
            if (node == null) return null;
            //If has right child, return the left-most node in the right sub-tree
            if (node.rightChild != null) return getLeaf(node.rightChild, true);
            else if (node.parent != null)
            {
                if (node.parent.rightChild != node)
                {
                    return node.parent;
                }
                while (node.parent==null || node.parent.rightChild == node)
                {
                    if (node.parent == null) return null;
                    node = node.parent;
                }
                next=node.parent;
            }
            return next;
        }

        public int commonAncestor(int x, int y)
        {
            BSTNode xNode = find(x);
            BSTNode yNode = find(y);

            if (xNode == yNode) return xNode.data;
            if (xNode == null || yNode == null) return -1;
            Stack<int> xPath = getPathtoRoot(xNode);
            Stack<int> yPath = getPathtoRoot(yNode);

            int anc = xPath.Pop();
            yPath.Pop();
            while (xPath.Count > 0 && yPath.Count > 0)
            {
                int xpop = xPath.Pop();
                int ypop = yPath.Pop();
                if (xpop != ypop) break;
                anc = xpop;
            }
            return anc;
        }

        private Stack<int> getPathtoRoot(BSTNode node)
        {
            if (node == null) return null;
            Stack<int> path = new Stack<int>();
            path.Push(node.data);
            while (node.parent != null)
            {
                node = node.parent;
                path.Push(node.data);
            }
            return path;
        }


        public void allPaths(int k)
        {
            Stack<BSTNode> path = new Stack<BSTNode>();
             allPaths(root, 0, path, k);
        }
        private void allPaths(BSTNode node, int sum,Stack<BSTNode> path,int k)
        {
            if (node == null) return;
            sum += node.data;
            path.Push(node);
            //Path ends here
            if (sum == k)
            {
                printPath(path);
            }

            //Is part of a path
            if (node.leftChild != null) allPaths(node.leftChild, sum, path, k);
            if (node.rightChild != null) allPaths(node.rightChild, sum, path, k);
            
            //If the start of a new path
            Stack<BSTNode> newPath = new Stack<BSTNode>();
            newPath.Push(node);
            if (node.leftChild != null) allPaths(node.leftChild, node.data, newPath, k);
            if (node.rightChild != null) allPaths(node.rightChild, node.data, newPath, k);

            //Done searching through, remove the node from it.
            path.Pop();
        }

        private void printPath(Stack<BSTNode> l)
        {
            Console.Write("Path: ");
            foreach (BSTNode n in l)
            {
                Console.Write(n.data + " ");
            }
            Console.WriteLine("\n");

        }

    }
}
