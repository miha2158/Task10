namespace Task10
{
    class Tree<T>
    {
        public T data = default(T);
        private Tree<T>[] nodes = new Tree<T>[0];

        public int Length => nodes.Length;
        public Tree<T> this[int i]
        {
            get
            {
                return nodes[i];
            }
            set
            {
                if (i < Length)
                    nodes[i] = value;
                else if (i == Length)
                {
                    var newNodes = new Tree<T>[Length + 1];
                    for (int j = 0; j < Length; j++)
                        newNodes[j] = nodes[j];
                    nodes = newNodes;
                }
            }
        }

        public Tree()
        {
            
        }
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
            nodes[Length] = new Tree<T>(data);
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








    }
}
