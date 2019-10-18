using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SkipList
{
    class SkipList<T> : ICollection<T> where T : IComparable
    {
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        private Node<T> head;
        private Random random;

        public SkipList()
        {
            head = new Node<T>(default, 0);
            random = new Random();
        }

        private void NewHead()
        {
            Node<T> newHead = new Node<T>(default, head.Height + 1);
            for (int i = 0; i < head.Height; i++)
            {
                newHead[i] = head[i];
            }
            head = newHead;
        }

        public void Add(T item, int height)
        {
            Node<T> newNode = new Node<T>(item, height);
            Console.WriteLine(newNode.Height);

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
