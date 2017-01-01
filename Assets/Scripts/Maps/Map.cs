using UnityEngine;

/// <summary>
/// A map on the game. The player can only be on a single map at a time
/// </summary>
public class Map : MonoBehaviour
{
    private const int DEFAULT_SIZE_X = 32;
    private const int DEFAULT_SIZE_Y = 32;

    public int ID = 0;
    public string Title = "";
    public Constants.MapType Type = Constants.MapType.Normal;
    public Vector2 Size = new Vector2(DEFAULT_SIZE_X, DEFAULT_SIZE_Y);
    public Tile[,] Tiles = null;
    
    public void SetSize(int x, int y)
    {
        Size  = new Vector2(x, y);
        Tiles = new Tile[x, y];
    }
}
