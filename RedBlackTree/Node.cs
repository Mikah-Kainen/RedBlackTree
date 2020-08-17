using System;
using System.Collections.Generic;
using System.Text;

namespace RedBlackTree
{
    public class Node<T> where T : IComparable<T>
    {
        public T Value { get; private set; }
        public Node<T> LeftChild;
        public Node<T> RightChild;
        public bool isRed { get; set; }

        public Node(T Value)
        {
            this.Value = Value;
            LeftChild = null;
            RightChild = null;
            isRed = true;
        }

        public bool IsFourNode()
        {
            if(LeftChild == null || RightChild == null)
            {
                return false;
            }

            return LeftChild.isRed && RightChild.isRed;
        }

        public bool IsLessThan(Node<T> comparerNode)
        {
            return Value.CompareTo(comparerNode.Value) < 0;
        }

        public bool IsLeafNode()
        {
            return RightChild == null && LeftChild == null;
        }

        public Node<T> FindMin()
        {
            if(RightChild == null)
            {
                return null;
            }
            Node<T> currentNode = RightChild;
            while(currentNode.LeftChild != null)
            {
                currentNode = currentNode.LeftChild;
            }
            return currentNode;
        }

        public void ReplaceValue(Node<T> replacementNode)
        {
            Value = replacementNode.Value;
        }
    }
}
