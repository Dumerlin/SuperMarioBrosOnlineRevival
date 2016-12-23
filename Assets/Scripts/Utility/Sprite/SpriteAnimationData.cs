using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Stores animation data.
/// </summary>
[CreateAssetMenu(fileName = "SpriteAnimationData", menuName = "SMBO/Create Sprite Animation Data")]
public class SpriteAnimationData : AnimationData
{
    /// <summary>
    /// The set of sprites.
    /// </summary>
    public FrameData[] Frames = null;

    public override FrameData[] this[PlayerMovement.FacingDirections direction]
    {
        //Always return these sprites regardless of direction
        get { return Frames; }
    }
}