using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays an animation given the data put in. Handles looping and flipping.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
public sealed class AnimationPlayer : MonoBehaviour
{
    /// <summary>
    /// The SpriteRenderer.
    /// </summary>
    private SpriteRenderer spriteRenderer = null;

    /// <summary>
    /// The current animation playing.
    /// </summary>
    private AnimationData AnimationPlaying = null;

    /// <summary>
    /// The frames of the animation.
    /// </summary>
    private AnimationData.FrameData[] Frames = null;

    /// <summary>
    /// Tracks when the next sprite should be displayed.
    /// </summary>
    private int FrameTracker = 0;

    /// <summary>
    /// The current index of the sprite being rendered.
    /// </summary>
    private int CurIndex = 0;

    /// <summary>
    /// The max number of loops of the animation. If reached, the animation stops on the last frame.
    /// </summary>
    private int MaxLoops = 0;

    /// <summary>
    /// The current number of loops the animation completed.
    /// </summary>
    private int CurLoops = 0;

    /// <summary>
    /// Gets the current frame being displayed.
    /// </summary>
    private AnimationData.FrameData CurFrame { get { return Frames[CurIndex]; } }

    /// <summary>
    /// Whether the animation has finished playing or not.
    /// </summary>
    public bool Finished { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetData(AnimationData newAnimation, PlayerDirection.FacingDirections direction)
    {
        if (newAnimation == null)
        {
            Debug.LogWarning("null AnimationData passed to SetData. Not playing animation");
            return;
        }

        if (newAnimation[direction] == null || newAnimation[direction].Length == 0)
        {
            Debug.LogWarning("animation " + newAnimation.AnimName + " has no direction data for " + direction + ". Not playing animation");
            return;
        }

        AnimationPlaying = newAnimation;
        Frames = AnimationPlaying[direction];
        ResetAnim();
    }

    public void ResetAnim()
    {
        Finished = false;
        CurIndex = CurLoops = 0;
        FrameTracker = Time.frameCount + AnimationPlaying.FramesPerSprite;

        spriteRenderer.sprite = CurFrame.Sprite;
        spriteRenderer.flipX = CurFrame.Flipped;
    }

    private void Update()
    {
        //No sprites to display, return
        if (Frames == null || Frames.Length == 0) return;

        //Check if the sprite has been up for enough frames
        if (Finished == false && Time.frameCount >= FrameTracker)
        {
            //Update the index
            CurIndex++;

            //After going through all the animation frames...
            if (CurIndex >= Frames.Length)
            {
                //If the animation shoud loop, increment the number of loops
                if (AnimationPlaying.Loop == true)
                {
                    //Don't increment if the animation goes on forever.
                    if (MaxLoops > AnimationData.INFINITE_LOOP)
                    {
                        CurLoops++;
                    }

                    //If we're done with loops, end the animation
                    if (MaxLoops > AnimationData.INFINITE_LOOP && CurLoops >= MaxLoops)
                    {
                        Finished = true;
                        CurIndex = Frames.Length - 1;
                        return;
                    }
                    //Otherwise restart
                    else
                    {
                        CurIndex = 0;
                    }
                }
                else
                {
                    CurIndex = Frames.Length - 1;
                }
            }

            //Update the sprite rendered and flip if necessary
            spriteRenderer.sprite = CurFrame.Sprite;
            spriteRenderer.flipX = CurFrame.Flipped;

            //Update the frame count
            FrameTracker = Time.frameCount + AnimationPlaying.FramesPerSprite;
        }
    }
}
