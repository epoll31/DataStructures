using System;

namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            SkipList<string> list = new SkipList<string>();
            //Random random = new Random();
            //for (int i = 0; i < 100; i++)
            //{
            //    list.Add(random.Next(0, 1000));
            //}

            list.Add("Alice", 1);
            list.Add("Bob", 2);
            list.Add("Cal", 1);
            list.Add("Dave", 3);
            list.Add("Ed", 1);
            list.Add("Frank", 4);
            list.Add("Gil", 1);
            list.Add("Hank", 2);

            list.Remove("Frank");
        }
    }
}
