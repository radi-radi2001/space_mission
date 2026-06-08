namespace SpaceApp
{
    internal class Space {
        private static string[] _astronauts = {"S1", "S2", "S3"};
        private static Tuple<int, int> _final;
        
        private static void SearchBfs(Astronaut astronaut, HelperFuncs  helperFuncs)
        {
            string [,] matrix = astronaut.Matrix;
            Tuple<int, int> astronautStartPosition = new Tuple<int, int>(astronaut.PositionX, astronaut.PositionY);
            Queue<Tuple<int, int>> storageBfs = new Queue<Tuple<int, int>>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
            Dictionary<Tuple<int, int>,Tuple<int, int>> path = new Dictionary<Tuple<int, int>,Tuple<int, int>>();
            path.Clear();
            
            storageBfs.Enqueue(astronautStartPosition);
            visited.Add(astronautStartPosition);
            
            while (storageBfs.Count != 0)
            {
                var parent = storageBfs.Peek();
                var right = helperFuncs.CheckIfAsteroidRight(parent.Item1, parent.Item2, matrix, _astronauts);
                var left = helperFuncs.CheckIfAsteroidLeft(parent.Item1, parent.Item2, matrix, _astronauts);
                var up = helperFuncs.CheckIfAsteroidUp(parent.Item1, parent.Item2, matrix, _astronauts);
                var down = helperFuncs.CheckIfAsteroidDown(parent.Item1, parent.Item2, matrix, _astronauts);
                
                List<Tuple<int, int>> children =[right, down, left, up];
                
                foreach (var child in children)
                {
                    if (child != null)
                    {
                        if (helperFuncs.CheckIfFinal(child.Item1, child.Item2, matrix))
                        {
                            astronaut.IsReachFinal = true;
                            path.Add(child, parent);
                            storageBfs.Clear();
                            visited.Clear();
                            astronaut.Path = new Dictionary<Tuple<int, int>, Tuple<int, int>>(path);
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
        }

        private static void SetStepsAndMatrixPath(Astronaut astronaut)
        {
            string[,] matrix = astronaut.Matrix;
            int pathCounter = 0;
            bool reachFinal = astronaut.IsReachFinal;
            var path = astronaut.Path;
            
            if (reachFinal)
            {
                var current = _final;

                while (current.Item1 != astronaut.PositionX
                       || current.Item2 != astronaut.PositionY)
                {
                    pathCounter++;
                    var parent = path[current];
                    current = parent;
                    if (current.Equals(new Tuple<int, int>(astronaut.PositionX, astronaut.PositionY)))
                    {
                        break;
                    }

                    matrix[current.Item1, current.Item2] = "*";
                }
            }
            astronaut.Matrix = matrix;
            astronaut.StepsShortestPath =  pathCounter;
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

        private static void PrintForEachAstronautInOrder(SortedDictionary<int, Astronaut> astronautsByStepsShortestPath)
        {
            foreach (var entry in astronautsByStepsShortestPath)
            {
                if (entry.Value.IsReachFinal)
                {
                    Console.WriteLine("Astronaut " + entry.Value.Name + " - Shortest path: " + entry.Value.StepsShortestPath);
                    PrintMatrix(entry.Value.Matrix);   
                }
                else
                {
                    Console.WriteLine("Mission failed — Astronaut " + entry.Value.Name + " lost in space! ");
                }
            }
        }
        
        public static void Main(string[] args)
        {
            var astronauts = new List<Astronaut>();
            var helperFuncs = new HelperFuncs();
            
            Console.Write("Map rows: ");
            var rowsInput = Convert.ToInt32(Console.ReadLine());
            helperFuncs.CheckSize(rowsInput);

            Console.Write("Map columns: ");
            var colsInput = Convert.ToInt32(Console.ReadLine());
            helperFuncs.CheckSize(colsInput);


            string[,] matrix = new string[rowsInput, colsInput];
            // Matrix 2 dimension
            Console.WriteLine("Cosmic map: ");
            for (var i = 0; i < rowsInput; i++)
            {
                string[] matrixRowInput = Console.ReadLine().Split(new[] {" "}, StringSplitOptions.None);
                for (var j = 0; j < colsInput; j++)
                {
                    if(_astronauts.Any(matrixRowInput[j].Contains)){
                        astronauts.Add(new Astronaut(matrixRowInput[j], false , i, j, 0));
                    }

                    if (matrixRowInput[j].Equals("F"))
                    {
                        _final = new Tuple<int, int>(i, j);
                    }

                    matrix[i, j] = matrixRowInput[j];
                }
            }
            
            SortedDictionary<int, Astronaut> astronautsByStepsShortestPath = new SortedDictionary<int, Astronaut>();
            foreach (var entry in astronauts)
            {
                entry.Matrix = matrix.Clone() as string[,]; //HAHAHAHAHHAHA PASS BY REFERENCE, took me some time to release, ...........
                SearchBfs(entry, helperFuncs);
                SetStepsAndMatrixPath(entry);
                astronautsByStepsShortestPath.Add(entry.StepsShortestPath, entry);
                
            }

            PrintForEachAstronautInOrder(astronautsByStepsShortestPath);
        }
    }
}