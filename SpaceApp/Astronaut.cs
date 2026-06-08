namespace SpaceApp;

public class Astronaut
{
    public Astronaut()
    {
        
    }
    public Astronaut(string name, bool isReachFinal, int positionX, int positionY, int stepsShortestPath)
    {
        Name = name;
        IsReachFinal = isReachFinal;
        PositionX = positionX;
        PositionY = positionY;
        StepsShortestPath = stepsShortestPath;
        Path = new Dictionary<Tuple<int,int>, Tuple<int,int>>();
    }
    
    private Dictionary<Tuple<int, int>, Tuple<int, int>> path;
    public Dictionary<Tuple<int, int>, Tuple<int, int>> Path
    {
        get;
        set;
    }
    private string[,] _matrix;
    public string[,] Matrix
    {
        get;
        set;
    }
    
    private string name;
    public string Name
    {
        get;
        set;
    }

    private bool isReachFinal;
    public bool IsReachFinal
    {
        get;
        set;
    }


    private int positionX;
    public int PositionX
    {
        get;
        set;
    }

    private int positionY;
    public int PositionY
    {
        get;
        set;
    }

    private int stepsShortestPath;
    public int StepsShortestPath
    {
        get;
        set;
    }
}