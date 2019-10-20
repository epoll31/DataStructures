﻿using System;

namespace HeapTree
{
    class Program
    {
        static void Main(string[] args)
        {
            HeapTree<int> heap = new HeapTree<int>();
            //heap.Insert(1);
            //heap.Insert(9);
            //heap.Insert(2);
            //heap.Insert(13);
            //heap.Insert(10);
            //heap.Insert(3);
            //heap.Insert(0);

            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                heap.Insert(random.Next(0, 1000));
            }

            while(heap.Count > 0)
            {
                Console.WriteLine(heap.Pop());
            }

        }
    }
}
