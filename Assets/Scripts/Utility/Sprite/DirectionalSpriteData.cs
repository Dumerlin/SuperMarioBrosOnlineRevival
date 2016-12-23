using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data for a directional animation. Individually defined to make it easier to edit in the Inspector.
/// </summary>
[CreateAssetMenu(fileName = "DirectionalAnimationData", menuName = "SMBO/Create Directional Animation Data", order = 1)]
public class DirectionalSpriteData : AnimationData
{
    /// <summary>
    /// The sets of sprites.
    /// </summary>
    public FrameData[] North = null;
    public FrameData[] NorthEast = null;
    public FrameData[] East = null;
    public FrameData[] SouthEast = null;
    public FrameData[] South = null;
    public FrameData[] SouthWest = null;
    public FrameData[] West = null;
    public FrameData[] NorthWest = null;

    //Return the appropriate array based off the direction
    public override FrameData[] this[PlayerDirection.FacingDirections direction]
    {
        get
        {
            switch (direction)
            {
                case PlayerDirection.FacingDirections.North: return North;
                case PlayerDirection.FacingDirections.NorthEast: return NorthEast;
                case PlayerDirection.FacingDirections.East: return East;
                case PlayerDirection.FacingDirections.SouthEast: return SouthEast;
                default:
                case PlayerDirection.FacingDirections.South: return South;
                case PlayerDirection.FacingDirections.SouthWest: return SouthWest;
                case PlayerDirection.FacingDirections.West: return West;
                case PlayerDirection.FacingDirections.NorthWest: return NorthWest;
            }
        }
    }
}
