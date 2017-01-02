using UnityEngine;

/// <summary>
/// A map on the game. The player can only be on a single map at a time
/// </summary>
public class Map : Singleton<Map>
{
    private const int DEFAULT_WIDTH = 32;
    private const int DEFAULT_HEIGHT = 32;

    public int ID = 1;
    public string Title = "";
    public Constants.MapType Type = Constants.MapType.Normal;
    public Vector2 Size = new Vector2();
    public Tile[,] Tiles = null;

    public Rect Bounds { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        SetSize(DEFAULT_WIDTH, DEFAULT_HEIGHT);
    }
    
    public void SetSize(int width, int height)
    {
        Size  = new Vector2(width, height);
        Tiles = new Tile[width, height];

        float x = (-Size.x / 2) * Tile.SIZE_X;
        float y = (-Size.y / 2)* Tile.SIZE_Y;
        float right = Size.x * Tile.SIZE_X;
        float bottom = Size.y * Tile.SIZE_Y;

        Bounds = new Rect(x, y, right, bottom);

        Debug.Log(Bounds);
    }
}
