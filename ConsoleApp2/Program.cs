using System.Collections;

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
        

        private static Tuple<int, int> CheckIfAsteroidRight(int x, int y, string[,] matrix)
        {
            y++;
            if (matrix.GetLength(1) - 1 < y)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;
            
            return new Tuple<int, int>(x,y);
        }
        private static Tuple<int, int> CheckIfAsteroidLeft(int x, int y, string[,] matrix)
        {
            y--;
            if (y < 0)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;
            
            
            return  new Tuple<int, int>(x,y);
            
        }
        private static Tuple<int, int> CheckIfAsteroidDown(int x, int y, string[,] matrix)
        {
            x++;
            if (x > matrix.GetLength(0) - 1)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;
            

            return  new Tuple<int, int>(x,y);
        }
        private static Tuple<int, int> CheckIfAsteroidUp(int x, int y, string[,] matrix)
        {
            x--;
            if (x < 0)
            {
                return null;
            }
            if (CheckIfHitAsteroid(x, y, matrix)) return null;
            if (CheckIfHitAstro(x, y, matrix)) return null;

            return  new Tuple<int, int>(x,y);
        }
        
        private static void RecursionTest((int x, int y) tuple, string[,] matrix)
        {
            Queue queue = new Queue();
            
            Tuple<int, int> right = CheckIfAsteroidRight(tuple.x, tuple.y, matrix);
            Tuple<int, int> left = CheckIfAsteroidLeft(tuple.x, tuple.y, matrix);
            Tuple<int, int> up = CheckIfAsteroidUp(tuple.x, tuple.y, matrix);
            Tuple<int, int> down = CheckIfAsteroidDown(tuple.x, tuple.y, matrix);
            
            if (right != null){
                Console.WriteLine("RIGHT");
                RecursionTest((right.Item1,right.Item2), matrix);
            }

            if (left != null)
            {
                Console.WriteLine("LEFT");
                RecursionTest((left.Item1,left.Item2), matrix);
            }

            if (up != null)
            {
                Console.WriteLine("UP");
                RecursionTest((up.Item1,up.Item2), matrix);
            }
            if (down != null)
            {
                Console.WriteLine("DOWN");
                RecursionTest((down.Item1,down.Item2), matrix);
            }
            Console.WriteLine("DEBUGGER");
        }
        
        public static void Main(string[] args)
        {
            Dictionary<string, (int x, int y)> astronautsDictionaryPosition = new Dictionary<string, (int x, int y)>();
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
                        astronautsDictionaryPosition.Add(matrixRowInput[j], (i,j));
                    }

                    matrix[i, j] = matrixRowInput[j];
                }
            }
            foreach (var entry in astronautsDictionaryPosition)
            {
                // TODO FINISH the algo
                RecursionTest(entry.Value, matrix);
            }
            
            Console.WriteLine("DEBUGGER");
        }
    }
}