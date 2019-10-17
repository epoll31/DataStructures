using System;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    public class Node<T> where T : IComparable
    {
        public Node<T> this[int index]
        {
            get
            {
                return neighbors[index];
            }
            set
            {
                neighbors[index] = value;
            }
        }

        private Node<T>[] neighbors;
        public int Height => neighbors.Length;

        public T Value { get; private set; }

        public Node(T value, int Height)
        {
            Value = value;
            neighbors = new Node<T>[Height];
        }
    }
}
