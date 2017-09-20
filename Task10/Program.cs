using System;
using System.Collections.Generic;
using System.IO;

using static System.Console;

namespace Task10
{
    class Program
    {
        static int Read(string message, string errorMessage = "", Func<int,bool> criteria = null)
        {
            {
                if (errorMessage == "")
                    errorMessage = message;
                if (criteria == null)
                    criteria = _ => true;
            }
            int num = 0;
            WriteLine(message);
            while (!int.TryParse(ReadLine(), out num) || !criteria(num))
                WriteLine(errorMessage);
            return num;
        }

        public static Tree<int> tree;

        static void Main(string[] args)
        {
            int levels = Read("Введите количество уровней", "Количество уровней должно быть больше 0", num => num >= 0);
            int maxNodes = Read("Введите максимальное количество элементов", "Количество элементов должно быть больше 0", num => num >= 0);

            var root = new Tree<int>();
            tree = root;
            root.randomise(levels - 1, maxNodes, 35);

            WriteLine();
            //root.WriteToFile("out.txt");
            root.Write();

            int[] treeArray = root.BreadthFirst(out int countedLevels, out int[] levelsNodeCount);

            WriteLine();
            WriteLine("Проход дерева в ширину");
            WriteLine(string.Join(" ", treeArray));
            WriteLine();
            WriteLine("Количество ээлементов на уровнях");
            WriteLine(string.Join(" ", levelsNodeCount));
            WriteLine();
            WriteLine("Вычисленная высота равна {0}", countedLevels);

            ReadKey(true);
        }
    }
}