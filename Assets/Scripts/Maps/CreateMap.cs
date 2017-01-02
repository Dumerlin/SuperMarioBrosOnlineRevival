using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    private Sprite[] TileSheetSprites { get; set; }

    private void Start()
    {
        TileSheetSprites = Resources.LoadAll<Sprite>("Sprites/Tiles0");
        Create();
    }

    public void Create()
    {
        GameObject mapComponent = new GameObject("Map");
        Map map = mapComponent.AddComponent<Map>();

        for (int x = 0; x < map.Size.x; x++)
        {
            for (int y = 0; y < map.Size.y; y++)
            {
                CreateTile(mapComponent, x, y);
            }
        }

        // Center the map on screen
        mapComponent.transform.position = new Vector3(-map.Bounds.xMax / 32, -map.Bounds.yMax / 32);
    }

    private void CreateTile(GameObject mapComponent, int x, int y)
    {
        GameObject tileComponent = new GameObject("Tile");
        Tile tile = tileComponent.AddComponent<Tile>();

        tileComponent.transform.SetParent(mapComponent.transform);

        int tileIndex = Util.RandNum.Next(1, TileSheetSprites.Length);

        Sprite sprite = TileSheetSprites[tileIndex];
        tile.Sprite = sprite;
        tile.spriteRenderer.sprite = sprite;

        Map map = mapComponent.GetComponent<Map>();
        map.Tiles[x, y] = tile;

        // Divide by 32 because that's the scaling on the camera
        float xPos = (x * map.Size.x) / 32;
        float yPos = (y * map.Size.y) / 32;

        tileComponent.transform.position = new Vector3(xPos, yPos);
    }
}
