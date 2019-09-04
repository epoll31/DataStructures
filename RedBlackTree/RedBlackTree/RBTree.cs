using System;
using System.Collections.Generic;
using System.Text;

namespace RedBlackTree
{
    public class RBTree<T> where T : IComparable
    {
        public Node<T> Head { get; private set; }
        public int Count { get; private set; }

        public RBTree()
        {
            Head = null;
            Count = 0;
        }

        public void Add(T value)
        {
            if (Head == null)
            {
                Head = new Node<T>(value);
                Count++;
                Head.IsRed = false;
                return;
            }

            Node<T> currentNode = Head;
            while (currentNode != null)
            {
                if (!currentNode.IsRed && (currentNode.LeftChild != null && currentNode.LeftChild.IsRed) && (currentNode.RightChild != null && currentNode.RightChild.IsRed))
                {
                    FlipColor(currentNode);
                }
                if (value.CompareTo(currentNode.Value) <= 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = new Node<T>(value, currentNode);
                        Count++;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.LeftChild;
                    }
                }
                else
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = new Node<T>(value, currentNode);
                        Count++;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.RightChild;
                    }
                }
            }

            Balance(currentNode);
        }

        private void Balance(Node<T> currentNode)
        {
            while (currentNode != null)
            {
                if (currentNode.RightChild != null && currentNode.RightChild.IsRed)
                {
                    currentNode = RotateLeft(currentNode);
                }
                if ((currentNode.LeftChild != null && currentNode.LeftChild.IsRed) && (currentNode.LeftChild.LeftChild != null && currentNode.LeftChild.LeftChild.IsRed))
                {
                    currentNode = RotateRight(currentNode);
                }
                currentNode = currentNode.Parent;
            }

            Head.IsRed = false;
        }

        private Node<T> RotateLeft(Node<T> currentNode)
        {
            Node<T> newHead = currentNode.RightChild;

            if (currentNode.Parent == null)
            {
                Head = newHead;
                newHead.Parent = null;
            }
            else
            {
                newHead.Parent = currentNode.Parent;
                if (currentNode.IsLeftChild)
                {
                    currentNode.Parent.LeftChild = newHead;
                }
                else
                {
                    currentNode.Parent.RightChild = newHead;
                }
            }

            currentNode.RightChild = newHead.LeftChild;
            if (newHead.LeftChild != null)
            {
                newHead.LeftChild.Parent = currentNode;
            }
            newHead.LeftChild = currentNode;

            currentNode.Parent = newHead;

            newHead.LeftChild.IsRed = true;
            newHead.IsRed = false;

            return newHead;
        }

        private Node<T> RotateRight(Node<T> currentNode)
        {
            Node<T> newHead = currentNode.LeftChild;

            if (currentNode.Parent == null)
            {
                Head = newHead;
                newHead.Parent = null;
            }
            else
            {
                newHead.Parent = currentNode.Parent;
                if (currentNode.IsLeftChild)
                {
                    currentNode.Parent.LeftChild = newHead;
                }
                else
                {
                    currentNode.Parent.RightChild = newHead;
                }
            }

            currentNode.LeftChild = newHead.RightChild;
            if (newHead.RightChild != null)
            {
                newHead.RightChild.Parent = currentNode;
            }
            newHead.RightChild = currentNode;

            currentNode.Parent = newHead;

            newHead.RightChild.IsRed = true;
            newHead.IsRed = false;
            return newHead;
        }

        private void FlipColor(Node<T> node)
        {
            if (!node.IsRed && (node.LeftChild != null && node.LeftChild.IsRed) && (node.RightChild != null && node.RightChild.IsRed))
            {
                node.IsRed = true;
                node.LeftChild.IsRed = false;
                node.RightChild.IsRed = false;
            }
        }
    }
}
