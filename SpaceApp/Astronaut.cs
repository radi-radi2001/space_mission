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
    public Dictionary<Tuple<int, int>, Tuple<int, int>> Path
    {
        get;
        set;
    }
    public string[,] Matrix
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public bool IsReachFinal
    {
        get;
        set;
    }
    public int PositionX
    {
        get;
        set;
    }
    public int PositionY
    {
        get;
        set;
    }
    public int StepsShortestPath
    {
        get;
        set;
    }
}