namespace Task10
{
    class Tree<T>
    {
        public T data = default(T);
        private Tree<T>[] nodes = new Tree<T>[0];

        public int Length => nodes.Length;
        public Tree<T>[] Nodes => nodes;
        public Tree<T> this[int index]
        {
            get
            {
                return nodes[index];
            }
            set
            {
                if (index < Length)
                    nodes[index] = value;
                else if (index == Length)
                {
                    var newNodes = new Tree<T>[Length + 1];
                    for (int j = 0; j < Length; j++)
                        newNodes[j] = nodes[j];
                    nodes = newNodes;
                }
            }
        }

        public Tree() { }
        public Tree(T data)
        {
            this.data = data;
        }
        public Tree(Tree<T>[] nodes)
        {
            this.nodes = nodes;
        }
        public Tree(T data, Tree<T>[] nodes)
        {
            this.data = data;
            this.nodes = nodes;
        }

        public void AddNode(T data)
        {
            this[Length] = new Tree<T>(data);
        }
        public void AddNodes(T[] data)
        {
            foreach (T t in data)
                AddNode(t);
        }
        public void RemoveNode(int index)
        {
            var newNodes = new Tree<T>[Length - 1];
            for (int i = 0; i < Length; i++)
                if (i <= index)
                    newNodes[i] = nodes[i];
                else
                    newNodes[i - 1] = nodes[i];
        }

        public T[] BreadthFirst(out int levels)
        {
            var breadth = new Walk<T>(this);
            var result = breadth.Queue;
            levels = breadth.Levels;
            return result;
        }






    }
}
