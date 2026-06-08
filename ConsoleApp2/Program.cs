using System.Collections;

namespace ConsoleApplication1
{
    internal class Space {
        private static string[] Astronauts = {"S1", "S2", "S3"};
        private static Tuple<int, int> _final;
        
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

        private static Dictionary<Tuple<int, int>,Tuple<int, int>> searchBFS(Tuple<int, int> tuple, string[,] matrix)
        {
            Queue<Tuple<int, int>> storageBfs = new Queue<Tuple<int, int>>();
            storageBfs.Enqueue(tuple); 
            
            List<Tuple<int, int>> visited = new List<Tuple<int, int>>();
            Dictionary<Tuple<int, int>,Tuple<int, int>> path = new Dictionary<Tuple<int, int>,Tuple<int, int>>();
            visited.Add(tuple);
            
            while (storageBfs.Count != 0)
            {
                var parent = storageBfs.Peek();
                var right = CheckIfAsteroidRight(parent.Item1, parent.Item2, matrix);
                var left = CheckIfAsteroidLeft(parent.Item1, parent.Item2, matrix);
                var up = CheckIfAsteroidUp(parent.Item1, parent.Item2, matrix);
                var down = CheckIfAsteroidDown(parent.Item1, parent.Item2, matrix);
                
                List<Tuple<int, int>> children =[right, down, left, up];
                
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        if (CheckIfFinal(child.Item1, child.Item2, matrix))
                        {
                            path.Add(child, parent);
                            storageBfs.Clear();
                            visited.Clear();
                            break;
                        }
                        if (!visited.Contains(child))
                        {
                            path.Add(child, parent);
                            storageBfs.Enqueue(child);
                            visited.Add(child);
                        }
                    }
                }

                if (storageBfs.Count != 0)
                {
                    storageBfs.Dequeue();   
                }
            }

            return path;
        }

        private static int getSteps(Dictionary<Tuple<int, int>,Tuple<int, int>> path, string[,] matrix, Tuple<int, int> astronaut, int pathCounter)
        {
            var current = _final;
            
            while (current.Item1 != astronaut.Item1
                   || current.Item2 != astronaut.Item2)
            {
                pathCounter++;
                var parent = path[current];
                current = parent;
                if (current.Equals(astronaut))
                {
                    break;
                }
                matrix[current.Item1, current.Item2] = "*";
            }
            return pathCounter;
        }

        private static void PrintMatrix(string[,] matrix)
        {
            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
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

                    if (matrixRowInput[j].Equals("F"))
                    {
                        _final = new Tuple<int, int>(i, j);
                    }

                    matrix[i, j] = matrixRowInput[j];
                }
            }
            
            SortedDictionary<int, Tuple<string, string[,]>> everything = new SortedDictionary<int, Tuple<string, string[,]>>();
            
            foreach (var entry in astronautsDictionaryPosition)
            {
                var matrixCopy = matrix.Clone() as string[,]; //HAHAHAHAHHAHA PASS BY REFERENCE, took me some time to release, ...........
                var pathCounter = 0;
                var dict = searchBFS(new Tuple<int, int>(entry.Value.Item1,entry.Value.Item2), matrixCopy);
                pathCounter = getSteps(dict, matrixCopy, new Tuple<int, int>(entry.Value.Item1,entry.Value.Item2), pathCounter);
                everything.Add(pathCounter, new Tuple<string, string[,]>(entry.Key, matrixCopy));
            }

            foreach (var entry in everything)
            {
                Console.WriteLine("Astronaut " + entry.Value.Item1 + " - Shortest path: " + entry.Key);
                PrintMatrix(entry.Value.Item2);
            }
        }
    }
}