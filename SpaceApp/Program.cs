using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

/* Overall what is doing. We accept data from input row by row. We store it in matrix. Then with BFS search, we try to find the final
 point by checking for each node, the children. We check for visited so we don't go back,  we store the path because when we reach the final if there's even.
 we don't know how we ended up there. So we keep track for each node the parent. Then we create the path from beginning to end + number of steps. Get the shortest path
 between astronauts, print and finito.
 */


namespace SpaceApp
{
    internal class Space
    {
        private static string[] _astronauts = { "S1", "S2", "S3" };
        private static Tuple<int, int> _final;

        private static void SearchBfs(Astronaut astronaut, HelperFuncs helperFuncs)
        {
            string[,] matrix = astronaut.Matrix;
            Tuple<int, int> astronautStartPosition = new Tuple<int, int>(astronaut.PositionX, astronaut.PositionY);
            Queue<Tuple<int, int>> storageBfs = new Queue<Tuple<int, int>>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();
            Dictionary<Tuple<int, int>, Tuple<int, int>> path = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
            path.Clear();

            storageBfs.Enqueue(astronautStartPosition);
            visited.Add(astronautStartPosition);

            while (storageBfs.Count != 0)
            {
                var parent = storageBfs.Peek();

                // Check the 4 directions for possible open space
                var right = helperFuncs.CheckIfAsteroidRight(parent.Item1, parent.Item2, matrix, _astronauts);
                var left = helperFuncs.CheckIfAsteroidLeft(parent.Item1, parent.Item2, matrix, _astronauts);
                var up = helperFuncs.CheckIfAsteroidUp(parent.Item1, parent.Item2, matrix, _astronauts);
                var down = helperFuncs.CheckIfAsteroidDown(parent.Item1, parent.Item2, matrix, _astronauts);

                List<Tuple<int, int>> children = [right, down, left, up];

                // Check if each child(Open space) from parent, has reached final, if not add him to visited and to the queue so we can check its children etc.  
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

                // if from start no children it throws error, could put some error handling ?
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
                // iterate from final point to the start to create the path from starts *
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
            astronaut.StepsShortestPath = pathCounter;
        }

        private static StringBuilder PrintMatrix(string[,] matrix, StringBuilder sb, bool reachFinal)
        {
            if (reachFinal)
            {
                int rowLength = matrix.GetLength(0);
                int colLength = matrix.GetLength(1);
                for (int i = 0; i < rowLength; i++)
                {
                    for (int j = 0; j < colLength; j++)
                    {
                        sb.Append(matrix[i, j] + " ");
                        Console.Write(matrix[i, j] + " ");
                    }

                    sb.AppendLine();
                    Console.WriteLine();
                }
            }

            return sb;
        }

        private static StringBuilder PrintForEachAstronautInOrder(SortedDictionary<int, Astronaut> astronautsByStepsShortestPath)
        {
            var sb = new StringBuilder();
            // For each astronaut we print the matrix, already sorted because of SortedDictionary
            foreach (var entry in astronautsByStepsShortestPath)
            {
                string title = "Mission failed — Astronaut " + entry.Value.Name + " lost in space!";
                if (entry.Value.IsReachFinal)
                {
                    title = "Astronaut " + entry.Value.Name + " - Shortest path: " +
                            entry.Value.StepsShortestPath;
                }
                sb.Append(title);
                sb.AppendLine();
                Console.WriteLine(title);
                sb = PrintMatrix(entry.Value.Matrix, sb, entry.Value.IsReachFinal);
            }

            return sb;
        }

        public static void Main(string[] args)
        {
            var astronauts = new List<Astronaut>();
            var helperFuncs = new HelperFuncs();

            // Input for matrix size - X and Y
            Console.Write("Map rows: ");
            var rowsInput = Convert.ToInt32(Console.ReadLine());
            helperFuncs.CheckSize(rowsInput);

            Console.Write("Map columns: ");
            var colsInput = Convert.ToInt32(Console.ReadLine());
            helperFuncs.CheckSize(colsInput);


            string[,] matrix = new string[rowsInput, colsInput];
            Console.WriteLine("Cosmic map: ");
            // iterate for each row we accept line of data, we add it to the matrix then we go to next row and same thing again until we hit size of rows
            for (var i = 0; i < rowsInput; i++)
            {
                string[] matrixRowInput = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.None);
                for (var j = 0; j < colsInput; j++)
                {
                    if (_astronauts.Any(matrixRowInput[j].Contains))
                    {
                        astronauts.Add(new Astronaut(matrixRowInput[j], false, i, j, 0));
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
                entry.Matrix =
                    matrix.Clone() as string[,]; //HAHAHAHAHHAHA PASS BY REFERENCE, took me some time to release, ...........
                SearchBfs(entry, helperFuncs);
                SetStepsAndMatrixPath(entry);
                astronautsByStepsShortestPath.Add(entry.StepsShortestPath, entry);
            }
            
            // finale part
            SendEmail(astronautsByStepsShortestPath);
        }

        private static void SendEmail(SortedDictionary<int, Astronaut> astronautsByStepsShortestPath)
        {
            var senderEmail = Console.ReadLine();
            var senderPassword = Console.ReadLine();
            var receiverEmail = Console.ReadLine();
            
            SmtpClient smtpObj = new SmtpClient("smtp.gmail.com");
            
            // set smtp-client with basicAuthentication
            smtpObj.Port = 587;
            smtpObj.EnableSsl = true;
            smtpObj.UseDefaultCredentials = false;
            NetworkCredential basicAuthenticationInfo = new NetworkCredential(senderEmail, senderPassword);
            smtpObj.Credentials = basicAuthenticationInfo;

            
            MailAddress from = new MailAddress(senderEmail, senderEmail);
            MailAddress to = new MailAddress(receiverEmail,receiverEmail);
            MailMessage myMail = new MailMessage(from, to);

            // set subject and encoding
            myMail.Subject = "Space Game Hitachi something";
            myMail.SubjectEncoding = Encoding.UTF8;

            // set body-message and encoding
            myMail.Body = PrintForEachAstronautInOrder(astronautsByStepsShortestPath).ToString();
            myMail.BodyEncoding = Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = false;

            smtpObj.Send(myMail);
        }
    }
}