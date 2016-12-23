using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tells the direction the player is facing.
/// </summary>
public sealed class PlayerDirection : MonoBehaviour
{
    public delegate void DirectionChanged(FacingDirections newDirection);

    /// <summary>
    /// The event for when the player changes directions.
    /// This isn't called if the direction was changed and the player was already facing that direction.
    /// </summary>
    public event DirectionChanged DirectionChangedEvent = null;

    public enum FacingDirections
    {
        North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
    }

    public FacingDirections CurDirection { get; private set; }

    private void Awake()
    {
        //Default to South
        CurDirection = FacingDirections.South;
    }

    private void OnDestroy()
    {
        DirectionChangedEvent = null;
    }

    /// <summary>
    /// Sets the direction for the player to face.
    /// </summary>
    /// <param name="newDirection">The new direction for the player to face.</param>
    public void SetDirection(FacingDirections newDirection)
    {
        //Fire the event if the direction was different
        if (CurDirection != newDirection)
        {
            if (DirectionChangedEvent != null)
            {
                DirectionChangedEvent(newDirection);
            }

            CurDirection = newDirection;
        }
    }
}
