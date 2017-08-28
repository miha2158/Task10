namespace Task10 {
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
}