using System;

namespace PriorityQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<int, string> priorityQueue = new PriorityQueue<int, string>();
            priorityQueue.Insert("ethan01", 0);
            priorityQueue.Insert("ethan22", 2);
            priorityQueue.Insert("ethan11", 1);
            priorityQueue.Insert("ethan21", 2);
            priorityQueue.Insert("ethan12", 1);
            priorityQueue.Insert("ethan02", 0);

            while (priorityQueue.Count != 0)
            {
                Console.WriteLine(priorityQueue.Pop());
            }
        }
    }
}
