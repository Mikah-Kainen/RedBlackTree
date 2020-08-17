using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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
            RootNode = Remove(RootNode, targetNode);
            Count--;
            return true;
        }

        private Node<T> Remove(Node<T> currentNode, Node<T> targetNode)
        {
            if(currentNode == null)
            {
                return null;
            }
            if(targetNode.IsLessThan(currentNode))
            {
                if(currentNode.LeftChild == null)
                {
                    throw new Exception("targetNotFound");
                }
                if (!IsNodeRed(currentNode.LeftChild) && !IsNodeRed(currentNode.LeftChild.LeftChild))
                {
                    currentNode = MoveRedLeft(currentNode);
                }
                currentNode.LeftChild = Remove(currentNode.LeftChild, targetNode);
            }
            else
            {
                if(IsNodeRed(currentNode.LeftChild))
                {
                    currentNode = RightRotation(currentNode);
                }
                if (currentNode.Equals(targetNode) && currentNode.IsLeafNode())
                {
                    currentNode = null;
                    return null;
                }
                else if (!currentNode.Equals(targetNode))
                {
                    if (!IsNodeRed(currentNode.RightChild) && !IsNodeRed(currentNode.RightChild.LeftChild))
                    {
                        currentNode = MoveRedRight(currentNode);
                    }
                    currentNode.RightChild = Remove(currentNode.RightChild, targetNode);
                }
                else if (!IsNodeRed(currentNode.RightChild) && !IsNodeRed(currentNode.RightChild.LeftChild))
                {
                    T tempHolder = currentNode.Value;
                    currentNode = MoveRedRight(currentNode);
                    if (!tempHolder.Equals(currentNode.Value))
                    {
                        //throw new Exception("Mikah Look at This!!!!!!!!!!!!!!!!");
                        currentNode.RightChild = Remove(currentNode.RightChild, targetNode);
                    }
                    else 
                    {
                        Node<T> replacementNode = currentNode.FindMin();
                        currentNode.ReplaceValue(replacementNode);
                        currentNode.RightChild = Remove(currentNode.RightChild, replacementNode);
                    }
                }
                else
                {
                    Node<T> replacementNode = currentNode.FindMin();
                    currentNode.ReplaceValue(replacementNode);
                    currentNode.RightChild = Remove(currentNode.RightChild, replacementNode);
                }
            }
            currentNode = Fixup(currentNode);
            return currentNode;
        }

        //private Node<T> Remove(Node<T> currentNode, Node<T> targetNode)
        //{
        //    if(currentNode == null)
        //    {
        //        return null;
        //    }
        //    if(targetNode.IsLessThan(currentNode))
        //    {
        //        if(!IsNodeRed(currentNode.LeftChild) && !IsNodeRed(currentNode.LeftChild.LeftChild))
        //        {
        //            currentNode = MoveRedLeft(currentNode);
        //        }
        //        currentNode.LeftChild = Remove(currentNode.LeftChild, targetNode);
        //    }
        //    else
        //    {
        //        if(IsNodeRed(currentNode.LeftChild))
        //        {
        //            currentNode = RightRotation(currentNode);
        //        }
        //        if(targetNode.Equals(currentNode) && currentNode.IsLeafNode())
        //        {
        //            return null;
        //        }
        //        else if(!targetNode.Equals(currentNode))
        //        {
        //            if (!IsNodeRed(currentNode.RightChild) && !IsNodeRed(currentNode.RightChild.LeftChild))
        //            {
        //                currentNode = MoveRedRight(currentNode);
        //            }
        //            currentNode.RightChild = Remove(currentNode.RightChild, targetNode);
        //        }
        //        else
        //        {
        //            if (!IsNodeRed(currentNode.RightChild) && !IsNodeRed(currentNode.RightChild.LeftChild))
        //            {
        //                currentNode = MoveRedRight(currentNode);
        //                Node<T> removeNode = currentNode.RightChild;

        //                Node<T> replacementNode = removeNode.RightChild.FindMin();
        //                removeNode.ReplaceValue(replacementNode);
        //                removeNode.RightChild = Remove(removeNode.RightChild, replacementNode);

        //                currentNode.RightChild = Fixup(removeNode);
        //            }
        //            else
        //            {
        //                Node<T> replacementNode = currentNode.RightChild.FindMin();
        //                currentNode.ReplaceValue(replacementNode);
        //                currentNode.RightChild = Remove(currentNode.RightChild, replacementNode);
        //            }
        //        }
        //    }

        //    currentNode = Fixup(currentNode);
        //    return currentNode;
        //}

        public Node<T> Fixup(Node<T> currentNode)
        {
            if(IsNodeRed(currentNode.RightChild))
            {
                currentNode = LeftRotation(currentNode);
            }
            if(IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.LeftChild))
            {
                currentNode.LeftChild = RightRotation(currentNode.LeftChild);
            }
            if(currentNode.IsFourNode())
            {
                FlipColor(currentNode);
            }
            if(IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.RightChild))
            {
                Node<T> leftChild = currentNode.LeftChild;
                if(IsNodeRed(leftChild.RightChild))
                {
                    leftChild = LeftRotation(leftChild);
                }
                if(IsNodeRed(leftChild.LeftChild) && IsNodeRed(leftChild.LeftChild.LeftChild))
                {
                    leftChild.LeftChild = RightRotation(leftChild.LeftChild);
                }
                currentNode.LeftChild = leftChild;
            }
            return currentNode;
        }

        public Node<T> MoveRedLeft(Node<T> currentNode)
        {
            if(IsNodeRed(currentNode.LeftChild) || currentNode == null)
            {
                throw new Exception("MoveRedLeft called improperly");
            }

            FlipColor(currentNode);
            if(IsNodeRed(currentNode.RightChild) && IsNodeRed(currentNode.RightChild.LeftChild))
            {
                currentNode.RightChild = RightRotation(currentNode.RightChild);
                currentNode = LeftRotation(currentNode);
                FlipColor(currentNode);
                if(IsNodeRed(currentNode.RightChild.RightChild))
                {
                    currentNode.RightChild = LeftRotation(currentNode.RightChild);
                }
            }

            return currentNode;
        }

        public Node<T> MoveRedRight(Node<T> currentNode)
        {
            if(IsNodeRed(currentNode.RightChild) || currentNode == null)
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
