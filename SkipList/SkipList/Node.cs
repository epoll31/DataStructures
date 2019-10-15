using System;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    public class Node<T> where T : IComparable
    {
        private List<Node<T>> neighbors;
        public int Height => neighbors.Count;

        public Node(int Height)
        {
            neighbors = new List<Node<T>>(Height);
        }

        
    }
}
