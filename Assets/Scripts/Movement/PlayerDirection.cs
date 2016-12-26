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

    public FacingDirections CurDirection;

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

    /// <summary>
    /// Gets a direction based on a speed value.
    /// </summary>
    /// <param name="speed">A Vector2 of the speed the character is moving.</param>
    /// <param name="curDirection">The current direction value. This is returned if there is no movement.</param>
    /// <returns></returns>
    public static FacingDirections GetDirectionFromSpeed(Vector2 speed, FacingDirections curDirection)
    {
        //Moving left
        if (speed.x < 0)
        {
            if (speed.y < 0) return FacingDirections.SouthWest;
            else if (speed.y > 0) return FacingDirections.NorthWest;
            return FacingDirections.West;
        }
        //Moving right
        else if (speed.x > 0)
        {
            if (speed.y < 0) return FacingDirections.SouthEast;
            if (speed.y > 0) return FacingDirections.NorthEast;
            return FacingDirections.East;
        }

        //Moving down
        if (speed.y < 0) return FacingDirections.South;

        //Moving up
        if (speed.y > 0) return FacingDirections.North;

        //Return the current direction if not moving
        return curDirection;
    }
}
