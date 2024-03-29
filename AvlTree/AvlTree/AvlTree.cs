﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AvlTree
{
    public class AvlTree<T> where T : IComparable
    {
        public Node<T> Head { get; private set; }
        public int Count { get; private set; }

        public AvlTree()
        {
            Head = null;
            Count = 0;
        }

        private Node<T> GetNode(T value)
        {
            var currentNode = Head;

            while (currentNode != null)
            {
                if (value.CompareTo(currentNode.Value) < 0)
                {
                    currentNode = currentNode.LeftChild;
                }
                else if (value.CompareTo(currentNode.Value) > 0)
                {
                    currentNode = currentNode.RightChild;
                }
                else
                {
                    return currentNode;
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
                return;
            }

            var currentNode = Head;

            while (currentNode != null)
            {
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
                else if (value.CompareTo(currentNode.Value) > 0)
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
        public void Remove(T value) => Remove(GetNode(value));
        private void Remove(Node<T> nodeToRemove)
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
                Remove(replacementNode);
                if (nodeToRemove.LeftChild != null)
                {
                    nodeToRemove = nodeToRemove.LeftChild;
                }
                else if (nodeToRemove.RightChild != null)
                {
                    nodeToRemove = nodeToRemove.RightChild;
                }
            }

            Balance(nodeToRemove.Parent);
        }

        private void Balance(Node<T> currentNode)
        {
            while (Math.Abs(currentNode.Balance) <= 1)
            {
                currentNode = currentNode.Parent;
                if (currentNode == null)
                {
                    return;
                }
            }

            if (currentNode.Balance > 1)
            {
                if (currentNode.RightChild.Balance < 0)
                {
                    RotateRight(currentNode.RightChild);
                }
                RotateLeft(currentNode);
            }
            else
            {
                if (currentNode.LeftChild.Balance > 0)
                {
                    RotateLeft(currentNode.LeftChild);
                }
                RotateRight(currentNode);
            }
        }

        private void RotateLeft(Node<T> currentNode)
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
        }

        private void RotateRight(Node<T> currentNode)
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
        }
    }
}
