using System;
using System.Collections.Generic;
using System.Text;

namespace RedBlackTree
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; internal set; }

        public Node<T> Parent { get; internal set; }
        public Node<T> LeftChild { get; internal set; }
        public Node<T> RightChild { get; internal set; }
        public bool IsRed { get; internal set; }
        public bool IsLeftChild => Parent.LeftChild == this;
        public bool IsLeafNode => LeftChild == null && RightChild == null;

        public Node(T value)
        {
            Value = value;
            IsRed = true;
        }

        public Node(T value, Node<T> parent)
        {
            Value = value;
            Parent = parent;
            IsRed = true;
        }
    }
}
