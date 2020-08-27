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
            //Tree.Add(1);
            //Tree.Add(7);
            //Tree.Add(11);
            //Tree.Add(17);

            //var cu = Tree.FindFloor(11);
            //var sine = Tree.FindCeiling(7);

            //for (int x = 0; x < 500; x++)
            //{
            //    Random random = new Random();
            //    int[] numbers = new int[500];

            //    for (int i = 0; i < 500; i++)
            //    {
            //        numbers[i] = random.Next(1, 1000);
            //        Tree.Add(numbers[i]);
            //    }
            //    for (int i = 0; i < 500; i += 2)
            //    {
            //        Tree.Remove(numbers[i]);
            //    }

            //    //Console.WriteLine("Finished");
            //}

            Tree.Add(576);
            Tree.Add(234);
            Tree.Add(199);
            Tree.Add(890);
            Tree.Add(900);
            Tree.Add(673);
            Tree.Add(467);

            var thing = Tree.FindCeiling(0);
            var bla = Tree.FindCeiling(100);
            var who = Tree.FindCeiling(200);
            var sa = Tree.FindCeiling(300);
            var why = Tree.FindCeiling(500);
            var food = Tree.FindCeiling(700);
            var sugar = Tree.FindCeiling(100000);

            var things = Tree.FindFloor(0);
            var blas = Tree.FindFloor(100);
            var whos = Tree.FindFloor(200);
            var saw = Tree.FindFloor(300);
            var whys = Tree.FindFloor(500);
            var foods = Tree.FindFloor(700);
            var sugarw = Tree.FindFloor(100000);
        }
    }
}
