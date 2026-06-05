using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApplication1
{
    internal class Space {
        public static string[] Astronauts = {"S1", "S2", "S3"};
        
        private static bool CheckIfHitAsteroid(int x, int y, string[,] matrix)
        {
            if (matrix[x, y].Equals("X"))
            {
                return true;
            }

            return false;
        }
        
        private static bool CheckIfHitAstro(int x, int y, string[,] matrix)
        {
            if (Astronauts.Any(matrix[x, y].Contains))
            {
                return true;
            }

            return false;
        }
        
        static void CheckSize(int  size) 
        {
            if (size < 2 || size > 100)
            {
                Environment.Exit(1);
            }
        }
        

        private static int[] CheckIfAsteroidRight(int x, int y, string[,] matrix)
        {
            y++;
            if (matrix.GetLength(1) - 1 < y)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;
            
            int[] xy = { x, y };
            return xy;
        }
        private static int[] CheckIfAsteroidLeft(int x, int y, string[,] matrix)
        {
            y--;
            if (y < 0)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;
            
            
            int[] xy = { x, y };
            return xy;
            
        }
        private static int[] CheckIfAsteroidDown(int x, int y, string[,] matrix)
        {
            x++;
            if (x > matrix.GetLength(0) - 1)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;
            

            int[] xy = {x,y};
            return xy;
        }
        private static int[] CheckIfAsteroidUp(int x, int y, string[,] matrix)
        {
            x--;
            if (x < 0)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;

            int[] xy = {x,y};
            return xy;
        }
        
        private static void RecursionTest(int row, int col, string[,] matrix)
        {
            Queue queue = new Queue();
            int[] right = CheckIfAsteroidRight(row, col, matrix);
            int[] left = CheckIfAsteroidLeft(row, col, matrix);
            int[] up = CheckIfAsteroidUp(row, col, matrix);
            int[] down = CheckIfAsteroidDown(row, col, matrix);
            
            if (right != null){
                Console.WriteLine("RIGHT");
                RecursionTest(right[0], right[1], matrix);
            }

            if (left != null)
            {
                Console.WriteLine("LEFT");
                RecursionTest(left[0], left[1], matrix);
            }

            if (up != null)
            {
                Console.WriteLine("UP");
                RecursionTest(up[0], up[1], matrix);
            }
            if (down != null)
            {
                Console.WriteLine("DOWN");
                RecursionTest(down[0], down[1], matrix);
            }
            Console.WriteLine("DEBUGGER");
        }
        
        public static void Main(string[] args)
        {
            
            Dictionary<string, int[,]> astronautsDictionaryPosition = new Dictionary<string, int[,]>();
            
            Console.Write("Map rows: ");
            var rowsInput = Convert.ToInt32(Console.ReadLine());
            CheckSize(rowsInput);

            Console.Write("Map columns: ");
            var colsInput = Convert.ToInt32(Console.ReadLine());
            CheckSize(colsInput);


            string[,] matrix = new string[rowsInput, colsInput];
            // Matrix 2 dimension
            Console.WriteLine("Cosmic map: ");
            for (int i = 0; i < rowsInput; i++)
            {
                string[] matrixRowInput = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.None);
                for (int j = 0; j < colsInput; j++)
                {
                    if(Astronauts.Any(matrixRowInput[j].Contains)){
                        astronautsDictionaryPosition.Add(matrixRowInput[j], new int[i,j]);
                    }

                    matrix[i, j] = matrixRowInput[j];
                }
            }
            foreach (var entry in astronautsDictionaryPosition)
            {
                int astronautX = entry.Value.GetLength(0);
                int astronautY = entry.Value.GetLength(1);
                // TODO FINISH the algo
                RecursionTest(astronautX, astronautY, matrix);
            }
            
            Console.WriteLine("DEBUGGER");
        }
    }
}