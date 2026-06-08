namespace SpaceApp;

public class HelperFuncs
{
    public void CheckSize(int  size) 
    {
        if (size < 2 || size > 100)
        {
            Environment.Exit(1);
        }
    }
    public bool CheckIfHitAsteroid(int x, int y, string[,] matrix)
    {
        if (matrix[x, y].Equals("X"))
        {
            return true;
        }

        return false;
    }
        
    public bool CheckIfFinal(int x, int y, string[,] matrix)
    {
        if (matrix[x, y].Equals("F"))
        {
            return true;
        }
        return false;
    }
        
    public bool CheckIfHitAstro(int x, int y, string[,] matrix, string[] Astronauts)
    {
        if (Astronauts.Any(matrix[x, y].Contains))
        {
            return true;
        }

        return false;
    }
    public Tuple<int, int> CheckIfAsteroidRight(int x, int y, string[,] matrix, string[] Astronauts)
    {
        y++;
        if (matrix.GetLength(1) - 1 < y)
        {
            return null;
        }
        if (CheckIfHitAsteroid(x, y, matrix)) return null;
        if (CheckIfHitAstro(x, y, matrix, Astronauts)) return null;
            
        return new Tuple<int, int>(x,y);
    }
    public Tuple<int, int> CheckIfAsteroidLeft(int x, int y, string[,] matrix, string[] Astronauts)
    {
        y--;
        if (y < 0)
        {
            return null;
        }
        if (CheckIfHitAsteroid(x, y, matrix)) return null;
        if (CheckIfHitAstro(x, y, matrix, Astronauts)) return null;
            
            
        return  new Tuple<int, int>(x,y);
            
    }
    public Tuple<int, int> CheckIfAsteroidDown(int x, int y, string[,] matrix, string[] Astronauts)
    {
        x++;
        if (x > matrix.GetLength(0) - 1)
        {
            return null;
        }
        if (CheckIfHitAsteroid(x, y, matrix)) return null;
        if (CheckIfHitAstro(x, y, matrix, Astronauts)) return null;
            

        return  new Tuple<int, int>(x,y);
    }
    public Tuple<int, int> CheckIfAsteroidUp(int x, int y, string[,] matrix, string[] Astronauts)
    {
        x--;
        if (x < 0)
        {
            return null;
        }
        if (CheckIfHitAsteroid(x, y, matrix)) return null;
        if (CheckIfHitAstro(x, y, matrix, Astronauts)) return null;

        return  new Tuple<int, int>(x,y);
    }
}