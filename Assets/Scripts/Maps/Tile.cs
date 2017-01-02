using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public void AddDecal(Decal decal)
    {
        decal.Tile = this;
        Decals.Add(decal);
    }

    private void DisplayDecals()
    {
        List<Decal> orderedDecals = Decals.OrderByDescending(d => d.ZIndex).ToList();

        foreach (Decal decal in orderedDecals)
        {

        }
    }
}
