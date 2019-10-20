using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SkipList
{
    public class SkipList<T> : ICollection<T> where T : IComparable
    {
        public T this[int i]
        {
            get
            {
                Node<T> currentNode = head;
                for (; i >= 0; i--)
                {
                    currentNode = currentNode[0];
                }
                return currentNode.Value;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        private Node<T> head;
        private Random random;

        public SkipList()
        {
            head = new Node<T>(default, 0);
            random = new Random();
        }

        private void NewHead() => NewHead(true);
        private void NewHead(bool shouldAdd)
        {
            if (shouldAdd)
            {
                Node<T> newHead = new Node<T>(default, head.Height + 1);
                for (int i = 0; i < head.Height; i++)
                {
                    newHead[i] = head[i];
                }
                head = newHead;
            }
            else
            {
                Node<T> newHead = new Node<T>(default, head.Height - 1);
                for (int i = 0; i < newHead.Height; i++)
                {
                    newHead[i] = head[i];
                }
                head = newHead;
            }
        }

        public void Add(T item, int height)
        {
            Node<T> newNode = new Node<T>(item, height);

            if (head.Height < newNode.Height)
            {
                NewHead();
                head[head.Height - 1] = newNode;
            }

            Node<T> currentNode = head;
            for (int i = newNode.Height - 1; i >= 0; i--)
            {
                if (currentNode[i] == null && currentNode.Height >= i + 1 && currentNode[i + 1] != null)
                {
                    currentNode[i] = currentNode[i + 1];
                }

                if (item.CompareTo(currentNode[i].Value) > 0)//move right
                {
                    if (currentNode[i][i] == null)
                    {
                        currentNode[i][i] = newNode;
                    }
                    currentNode = currentNode[i];
                    i++;
                }
                else if (item.CompareTo(currentNode[i].Value) < 0)//move down
                {
                    if (i < newNode.Height)
                    {
                        newNode[i] = currentNode[i];
                        currentNode[i] = newNode;
                    }
                    continue;
                }
                else
                {
                    continue;
                }
            }

            Count++;
        }

        public void Add(T Item)
        {
            Add(Item, GetNewHeight());
        }

        public bool Remove(T item)
        {
            Node<T> currentNode = head;
            for (int i = head.Height - 1; i >= 0; i--)
            {
                if (item.CompareTo(currentNode[i].Value) < 0)//move down
                {
                    continue;
                }
                else if (item.CompareTo(currentNode[i].Value) > 0)//move right
                {
                    currentNode = currentNode[i++];
                }
                else//delete currentNode[i]
                {
                    currentNode[i] = currentNode[i][i];
                    currentNode = head;
                }
            }

            if (head[head.Height - 1] == null)
            {
                NewHead(false);
            }

            if (!Contains(item))
            {
                Count--;
                return true;
            }
            else
            {
                return false;
            }
        }

        private int GetNewHeight()
        {
            int height = 1;
            while (random.Next(0, 2) == 0 && height <= head.Height)
            {
                height++;
            }
            return height;
        }

        public void Clear()
        {
            head = new Node<T>(default, 0);
            Count = 0;
        }

        public bool Contains(T item)
        {
            Node<T> currentNode = head;
            for (int i = head.Height - 1; i >= 0; i--)
            {
                if (currentNode[i] != null && item.CompareTo(currentNode[i].Value) > 0)//move right
                {
                    currentNode = currentNode[i++];
                }
                else if (currentNode[i] == null || item.CompareTo(currentNode[i].Value) < 0)//move down
                {
                    continue;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            while (true)
            {
                current = current[0];
                if (current != null)
                {
                    yield return current.Value;
                }
                else
                {
                    yield break;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
