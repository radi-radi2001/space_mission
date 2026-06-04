using System;

namespace ConsoleApplication1
{
    internal class Space
    {
        public static void Main(string[] args)
        {
            var rowsInput = Convert.ToInt32(Console.ReadLine());
            var colsInput = Convert.ToInt32(Console.ReadLine());
            
            int[] rows = new int[rowsInput];
            int[] cows = new int [colsInput];

            for (int i = 0; i < cows.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    Console.Write(cows[j] + " ");
                }

                Console.WriteLine("");
            }
        }
    }
}