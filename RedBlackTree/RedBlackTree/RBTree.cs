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

        public void Remove(T value)
        {
            if (Head == null)
            {
                return;
            }

            Node<T> currentNode = Head;
            while (currentNode != null)
            {
                if (value.CompareTo(currentNode.Value) < 0)
                {
                    if (currentNode.LeftChild.Is2Node)
                    {
                        MoveRedLeft(currentNode);
                    }

                    currentNode = currentNode.LeftChild;
                }
                else if (value.CompareTo(currentNode.Value) >= 0)
                {
                    if (currentNode.LeftChild.IsRed)
                    {
                        currentNode = RotateRight(currentNode/*.LeftChild??*/);
                    }

                    if (value.CompareTo(currentNode.Value) == 0)
                    {
                        if (currentNode.IsLeafNode)
                        {
                            if (currentNode.IsLeftChild)
                            {
                                currentNode.Parent.LeftChild = null;//removed
                            }
                            else
                            {
                                currentNode.Parent.RightChild = null;//removed
                            }
                            Count--;
                            break;
                        }
                        else
                        {
                            if (currentNode.RightChild.Is2Node)
                            {
                                MoveRedRight(currentNode);
                                currentNode = currentNode.RightChild;
                                continue;
                            }
                            else
                            {
                                if (currentNode.RightChild.Is2Node)
                                {
                                    MoveRedRight(currentNode);
                                }
                                currentNode = currentNode.RightChild;
                                continue;
                            }
                        }
                    }
                }
            }

            FixUp(currentNode.Parent);
        }

        private void RemoveSpecificNode(Node<T> nodeToRemove)
        {
            if (nodeToRemove.LeftChild == null && nodeToRemove.RightChild == null)
            {//no children
                if (nodeToRemove.Parent == null)
                {
                    Head = null;
                    Count--;
                    return;
                }
                else if (nodeToRemove.IsLeftChild)
                {
                    nodeToRemove.Parent.LeftChild = null;
                    Count--;
                }
                else
                {
                    nodeToRemove.Parent.RightChild = null;
                    Count--;
                }
            }
            else if (nodeToRemove.LeftChild != null && nodeToRemove.RightChild == null)
            {//left child
                if (nodeToRemove.Parent == null)
                {
                    Head = nodeToRemove.LeftChild;
                    nodeToRemove.LeftChild.Parent = null;
                    Count--;
                    return;
                }
                else if (nodeToRemove.IsLeftChild)
                {
                    nodeToRemove.Parent.LeftChild = nodeToRemove.LeftChild;
                    nodeToRemove.LeftChild.Parent = nodeToRemove.Parent;
                    Count--;
                }
                else
                {
                    nodeToRemove.Parent.RightChild = nodeToRemove.LeftChild;
                    nodeToRemove.LeftChild.Parent = nodeToRemove.Parent;
                    Count--;
                }
            }
            else if (nodeToRemove.LeftChild == null && nodeToRemove.RightChild != null)
            {//right child
                if (nodeToRemove.Parent == null)
                {
                    Head = nodeToRemove.RightChild;
                    nodeToRemove.RightChild.Parent = null;
                    Count--;
                    return;
                }
                else if (nodeToRemove.IsLeftChild)
                {
                    nodeToRemove.Parent.LeftChild = nodeToRemove.RightChild;
                    nodeToRemove.RightChild.Parent = nodeToRemove.Parent;
                    Count--;
                }
                else
                {
                    nodeToRemove.Parent.RightChild = nodeToRemove.RightChild;
                    nodeToRemove.RightChild.Parent = nodeToRemove.Parent;
                    Count--;
                }
            }
            else
            {//both children
                Node<T> replacementNode = nodeToRemove.LeftChild;
                while (replacementNode.RightChild != null)
                {
                    replacementNode = replacementNode.RightChild;
                }

                nodeToRemove.Value = replacementNode.Value;
                RemoveSpecificNode(replacementNode);
                if (nodeToRemove.LeftChild != null)
                {
                    nodeToRemove = nodeToRemove.LeftChild;
                }
                else if (nodeToRemove.RightChild != null)
                {
                    nodeToRemove = nodeToRemove.RightChild;
                }
            }
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

        private void MoveRedLeft(Node<T> node)
        {
            FlipColor(node);

            if (node.RightChild.LeftChild.IsRed)
            {
                RotateRight(node.RightChild);
                node = RotateLeft(node);
            }

            FlipColor(node);

            if (node.RightChild.RightChild.IsRed)
            {
                RotateLeft(node.RightChild);
            }
        }

        private void MoveRedRight(Node<T> node)
        {
            FlipColor(node);

            if (node.LeftChild.LeftChild.IsRed)
            {
                node = RotateRight(node);
            }

            FlipColor(node);
        }

        private void FixUp(Node<T> currentNode)
        {
            if (currentNode == null)
            {
                return;
            }

            if (currentNode.RightChild.IsRed)
            {
                currentNode = RotateLeft(currentNode);
            }

            if (currentNode.LeftChild.IsRed && currentNode.LeftChild.LeftChild.IsRed)
            {
                currentNode = RotateRight(currentNode);
            }

            if (currentNode.Is4Node)
            {
                FlipColor(currentNode);
            }

            Node<T> leftChild = currentNode.LeftChild;
            if (leftChild.RightChild.IsRed)
            {
                leftChild = RotateLeft(leftChild);
            }

            if (leftChild.LeftChild.IsRed && leftChild.LeftChild.LeftChild.IsRed)
            {
                leftChild = RotateRight(leftChild);
            }

            FixUp(currentNode.Parent);
        }
    }
}
