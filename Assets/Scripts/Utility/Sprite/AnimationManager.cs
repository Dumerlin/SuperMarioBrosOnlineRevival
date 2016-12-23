using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles animations for an object.
/// </summary>
[DisallowMultipleComponent]
public class AnimationManager : MonoBehaviour
{
    /// <summary>
    /// The name of the current animation being played.
    /// </summary>
    public string CurrentAnim { get; private set; }

    /// <summary>
    /// Plays an animation by name.
    /// </summary>
    /// <param name="animName">The name of the animation to play.</param>
    /// <param name="resetIfPlaying">Whether to reset the animation if it's currently playing.</param>
    public void PlayAnimation(string animName, bool resetIfPlaying = false)
    {

    }
}
