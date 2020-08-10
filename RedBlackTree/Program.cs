using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            RedBlack<int> Tree = new RedBlack<int>();
        
            for(int i = 0; i < 10; i ++)
            {
                Tree.Add(i);
            }

            for(int i = 19; i > 10; i --)
            {
                Tree.Add(i);
            }

        }
    }
}
