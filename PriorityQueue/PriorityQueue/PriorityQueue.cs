
using System;
using System.Collections.Generic;
using System.Text;

namespace PriorityQueue
{
    public class PriorityQueue<TPriority, TValue> where TPriority : IComparable where TValue : IComparable
    {
        private List<(TPriority, Queue<TValue>)> heap;

        public int Count = 0;

        public PriorityQueue()
        {
            heap = new List<(TPriority, Queue<TValue>)>();
        }

        public void Insert(TValue value, TPriority priority)
        {
            bool shouldAdd = true;
            for (int i = 0; i < heap.Count; i++)
            {
                if (heap[i].Item1.CompareTo(priority) == 0)
                {
                    heap[i].Item2.Enqueue(value);
                    shouldAdd = false;
                }
            }

            if (shouldAdd)
            {
                Queue<TValue> queue = new Queue<TValue>();
                queue.Enqueue(value);

                heap.Add((priority, queue));
                HeapifyUp(heap.Count - 1);
            }
            Count++;
        }

        private void HeapifyUp(int index)
        {
            int parentIndex = GetParent(index);
            while (heap[parentIndex].Item1.CompareTo(heap[index].Item1) > 0)
            {
                (TPriority, Queue<TValue>) temp = heap[parentIndex];
                heap[parentIndex] = heap[index];
                heap[index] = temp;
                index = parentIndex;
                parentIndex = GetParent(index);
            }
        }

        public TValue Pop()
        {
            TValue returnValue = heap[0].Item2.Dequeue();
            if (heap[0].Item2.Count == 0)
            {
                heap[0] = heap[heap.Count - 1];
                heap.RemoveAt(heap.Count - 1);
                HeapifyDown(0);
            }
            Count--;
            return returnValue;
        }

        private void HeapifyDown(int index)
        {
            int leftChildIndex = GetLeftChild(index);
            int rightChildIndex = GetRightChild(index);

            while (true)
            {
                if (rightChildIndex < heap.Count && heap[leftChildIndex].Item1.CompareTo(heap[rightChildIndex].Item1) > 0)//right is smaller
                {
                    if (rightChildIndex < heap.Count && heap[rightChildIndex].Item1.CompareTo(heap[index].Item1) < 0)//swap down
                    {
                        (TPriority, Queue<TValue>) temp = heap[rightChildIndex];
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
                    if (leftChildIndex < heap.Count && heap[leftChildIndex].Item1.CompareTo(heap[index].Item1) < 0)//swap down
                    {
                        (TPriority, Queue<TValue>) temp = heap[leftChildIndex];
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
    }
}
