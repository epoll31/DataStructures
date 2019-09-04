using System;
using System.Collections.Generic;
using System.Text;

namespace AvlTree
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; internal set; }
        public int Height
        {
            get
            {
                if (LeftChild == null && RightChild == null)
                {
                    return 1;
                }
                else if (LeftChild != null && RightChild == null)
                {
                    return LeftChild.Height + 1;
                }
                else if (LeftChild == null && RightChild != null)
                {
                    return RightChild.Height + 1;
                }
                else
                {
                    return (LeftChild.Height > RightChild.Height ? LeftChild.Height : RightChild.Height) + 1;
                }
            }
        }
        public int Balance
        {
            get
            {
                if (LeftChild == null && RightChild == null)
                {
                    return 0;
                }
                else if (LeftChild != null && RightChild == null)
                {
                    return -LeftChild.Height;
                }
                else if (LeftChild == null && RightChild != null)
                {
                    return RightChild.Height;
                }
                else
                {
                    return RightChild.Height - LeftChild.Height;
                }
            }
        }
        public bool IsLeftChild => Parent.LeftChild == this;

        public Node<T> Parent { get; internal set; }
        public Node<T> LeftChild { get; internal set; }
        public Node<T> RightChild { get; internal set; }

        public Node(T value)
        {
            Value = value;
        }
        public Node(T value, Node<T> parent)
        {
            Value = value;
            Parent = parent;
        }
    }
}
