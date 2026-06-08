namespace ConsoleApplication1;

public class Astronaut
{
    public Astronaut()
    {
        
    }
    public Astronaut(string name, bool isReachFinal, int positionX, int positionY, int stepsShortestPath)
    {
        this._name = name;
        this._isReachFinal = isReachFinal;
        this._positionX = positionX;
        this._positionY = positionY;
        this._stepsShortestPath = stepsShortestPath;
    }
    
    private string[,] _matrix;
    public string[,] Matrix
    {
        get => _matrix;
        set => _matrix = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    private string _name;
    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    private bool _isReachFinal;
    public bool IsReachFinal
    {
        get;
        set;
    }


    private int _positionX;
    public int PositionX
    {
        get;
        set;
    }

    private int _positionY;
    public int PositionY
    {
        get;
        set;
    }

    private int _stepsShortestPath;
    public int StepsShortestPath
    {
        get;
        set;
    }
}