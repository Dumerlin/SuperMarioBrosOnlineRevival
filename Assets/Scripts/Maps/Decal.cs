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

    public SpriteRenderer spriteRenderer = null;

    public Sprite Sprite = null;
    public int ZIndex = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sortingOrder = ZIndex;
    }
}
