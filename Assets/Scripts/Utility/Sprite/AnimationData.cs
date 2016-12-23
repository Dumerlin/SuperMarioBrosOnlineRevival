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
    /// The constant dictating when an animation should loop infinitely
    /// </summary>
    public const int INFINITE_LOOP = 0;

    /// <summary>
    /// The name of the Animation.
    /// </summary>
    public string AnimName = string.Empty;

    /// <summary>
    /// The number of frames to display each Sprite.
    /// This sets the rate at which the animation plays.
    /// </summary>
    public int FramesPerSprite = 6;

    /// <summary>
    /// Whether to loop the animation or not.
    /// </summary>
    public bool Loop = false;

    /// <summary>
    /// How many time to loop the animation if it loops.
    /// </summary>
    public int LoopTimes = INFINITE_LOOP;

    /// <summary>
    /// Gets a FrameData array from a direction.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public abstract FrameData[] this[PlayerDirection.FacingDirections direction] { get; }

    [Serializable]
    public class FrameData
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
