using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A decal on a tile
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
public class Decal : MonoBehaviour
{
    private const int DEFAULT_SIZE_X = 16;
    private const int DEFAULT_SIZE_Y = 16;

    public Sprite Sprite = null;
    public long ZIndex = 0;

    /// <summary>
    /// The tile to which the decal belongs
    /// </summary>
    public Tile Tile;
}
