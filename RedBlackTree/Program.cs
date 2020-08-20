using System;

namespace RedBlackTree
{
    class Program
    {
        static void Main(string[] args)
        {
            RedBlack<int> Tree = new RedBlack<int>();

            //Tree.Add(10);
            //Tree.Add(15);
            //Tree.Add(5);
            //Tree.Add(25);
            //Tree.Add(20);
            //Tree.Add(30);

            for (int x = 0; x < 1000; x ++)
            {
                Random random = new Random();
                int[] numbers = new int[1000];

                for (int i = 0; i < 1000; i++)
                {
                    numbers[i] = random.Next(0, 10000);
                    Tree.Add(numbers[i]);
                }
                for (int i = 0; i < 1000; i += 2)
                {
                    Tree.Remove(numbers[i]);
                }

                Console.WriteLine("Finished");
            }
        }
    }
}
