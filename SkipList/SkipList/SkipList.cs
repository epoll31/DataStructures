using System;
using System.Collections;
using System.Collections.Generic;
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

        public void Add(T item, int height = -1)
        {
            Node<T> newNode = new Node<T>(item, height == -1 ? GetNewHeight() : height);

            if (head.Height < newNode.Height)
            {
                NewHead();
                head[head.Height - 1] = newNode;
            }

            Node<T> currentNode = head;
            for (int i = head.Height - 1; i >= 0; i--)
            {
                if (item.CompareTo(currentNode[i].Value) > 0)//move right
                {
                    if (currentNode[i][i] == null)
                    {
                        currentNode[i] = newNode;
                        i++;
                    }
                    else
                    {
                        currentNode = currentNode[i];
                        i++;
                    }
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
            }

        }

        public void Add(T Item)
        {

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
            throw new NotImplementedException();
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
