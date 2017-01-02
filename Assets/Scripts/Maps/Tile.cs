using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A tile on a map
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
public class Tile : MonoBehaviour
{
    public const int SIZE_X = 32;
    public const int SIZE_Y = 32;

    public SpriteRenderer spriteRenderer = null;

    public Constants.TileType Type = Constants.TileType.Normal;
    public Sprite Sprite = null;
    public List<Decal> Decals = new List<Decal>();
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Place the tile under Mario by default
        // Note: hydrakiller4000 - Later we'll need to set this dynamically (Mask, Mask 2, Fringe, etc.)
        spriteRenderer.sortingOrder = -1;
    }

    public void AddDecal(Sprite sprite, int zIndex)
    {
        GameObject decalComponent = new GameObject("Decal");
        Decal decal = decalComponent.AddComponent<Decal>();

        decalComponent.transform.SetParent(transform);

        decal.Sprite = Sprite;
        decal.spriteRenderer.sprite = Sprite;
        decal.ZIndex = zIndex;

        Decals.Add(decal);
    }
}
