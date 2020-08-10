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

        private Node<T> FindParent(Node<T> targetNode)
        {
            Node<T> currentNode = RootNode;
            Node<T> previousNode = RootNode;
            while(currentNode != null)
            {
                if(currentNode.Equals(targetNode))
                {
                    return previousNode;
                }
                if (targetNode.IsLessThan(currentNode))
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }
            return null;
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
            RootNode = Add(newNode, RootNode);
            Count++;
            RootNode.isRed = false;
        }

        private Node<T> Add(Node<T> targetNode, Node<T> currentNode)
        {
            if(currentNode == null)
            {
                throw new Exception("Something Went Wrong!!");
            }

            if(currentNode.IsFourNode())
            {
                FlipColor(currentNode);
            }
            if(targetNode.IsLessThan(currentNode))
            {
                if (currentNode.LeftChild != null)
                {
                    currentNode.LeftChild = Add(targetNode, currentNode.LeftChild);
                }
                else
                {
                    currentNode.LeftChild = targetNode;
                }          
            }
            else
            {
                if (currentNode.RightChild != null)
                {
                    currentNode.RightChild = Add(targetNode, currentNode.RightChild);
                }
                else
                {
                    currentNode.RightChild = targetNode;
                }
            }

            if(currentNode.RightChild != null && currentNode.RightChild.isRed)
            {
                currentNode = LeftRotation(currentNode);
            }
            if(currentNode.LeftChild != null && currentNode.LeftChild.LeftChild != null && currentNode.LeftChild.isRed && currentNode.LeftChild.LeftChild.isRed)
            {
                currentNode = RightRotation(currentNode);
            }
            return currentNode;
        }

        private Node<T> RightRotation(Node<T> targetNode)
        {
            Node<T> tempHolder = targetNode.LeftChild;
            targetNode.LeftChild = tempHolder.RightChild;
            tempHolder.RightChild = targetNode;

            tempHolder.isRed = targetNode.isRed;
            targetNode.isRed = true;
            return tempHolder;
        }

        private Node<T> LeftRotation(Node<T> targetNode)
        {
            Node<T> tempHolder = targetNode.RightChild;
            targetNode.RightChild = tempHolder.LeftChild;
            tempHolder.LeftChild = targetNode;

            tempHolder.isRed = targetNode.isRed;
            targetNode.isRed = true;
            return tempHolder;
        }

        //private void LeftRotation(Node<T> targetNode)
        //{
        //    Node<T> parentNode = FindParent(targetNode);
        //    Node<T> tempHolder = targetNode.RightChild;

        //    targetNode.RightChild = targetNode.RightChild.LeftChild;
        //    tempHolder.LeftChild = targetNode;
        //    tempHolder.isRed = targetNode.isRed;
        //    tempHolder.LeftChild.isRed = true;
        //    if(parentNode.Equals(targetNode))
        //    {
        //        RootNode = tempHolder;
        //    }
        //    else
        //    {
        //        if(parentNode.RightChild != null && parentNode.RightChild.Equals(targetNode))
        //        {
        //            parentNode.RightChild = tempHolder;
        //        }
        //        else
        //        {
        //            parentNode.LeftChild = tempHolder;
        //        }
        //    }
        //}

        //private void RightRotation(Node<T> targetNode)
        //{
        //    Node<T> parentNode = FindParent(targetNode);
        //    Node<T> tempHolder = targetNode.LeftChild;

        //    targetNode.LeftChild = targetNode.LeftChild.RightChild;
        //    tempHolder.RightChild = targetNode;
        //    tempHolder.isRed = targetNode.isRed;
        //    tempHolder.RightChild.isRed = true;
        //    if (parentNode == null)
        //    {
        //        RootNode = tempHolder;
        //    }
        //    else
        //    {
        //        if (parentNode.RightChild != null && parentNode.RightChild.Equals(targetNode))
        //        {
        //            parentNode.RightChild = tempHolder;
        //        }
        //        else
        //        {
        //            parentNode.LeftChild = tempHolder;
        //        }
        //    }
        //}
    }
}
