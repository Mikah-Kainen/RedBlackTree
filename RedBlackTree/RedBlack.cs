﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

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
            while (currentNode != null)
            {
                if (currentNode.Equals(targetNode))
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

        public Node<T> Contains(T targetValue)
        {
            Node<T> currentNode = RootNode;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(targetValue))
                {
                    return currentNode;
                }
                else if (targetValue.CompareTo(currentNode.Value) < 0)
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

        private bool IsNodeRed(Node<T> targetNode)
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
            RootNode = Add(RootNode, targetValue);
            Count++;
            RootNode.isRed = false;
        }

        private Node<T> Add(Node<T> currentNode, T value)
        {
            if (currentNode == null)
            {
                return new Node<T>(value);
            }

            if (currentNode.IsFourNode())
            {
                FlipColor(currentNode);
            }

            if (value.CompareTo(currentNode.Value) < 0)
            {
                currentNode.LeftChild = Add(currentNode.LeftChild, value);
            }
            else
            {
                currentNode.RightChild = Add(currentNode.RightChild, value);
            }

            if (currentNode.RightChild != null && currentNode.RightChild.isRed)
            {
                currentNode = LeftRotation(currentNode);
            }

            if (IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.LeftChild))
            {
                currentNode = RightRotation(currentNode);
            }
            return currentNode;
        }

        public bool Remove(T targetValue)
        {
            Node<T> targetNode = Contains(targetValue);
            if(targetNode == null)
            {
                return false;
            }
            return Remove(RootNode, targetNode);
        }

        private bool Remove(Node<T> currentNode, Node<T> targetNode)
        {
            if(targetNode.IsLessThan(currentNode))
            {

            }
        }

        public Node<T> MoveRedLeft(Node<T> currentNode)
        {
            if(!IsNodeRed(currentNode.LeftChild) || currentNode == null)
            {
                throw new Exception("MoveRedLeft called improperly");
            }

            FlipColor(currentNode);
            if(IsNodeRed(currentNode.RightChild) && IsNodeRed(currentNode.RightChild.LeftChild))
            {
                currentNode.RightChild = RightRotation(currentNode.RightChild);
                currentNode = LeftRotation(currentNode);
                FlipColor(currentNode);
            }

            ////////////////////////////////////
            //////////////////
            ///WORK ON THIS PART OVER HERE
            ///??????///////////////////////////////////////////
            ///////////////////
            ///
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /// 
            /////////////////////////////
            //////////////////////////////////////////
            /////////////////////////
            return currentNode;
        }

        public Node<T> MoveRedRight(Node<T> currentNode)
        {
            if(!IsNodeRed(currentNode.RightChild) || currentNode == null)
            {
                throw new Exception("MoveRedRight called improperly");
            }

            FlipColor(currentNode);
            if(IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.LeftChild))
            {
                currentNode = RightRotation(currentNode);
                FlipColor(currentNode);
            }
            return currentNode;
        }

        //public void Add(T targetValue)
        //{
        //    Node<T> newNode = new Node<T>(targetValue);
        //    if (RootNode == null)
        //    {
        //        RootNode = newNode;
        //        newNode.isRed = false;
        //        Count++;
        //        return;
        //    }
        //    RootNode = Add(newNode, RootNode);
        //    Count++;
        //    RootNode.isRed = false;
        //}

        //private Node<T> Add(Node<T> targetNode, Node<T> currentNode)
        //{
        //    if (currentNode == null)
        //    {
        //        throw new Exception("Something Went Wrong!!");
        //    }

        //    if (currentNode.IsFourNode())
        //    {
        //        FlipColor(currentNode);
        //    }
        //    if (targetNode.IsLessThan(currentNode))
        //    {
        //        if (currentNode.LeftChild != null)
        //        {
        //            currentNode.LeftChild = Add(targetNode, currentNode.LeftChild);
        //        }
        //        else
        //        {
        //            currentNode.LeftChild = targetNode;
        //        }
        //    }
        //    else
        //    {
        //        if (currentNode.RightChild != null)
        //        {
        //            currentNode.RightChild = Add(targetNode, currentNode.RightChild);
        //        }
        //        else
        //        {
        //            currentNode.RightChild = targetNode;
        //        }
        //    }

        //    if (currentNode.RightChild != null && currentNode.RightChild.isRed)
        //    {
        //        currentNode = LeftRotation(currentNode);
        //    }
        //    if (currentNode.LeftChild != null && currentNode.LeftChild.LeftChild != null && currentNode.LeftChild.isRed && currentNode.LeftChild.LeftChild.isRed)
        //    {
        //        currentNode = RightRotation(currentNode);
        //    }
        //    return currentNode;
        //}

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
