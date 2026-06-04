using System;

namespace ConsoleApplication1
{
    internal class Space
    {
        static void CheckSize(int  size) 
        {
            if (size < 2 || size > 100)
            {
                Environment.Exit(1);
            }
        }
        public static void Main(string[] args)
        {
            string[] astronauts = {"S1", "S2", "S3"};
            const char destination = 'F';
            const char openSpace = 'O';
            const char asteroid = 'X';

            Console.Write("Map rows: ");
            var rowsInput = Convert.ToInt32(Console.ReadLine());
            CheckSize(rowsInput);

            Console.Write("Map columns: ");
            var colsInput = Convert.ToInt32(Console.ReadLine());
            CheckSize(colsInput);


            string[,] matrix = new string[rowsInput, colsInput];
            // Matrix 2 dimension
            Console.WriteLine("Cosmic map: ");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string[] matrixRowInput = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.None);
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = matrixRowInput[j];
                }
            }

            Console.WriteLine();
        }
    }
}