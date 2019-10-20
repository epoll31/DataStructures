using System;
using System.Collections.Generic;
using System.Text;

namespace HeapTree
{
    public class HeapTree<T> where T : IComparable
    {
        private List<T> heap;

        public int Count => heap.Count;

        public HeapTree()
        {
            heap = new List<T>();
        }

        public void Insert(T value)
        {
            heap.Add(value);
            HeapifyUp(heap.Count - 1);
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = GetParent(index);
            while (heap[parentIndex].CompareTo(heap[index]) > 0)
            {
                T temp = heap[parentIndex];
                heap[parentIndex] = heap[index];
                heap[index] = temp;
                index = parentIndex;
                parentIndex = GetParent(index);
            }
        }

        public T Pop()
        {
            T returnValue = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);
            HeapifyDown(0);
            return returnValue;
        }

        private void HeapifyDown(int index)
        {
            int leftChildIndex = GetLeftChild(index);
            int rightChildIndex = GetRightChild(index);

            while (true)
            {
                if (rightChildIndex < heap.Count && heap[leftChildIndex].CompareTo(heap[rightChildIndex]) > 0)//right is smaller
                {
                    if (rightChildIndex < heap.Count && heap[rightChildIndex].CompareTo(heap[index]) < 0)//swap down
                    {
                        T temp = heap[rightChildIndex];
                        heap[rightChildIndex] = heap[index];
                        heap[index] = temp;
                        index = rightChildIndex;
                        rightChildIndex = GetRightChild(index);
                        leftChildIndex = GetLeftChild(index);
                    }
                    else
                    {
                        break;
                    }
                }
                else //left is smaller
                {
                    if (leftChildIndex < heap.Count && heap[leftChildIndex].CompareTo(heap[index]) < 0)//swap down
                    {
                        T temp = heap[leftChildIndex];
                        heap[leftChildIndex] = heap[index];
                        heap[index] = temp;
                        index = leftChildIndex;
                        rightChildIndex = GetRightChild(index);
                        leftChildIndex = GetLeftChild(index);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private int GetParent(int index) => (index - 1) / 2;
        private int GetLeftChild(int index) => index * 2 + 1;
        private int GetRightChild(int index) => index * 2 + 2;

        public static T[] HeapSort(T[] data)
        {
            T[] returnData = new T[data.Length];
            HeapTree<T> heap = new HeapTree<T>();

            for (int i = 0; i < data.Length; i++)
            {
                heap.Insert(data[i]);
            }
            for (int i = 0; i < data.Length; i++)
            {
                returnData[i] = heap.Pop();
            }
            return returnData;
        }
    }
}
