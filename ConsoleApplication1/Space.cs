using System;

namespace ConsoleApplication1
{
    internal class Space
    {
        static Boolean CheckSize(int  size) 
        {
            if (size < 2 || size > 100)
            {
                return false;
            } 
            return true;
        }
        public static void Main(string[] args)
        {
            int[] rows = { };
            int[] cols = { };
            
            var rowsInput = Convert.ToInt32(Console.ReadLine());
            var colsInput = Convert.ToInt32(Console.ReadLine());
            
            if (CheckSize(rowsInput)){
                rows = new int[rowsInput];
            }
            
            if (CheckSize(colsInput)){
                cols = new int [colsInput];
            }
            
            // Matrix 2 dimension
            for (int i = 0; i < cols.Length; i++)
            {
                for (int j = 0; j < rows.Length; j++)
                {
                    Console.Write(cols[j] + " ");
                }

                Console.WriteLine("");
            }
        }
    }
}