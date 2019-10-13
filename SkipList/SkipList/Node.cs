using System;
using System.Collections.Generic;
using System.Text;

namespace SkipList
{
    public class Node<T> where T : IComparable
    {
        private List<Node<T>> _neighbors;

        public Node()
        {

        }
    }
}
