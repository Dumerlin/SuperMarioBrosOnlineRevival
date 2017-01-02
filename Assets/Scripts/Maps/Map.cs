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

    // The map that will be loaded when the user exits the map from the top, left, bottom, and right, respectively
    public int TopMapID;
    public int LeftMapID;
    public int BottomMapID;
    public int RightMapID;

    public bool TopMapExists    { get { return TopMapID > 0; } }
    public bool LeftMapExists   { get { return LeftMapID > 0; } }
    public bool BottomMapExists { get { return BottomMapID > 0; } }
    public bool RightMapExists  { get { return RightMapID > 0; } }

    protected override void Awake()
    {
        base.Awake();
        SetSize((int)Size.x, (int)Size.y);
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
    }
}
