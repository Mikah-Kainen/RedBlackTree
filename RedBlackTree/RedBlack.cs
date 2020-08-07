using System;
using System.Collections.Generic;
using System.Text;

namespace RedBlackTree
{
    public class RedBlack<T> where T : IComparable<T>
    {
        public int Count { get; private set; }
        public Node<T> RootNode;

        public RedBlack()
        {
            Count = 0;
            RootNode = null;
        }

        private bool isNodeRed(Node<T> targetNode)
        {
            if (targetNode == null)
            {
                return false;
            }
            return targetNode.isRed;
        }

        private void FlipColor(Node<T> targetNode)
        {
            if (targetNode == null)
            {
                return;
            }
            targetNode.isRed = !targetNode.isRed;

            if (targetNode.RightChild != null)
            {
                targetNode.RightChild.isRed = !targetNode.RightChild.isRed;
            }

            if (targetNode.LeftChild != null)
            {
                targetNode.LeftChild.isRed = !targetNode.LeftChild.isRed;
            }

            if (targetNode == RootNode)
            {
                targetNode.isRed = false;
            }
        }

        public void Add(T targetValue)
        {
            Node<T> newNode = new Node<T>(targetValue);
            if(RootNode == null)
            {
                RootNode = newNode;
                newNode.isRed = false;
                Count++;
                return;
            }
            Add(newNode, RootNode);
        }

        private void Add(Node<T> targetNode, Node<T> currentNode)
        {

        }

        private void LeftRotation(Node<T> targetNode)
        {
            Node<T> parentNode = FindParent(targetNode);
            Node<T> tempHolder = targetNode.RightChild;

            targetNode.RightChild = targetNode.RightChild.LeftChild;
            tempHolder.LeftChild = targetNode;
            if(parentNode == null)
            {
                RootNode = tempHolder;
            }
            else
            {
                if(parentNode.RightChild.Equals(targetNode))
                {
                    parentNode.RightChild = tempHolder;
                }
                else
                {
                    parentNode.LeftChild = tempHolder;
                }
            }

        }

        private void RightRotation(Node<T> targetNode)
        {

        }
    }
}
