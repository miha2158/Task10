using System;
using System.IO;

namespace Task10
{
    class Tree <T>
    {
        public T data = default(T);
        private Tree<T>[] nodes = new Tree<T>[0];

        public int Length => nodes.Length;
        public Tree<T>[] Nodes => nodes;
        public Tree<T> this[int index]
        {
            get { return nodes[index]; }
            set
            {
                if (index < Length)
                    nodes[index] = value;
                else if (index >= Length)
                {
                    var newNodes = new Tree<T>[index + 1];
                    for (int j = 0; j < Length; j++)
                        newNodes[j] = nodes[j];
                    newNodes[index] = value;
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
            if (data.Length == 0)
                return;
            this[nodes.Length + data.Length - 1] = null;
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

        public static Random R = new Random();
        public void randomise(int levels, int maxNodes, int maxNum)
        {
            try
            {
                data = (T)Convert.ChangeType(R.Next(maxNum), typeof (T));
            }
            catch (Exception) { }

            if (levels == 0)
                nodes = new Tree<T>[0];
            else
                nodes = new Tree<T>[R.Next(maxNodes)];

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new Tree<T>();
                nodes[i].randomise(levels - 1, maxNodes, maxNum);
            }
        }

        public void Write()
        {
            Write("", true);
        }
        private void Write(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(data);

            for (int i = 0; i < Nodes.Length; i++)
                Nodes[i].Write(indent, i == Nodes.Length - 1);
        }
        public void WriteToFile(string filename)
        {
            using (var fs = new FileStream(filename, FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    WriteToFile("", true, sw);
                }
            }
        }
        private void WriteToFile(string indent, bool last, StreamWriter sw)
        {
            sw.Write(indent);
            if (last)
            {
                sw.Write("\\-");
                indent += "  ";
            }
            else
            {
                sw.Write("|-");
                indent += "| ";
            }
            sw.WriteLine(data);

            for (int i = 0; i < Nodes.Length; i++)
                Nodes[i].WriteToFile(indent, i == Nodes.Length - 1, sw);
        }
    }
}