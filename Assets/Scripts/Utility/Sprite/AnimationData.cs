using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for defining animation data.
/// </summary>
public abstract class AnimationData : ScriptableObject
{
    /// <summary>
    /// The number of frames to display each Sprite.
    /// This sets the rate at which the animation plays.
    /// </summary>
    public int FramesPerSprite = 6;

    /// <summary>
    /// Whether the animation should flip the Sprites or not.
    /// </summary>
    //public bool Flipped = false;

    /// <summary>
    /// Gets a FrameData array from a direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public abstract FrameData[] this[PlayerMovement.FacingDirections direction] { get; }

    [Serializable]
    public struct FrameData
    {
        /// <summary>
        /// The sprite of this frame.
        /// </summary>
        public Sprite Sprite;

        /// <summary>
        /// Whether the sprite is flipped or not.
        /// </summary>
        public bool Flipped;

        public FrameData(Sprite sprite, bool flipped)
        {
            Sprite = sprite;
            Flipped = flipped;
        }
    }
}
