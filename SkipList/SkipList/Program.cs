using System;

namespace SkipList
{
    class Program
    {
        static void Main(string[] args)
        {
            SkipList<int> list = new SkipList<int>();
            list.Add(1, 1);
            list.Add(2, 2);
            list.Add(3, 1);
            list.Add(4, 3);
            list.Add(5, 1);
            list.Add(6, 4);
            list.Add(7, 1);
            list.Add(7, 2);
        }
    }
}
