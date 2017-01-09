using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreee
{
    class Node<T> where T : IComparable
    {
        List<T> list = new List<T>();
        
        private T number;
        private Node<T> rightLeaf;
        private Node<T> leftLeaf;
        public Node(T value)
        {
            number = value;
            rightLeaf = null;
            leftLeaf = null;

        }
        public int CompareTo(T item)
        {
            Node<T> number = item as Node<T>;
            return this.number.CompareTo(number.number);
        }
        public void AddData(Node<T> node, T input)
        {
           
            if (node == null)
            {
                node = new Node<T>(input);
            }
            else if (input.CompareTo(number) > 0)
            {
                AddData(node.rightLeaf, input);
            }
            else if (input.CompareTo(number) < 0)
            {
                AddData(node.leftLeaf, input);
            }
        }
        public bool IsEmpty()
        {
            if (rightLeaf == null  && leftLeaf == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Search(Node<T> node, T searchNumber)
        {
            if (node == null)
            {
                return false;
            }
            if (searchNumber.CompareTo(number) == 0)
            {
                Console.WriteLine(" {0}", number);
                return true;
            }
            else if (searchNumber.CompareTo(number) >0)
            {
                Console.WriteLine(" {0}", number);
                Search(rightLeaf, searchNumber);
            }
            else if (searchNumber.CompareTo(number) <0)
            {
                Console.WriteLine(" {0}", number);
                Search(leftLeaf, searchNumber);
            }
            return false;
        }
       

    }
}
