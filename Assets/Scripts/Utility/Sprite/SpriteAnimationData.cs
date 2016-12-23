using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Stores animation data.
/// </summary>
[CreateAssetMenu(fileName = "SpriteAnimationData", menuName = "SMBO/Create Sprite Animation Data")]
public class SpriteAnimationData : ScriptableObject
{
    /// <summary>
    /// The set of sprites.
    /// </summary>
    public Sprite[] Sprites = null;

    /// <summary>
    /// The number of frames to display each Sprite.
    /// This sets the rate at which the animation plays.
    /// </summary>
    public int FramesPerSprite = 6;

    /// <summary>
    /// Whether the animation should flip the Sprites or not.
    /// </summary>
    public bool Flipped = false;
}