using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            RBTree<int> tree = new RBTree<int>();
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
        }
    }
}
