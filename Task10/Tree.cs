namespace Task10
{
    class Walk<T>
    {
        private T[] queue = new T[0];
        private int levels = 0;

        public int Levels => levels;
        public T[] Queue => queue;

        private int Length => queue.Length;
        public T this[int index]
        {
            get { return queue[index]; }
            set
            {
                if (index < Length)
                    queue[index] = value;
                else if (index == Length)
                {
                    var newQueue = new T[Length + 1];
                    for (int i = 0; i < Length; i++)
                        newQueue[i] = queue[i];
                    newQueue[Length] = value;
                    queue = newQueue;
                }
            }
        }

        private void Add(T item)
        {
            this[Length] = item;
        }
        private void BreadthFirst(Tree<T> tree)
        {
            foreach (var node in tree.Nodes)
                BreadthFirst(node);
            Add(tree.data);
        }
        private int CountLevels(Tree<T> tree)
        {
            int result = 0;
            if (tree.Length == 0)
                return 1;

            foreach (var node in tree.Nodes)
            {
                int nodeLevels = CountLevels(node);
                if (nodeLevels > result)
                    result = nodeLevels;
            }
            return result;
        }

        public Walk(Tree<T> tree)
        {
            levels = CountLevels(tree);
            BreadthFirst(tree);
        }
    }

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
