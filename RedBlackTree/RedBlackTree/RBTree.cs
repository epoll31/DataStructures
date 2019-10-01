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

        public Node<T> GetNode(T value)
        {
            Node<T> currentNode = Head;
            while (currentNode != null)
            {
                if (value.CompareTo(currentNode.Value) == 0)
                {
                    return currentNode;
                }
                else if (value.CompareTo(currentNode.Value) < 0)
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (value.CompareTo(currentNode.Value) > 0)
                {
                    currentNode = currentNode.RightChild;
                }
            }
            return null;
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
                if (!IsRed(currentNode) && IsRed(currentNode.LeftChild) && IsRed(currentNode.RightChild))
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

        public void Remove(T value)
        {
            if (Head != null)
            {
                Head = Remove(Head, value);
                if (Head != null)
                {
                    Head.IsRed = false;
                }
            }
        }

        private Node<T> Remove(Node<T> currentNode, T value)
        {
            if (value.CompareTo(currentNode.Value) < 0) //continue to the left
            {
                if (currentNode.LeftChild != null)//does left exist
                {
                    if (!IsRed(currentNode.LeftChild) && !IsRed(currentNode.LeftChild.LeftChild))
                    {
                        currentNode = MoveRedLeft(currentNode);
                    }
                    currentNode.LeftChild = Remove(currentNode.LeftChild, value);
                }
            }
            else
            {
                if (IsRed(currentNode.LeftChild))
                {
                    currentNode = RotateRight(currentNode);
                }

                if (value.CompareTo(currentNode.Value) == 0 && currentNode.RightChild == null)
                {
                    //removes leaf nodes
                    Count--;
                    return null;
                }

                if (currentNode.RightChild != null)//does right exist
                {
                    if (!IsRed(currentNode.RightChild) && !IsRed(currentNode.RightChild.LeftChild))
                    {
                        currentNode = MoveRedRight(currentNode);
                    }

                    if (value.CompareTo(currentNode.Value) == 0)
                    {
                        Node<T> minimum = currentNode.RightChild;
                        while (minimum.LeftChild != null)
                        {
                            minimum = minimum.LeftChild;
                        }
                        currentNode.Value = minimum.Value;
                        currentNode.RightChild = Remove(currentNode.RightChild, minimum.Value);
                    }
                    else
                    {
                        currentNode.RightChild = Remove(currentNode.RightChild, value);
                    }
                }
            }

            return FixUp(currentNode);
        }

        private void Balance(Node<T> currentNode)
        {
            while (currentNode != null)
            {
                if (IsRed(currentNode.RightChild))
                {
                    currentNode = RotateLeft(currentNode);
                }
                if (IsRed(currentNode.LeftChild) && IsRed(currentNode.LeftChild.LeftChild))
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
            if (!IsRed(node) && IsRed(node.LeftChild) && IsRed(node.RightChild))
            {
                node.IsRed = true;
                node.LeftChild.IsRed = false;
                node.RightChild.IsRed = false;
            }
        }

        private Node<T> MoveRedLeft(Node<T> node)
        {
            FlipColor(node);

            if (IsRed(node.RightChild.LeftChild))
            {
                node.RightChild = RotateRight(node.RightChild);
                node = RotateLeft(node);
            }

            FlipColor(node);

            if (IsRed(node.RightChild.RightChild))
            {
                node.RightChild = RotateLeft(node.RightChild);
            }

            return node;
        }

        private Node<T> MoveRedRight(Node<T> node)
        {
            FlipColor(node);

            if (IsRed(node.LeftChild.LeftChild))
            {
                node = RotateRight(node);
                FlipColor(node);
            }

            return node;
        }

        private Node<T> FixUp(Node<T> currentNode)
        {
            if (IsRed(currentNode.RightChild))
            {
                currentNode = RotateLeft(currentNode);
            }

            if (IsRed(currentNode.LeftChild) && IsRed(currentNode.LeftChild.LeftChild))
            {
                currentNode = RotateRight(currentNode);
            }

            if (IsRed(currentNode.LeftChild) && IsRed(currentNode.RightChild))
            {
                FlipColor(currentNode);
            }

            if (currentNode.LeftChild != null && IsRed(currentNode.LeftChild.RightChild) && !IsRed(currentNode.LeftChild.LeftChild))
            {
                currentNode.LeftChild = RotateRight(currentNode.LeftChild);

                if (IsRed(currentNode.LeftChild))
                {
                    currentNode = RotateRight(currentNode);
                }
            }

            return currentNode;
        }

        private bool IsRed(Node<T> node)
        {
            return node != null && node.IsRed;
        }
    }
}
