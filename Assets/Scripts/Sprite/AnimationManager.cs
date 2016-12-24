using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles animations for an object.
/// </summary>
[RequireComponent(typeof(AnimationPlayer))]
[DisallowMultipleComponent]
public class AnimationManager : MonoBehaviour
{
    /// <summary>
    /// All the animations stored by name. Make sure each animation has a unique name.
    /// </summary>
    private readonly Dictionary<string, AnimationData> AnimDict = new Dictionary<string, AnimationData>();

    /// <summary>
    /// The path to load the animations from.
    /// </summary>
    public string AnimationPath = string.Empty;

    /// <summary>
    /// The data of the previous animation that was played.
    /// </summary>
    public AnimationData PreviousAnim { get; private set; }

    /// <summary>
    /// The data of the current animation being played.
    /// </summary>
    public AnimationData CurrentAnim { get; private set; }

    /// <summary>
    /// The AnimationPlayer used to play animations.
    /// </summary>
    private AnimationPlayer AnimPlayer = null;

    /// <summary>
    /// The direction the object is facing.
    /// </summary>
    private PlayerDirection playerDirection = null;

    private PlayerDirection.FacingDirections PrevDirection = PlayerDirection.FacingDirections.South;

    private void Awake()
    {
        AnimPlayer = GetComponent<AnimationPlayer>();
        playerDirection = GetComponent<PlayerDirection>();

        //Load all animations
        AnimationData[] animations = Resources.LoadAll<AnimationData>(AnimationPath);

        for (int i = 0; i < animations.Length; i++)
        {
            //Don't add duplicate names
            if (AnimDict.ContainsKey(animations[i].AnimName) == false)
            {
                AnimDict.Add(animations[i].AnimName, animations[i]);
            }
        }

        Debug.Log("Found " + animations.Length + " animations at " + AnimationPath);
    }

    /// <summary>
    /// Plays an animation by name.
    /// </summary>
    /// <param name="animName">The name of the animation to play.</param>
    //// <param name="directionFacing">The direction of the animation to play.</param>
    public void PlayAnimation(string animName)
    {
        if (AnimDict.ContainsKey(animName) == false)
        {
            Debug.LogWarning("No animation with name: " + animName + " can be found!");
            return;
        }

        PlayerDirection.FacingDirections directionFacing =
            (playerDirection != null) ? playerDirection.CurDirection : PlayerDirection.FacingDirections.South;

        //Don't reset the current animation if its the same and the direction it's facing is the same
        if (CurrentAnim != null && CurrentAnim.AnimName == animName && PrevDirection == directionFacing) return;

        PreviousAnim = CurrentAnim;
        CurrentAnim = AnimDict[animName];
        PrevDirection = directionFacing;

        AnimPlayer.SetData(CurrentAnim, directionFacing);
    }
}
