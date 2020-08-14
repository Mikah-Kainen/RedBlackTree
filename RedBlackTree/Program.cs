using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            RedBlack<int> Tree = new RedBlack<int>();

            //for(int i = 0; i < 10; i ++)
            //{
            //    Tree.Add(i);
            //}

            //for(int i = 19; i > 10; i --)
            //{
            //    Tree.Add(i);
            //}
            Tree.Add(5);
            Tree.Add(17);
            Tree.Add(19);
            Tree.Add(20);
            Tree.Add(21);
            Tree.Add(3);
            Tree.Add(4);
            Tree.Add(1);
            Tree.Remove(4);
            Tree.Remove(20);
            Tree.Remove(17);
        }
    }
}
