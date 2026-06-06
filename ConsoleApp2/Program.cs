using System.Collections;

namespace ConsoleApplication1
{
    internal class Space {
        private static string[] Astronauts = {"S1", "S2", "S3"};
        private static (int x, int y) Final;
        
        private static bool CheckIfHitAsteroid(int x, int y, string[,] matrix)
        {
            if (matrix[x, y].Equals("X"))
            {
                return true;
            }

            return false;
        }
        
        private static bool CheckIfFinal(int x, int y, string[,] matrix)
        {
            if (matrix[x, y].Equals("F"))
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

        private static void RecursionTest(Tuple<int, int> tuple, string[,] matrix, int pathCounter)
        {
            Queue<Tuple<int, int>> children = new Queue<Tuple<int, int>>();
            children.Enqueue(tuple); 
            
            List<(int x, int y)> visited = new List<(int x, int y)>();
            visited.Add((tuple.Item1, tuple.Item2));
            
            while (children.Count != 0)
            {
                var child = children.Peek();
                Tuple<int, int> right = CheckIfAsteroidRight(child.Item1, child.Item2, matrix);
                if (right != null)
                {
                    if (CheckIfFinal(right.Item1, right.Item2, matrix))
                    {
                        break;
                    }

                    if (!visited.Contains((right.Item1, right.Item2)))
                    {
                        Console.WriteLine("RIGHT");
                        children.Enqueue(right);
                        visited.Add((right.Item1, right.Item2));
                    }
                }
                Tuple<int, int> left = CheckIfAsteroidLeft(child.Item1, child.Item2, matrix);
                if (left != null)
                {
                    if (CheckIfFinal(left.Item1, left.Item2, matrix))
                    {
                        break;
                    }
                    if (!visited.Contains((left.Item1, left.Item2)))
                    {
                        Console.WriteLine("LEFT");
                        children.Enqueue(left);
                        visited.Add((left.Item1, left.Item2));
                    }
                }

                Tuple<int, int> up = CheckIfAsteroidUp(child.Item1, child.Item2, matrix);
                if (up != null)
                {
                    if (CheckIfFinal(up.Item1, up.Item2, matrix))
                    {
                        break;
                    }
                    if (!visited.Contains((up.Item1, up.Item2)))
                    {
                        Console.WriteLine("UP");
                        children.Enqueue(up);
                        visited.Add((up.Item1, up.Item2));
                    }
                }

                Tuple<int, int> down = CheckIfAsteroidDown(child.Item1, child.Item2, matrix);
                if (down != null)
                {
                    if (CheckIfFinal(down.Item1, down.Item2, matrix))
                    {
                        break;
                    }
                    if (!visited.Contains((down.Item1, down.Item2)))
                    {
                        Console.WriteLine("DOWN");
                        children.Enqueue(down);
                        visited.Add((down.Item1, down.Item2));
                    }
                }
                children.Dequeue();
            }
            Console.WriteLine(pathCounter);
        }

        public static void Main(string[] args)
        {
            Dictionary<string, (int, int)> astronautsDictionaryPosition = new Dictionary<string, (int, int)>();
            
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

                    if (matrixRowInput[j].Equals("F")) {
                        Final.x = i; 
                        Final.y = j;
                    }

                    matrix[i, j] = matrixRowInput[j];
                }
            }
            foreach (var entry in astronautsDictionaryPosition)
            {
                // TODO FINISH the algo
                int pathCounter = 0;
                Console.WriteLine($"{entry.Key}: {entry.Value.Item1}, {entry.Value.Item2}");
                RecursionTest(new Tuple<int, int>(entry.Value.Item1,entry.Value.Item2), matrix, pathCounter);
            }
            
            Console.WriteLine("DEBUGGER");
        }
    }
}