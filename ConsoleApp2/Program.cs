using System.Collections;

namespace ConsoleApplication1
{
    internal class Space {
        private static string[] Astronauts = {"S1", "S2", "S3"};
        private static (int x, int y) _final;
        
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
            
            List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
            Dictionary<Tuple<int, int>,Tuple<int, int>> path = new Dictionary<Tuple<int, int>,Tuple<int, int>>();
            visited.Add(tuple);
            
            while (children.Count != 0)
            {
                var child = children.Peek();
                var right = CheckIfAsteroidRight(child.Item1, child.Item2, matrix);
                var left = CheckIfAsteroidLeft(child.Item1, child.Item2, matrix);
                var up = CheckIfAsteroidUp(child.Item1, child.Item2, matrix);
                var down = CheckIfAsteroidDown(child.Item1, child.Item2, matrix);
                
                List<Tuple<int, int>> directions =[right, down, left, up];
                foreach (var direction in directions)
                {
                    if (direction != null)
                    {
                        if (CheckIfFinal(direction.Item1, direction.Item2, matrix))
                        {
                            break;
                        }
                        if (!visited.Contains((direction)))
                        {
                            children.Enqueue(direction);
                            path.Add(child,direction);
                            visited.Add((direction));
                        }
                    }
                }
                children.Dequeue();
            }

            Console.WriteLine("DEbuuger");
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
            for (var i = 0; i < rowsInput; i++)
            {
                string[] matrixRowInput = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.None);
                for (var j = 0; j < colsInput; j++)
                {
                    if(Astronauts.Any(matrixRowInput[j].Contains)){
                        astronautsDictionaryPosition.Add(matrixRowInput[j], (i,j));
                    }

                    if (matrixRowInput[j].Equals("F")) {
                        _final.x = i; 
                        _final.y = j;
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