using System;

namespace AvlTree
{
    class Program
    {
        static void Main(string[] args)
        {
            AvlTree<int> tree = new AvlTree<int>();

            tree.Add(30);
            tree.Add(91);
            tree.Add(19);
            tree.Add(76);
            tree.Add(40);
            tree.Add(71);
            tree.Add(8);
            tree.Add(99);
            tree.Add(90);
            tree.Add(21);

            tree.Remove(21);
            tree.Remove(30);
            tree.Remove(99);
            tree.Remove(71);
            tree.Remove(40);
            tree.Remove(8);
            tree.Remove(91);
            tree.Remove(19);
            tree.Remove(76);
            tree.Remove(90);
            ;
        }
    }
}
