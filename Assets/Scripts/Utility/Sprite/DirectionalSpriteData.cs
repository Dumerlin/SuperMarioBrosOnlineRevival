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
    public override FrameData[] this[PlayerMovement.FacingDirections direction]
    {
        get
        {
            switch (direction)
            {
                case PlayerMovement.FacingDirections.North: return North;
                case PlayerMovement.FacingDirections.NorthEast: return NorthEast;
                case PlayerMovement.FacingDirections.East: return East;
                case PlayerMovement.FacingDirections.SouthEast: return SouthEast;
                default:
                case PlayerMovement.FacingDirections.South: return South;
                case PlayerMovement.FacingDirections.SouthWest: return SouthWest;
                case PlayerMovement.FacingDirections.West: return West;
                case PlayerMovement.FacingDirections.NorthWest: return NorthWest;
            }
        }
    }
}
