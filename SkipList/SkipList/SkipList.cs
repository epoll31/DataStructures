using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    class SkipList<T> : ICollection<T> where T : IComparable
    {
        public int Count { get; private set;}

        public bool IsReadOnly => false;

        private Node<T> head;
        private Random random;

        public SkipList()
        {
            head = new Node<T>(0);
            random = new Random();
        }

        public void Add(T item)
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
