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
    private const int SIZE_X = 32;
    private const int SIZE_Y = 32;

    public SpriteRenderer spriteRenderer = null;

    private Vector2 Size = new Vector2(SIZE_X, SIZE_Y);

    public Constants.TileType Type = Constants.TileType.Normal;
    public Sprite Sprite = null;
    public List<Decal> Decals = new List<Decal>();
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
