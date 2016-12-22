using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple player movement class.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public enum FacingDirections
    {
        North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
    }

    /// <summary>
    /// Character speed.
    /// </summary>
    public float Speed = .1f;

    /// <summary>
    /// The direction the character is facing. This determines the sprites the character will use.
    /// </summary>
    public FacingDirections FacingDirection = FacingDirections.South;

    private void Update()
    {
        Vector3 diff = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            diff.y = Speed;
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            diff.y = -Speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            diff.x = -Speed;
        }
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            diff.x = Speed;
        }

        FacingDirection = SetDirectionFromSpeed(new Vector2(diff.x, diff.y));

        transform.position += diff;
    }

    private FacingDirections SetDirectionFromSpeed(Vector2 speed)
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
        return FacingDirection;
    }
}
