using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simple sprite animation class.
/// This should be used as a base and be extended with other functionality (namely Characters, as they use different sprites for each direction)
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
[DisallowMultipleComponent]
public class SpriteAnimation : MonoBehaviour
{
    /// <summary>
    /// The array of sprites.
    /// </summary>
    public Sprite[] Sprites;

    /// <summary>
    /// How many frames to display each Sprite.
    /// This sets the rate at which the animation plays.
    /// </summary>
    public int FramesPerSprite;

    /// <summary>
    /// The sprite renderer. Retrieved on Awake()
    /// </summary>
    protected SpriteRenderer spriteRenderer;

    /// <summary>
    /// Tracks when the next sprite should be displayed.
    /// </summary>
    protected int FrameTracker;

    /// <summary>
    /// The current index of the sprite being rendered.
    /// </summary>
    protected int CurIndex;

    /// <summary>
    /// Gets the current sprite being displayed.
    /// </summary>
    protected Sprite CurSprite { get { return Sprites[CurIndex]; } }

    // Use this for initialization
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        CurIndex = 0;
        FrameTracker = Time.frameCount + FramesPerSprite;

        if (Sprites == null || Sprites.Length == 0)
        {
            Debug.LogError("There are no sprites to animate!");
        }
        else
        {
            spriteRenderer.sprite = CurSprite;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //No sprites to display, return
        if (Sprites == null || Sprites.Length == 0) return;

        //Check if the sprite has been up for enough frames
        if (Time.frameCount >= FrameTracker)
        {
            //Update the index and wrap around
            CurIndex++;
            CurIndex %= Sprites.Length;

            //Update the sprite rendered
            spriteRenderer.sprite = CurSprite;

            //Update the frame count
            FrameTracker = Time.frameCount + FramesPerSprite;
        }
    }
}
