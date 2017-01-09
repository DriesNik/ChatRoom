using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreee
{
    class BinaryTree<T> where T : IComparable
    {
        private Node<T> root;
        private int count;
        public void Insert(T number)
        {
            if (CheckEmpty())
            {
               root = new Node<T>(number);
            }
            else
            {
                root.AddData(root, number);
            }
        }
        public bool CheckEmpty()
        {
           return root.IsEmpty();
        }
        public bool Search(T input)
        {
           return root.Search(root, input);
        }
    }
}
