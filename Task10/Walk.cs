namespace Task10
{
    class Walk <T>
    {
        private T[] queue = new T[0];
        private int levels = 0;
        public int[] levelsNodeCount = null;

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
                    var newQueue = new T[index + 1];
                    for (int i = 0; i < Length; i++)
                        newQueue[i] = queue[i];
                    newQueue[index] = value;
                    queue = newQueue;
                }
            }
        }

        private void Add(T item)
        {
            this[Length] = item;
        }
        private void AddRange(T[] items)
        {
            foreach (T item in items)
                Add(item);
        }
        private void BreadthFirst(Tree<T> tree, int i)
        {
            if (i == 0)
            {
                foreach (Tree<T> node in tree.Nodes)
                    Add(node.data);
                return;
            }

            for (int j = 0; j < tree.Length; j++)
                BreadthFirst(tree[j], i - 1);
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
            return result + 1;
        }
        private int levelNodeCount(Tree<T> tree, int i)
        {
            if (i == 0)
                return tree.Length;
            int result = 0;
            for (int j = 0; j < tree.Length; j++)
                result += levelNodeCount(tree[j],i - 1);
            return result;
        }

        public Walk(Tree<T> tree)
        {
            levels = CountLevels(tree);

            Add(tree.data);
            for (int i = 0; i < tree.Length + 1; i++)
                BreadthFirst(tree, i);

            levelsNodeCount = new int[levels];
            levelsNodeCount[0] = 1;
            for (int i = 1; i < levels; i++)
                levelsNodeCount[i] = levelNodeCount(tree, i-1);
        }
    }
}