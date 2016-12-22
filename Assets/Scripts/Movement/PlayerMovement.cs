using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple player movement class.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public delegate void DirectionChanged();

    /// <summary>
    /// The event for when the player changes directions.
    /// This isn't called if the direction was changed and the player was already facing that direction.
    /// </summary>
    public event DirectionChanged DirectionChangedEvent = null;

    public enum FacingDirections
    {
        North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest
    }

    protected Animator m_Animator = null;

    /// <summary>
    /// Character speed.
    /// </summary>
    public float Speed = .1f;

    /// <summary>
    /// The direction the character is facing. This determines the sprites the character will use.
    /// </summary>
    public FacingDirections FacingDirection = FacingDirections.South;

    //North, NorthEast, East, SouthEast, South, SouthWest, West, NorthWest in that order
    public Sprite[] IdleSprites = new Sprite[0];

    protected void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        DirectionChangedEvent = null;
    }
    Vector3 diff = Vector3.zero;
    private void Update()
    {
        diff = Vector3.zero;

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

        m_Animator.SetFloat("SpeedX", diff.x);
        m_Animator.SetFloat("SpeedY", diff.y);

        ChangeDirection(GetDirectionFromSpeed(new Vector2(diff.x, diff.y)));

        GetComponent<SpriteRenderer>().flipX = FacingDirection > FacingDirections.South;

        //if (diff.x == 0f && diff.y == 0f)
        //{
        //    GetComponent<SpriteRenderer>().sprite = IdleSprites[(int)FacingDirection];
        //    GetComponent<SpriteRenderer>().flipX = FacingDirection > FacingDirections.South;
        //}

        transform.position += diff;
    }

    private void LateUpdate()
    {
        if (diff.x == 0f && diff.y == 0f)
        {
            GetComponent<SpriteRenderer>().sprite = IdleSprites[(int)FacingDirection];
        }
    }

    /// <summary>
    /// Changes the direction the player is facing.
    /// </summary>
    /// <param name="newDirection">The new direction for the player to face.</param>
    public void ChangeDirection(FacingDirections newDirection)
    {
        if (FacingDirection != newDirection)
        {
            if (DirectionChangedEvent != null)
                DirectionChangedEvent();
        }

        FacingDirection = newDirection;
    }

    private FacingDirections GetDirectionFromSpeed(Vector2 speed)
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
