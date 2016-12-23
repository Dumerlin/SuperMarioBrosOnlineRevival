using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple player movement class.
/// </summary>
[RequireComponent(typeof(PlayerDirection))]
[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour
{
    private AnimationManager AnimManager = null;

    /// <summary>
    /// Character speed.
    /// </summary>
    public float Speed = .1f;

    /// <summary>
    /// The direction the character is facing. This determines the sprites the character will use.
    /// </summary>
    public PlayerDirection playerDirection = null;

    protected void Awake()
    {
        AnimManager = GetComponent<AnimationManager>();
        playerDirection = GetComponent<PlayerDirection>();
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

        //Set the direction to face
        playerDirection.SetDirection(GetDirectionFromSpeed(new Vector2(diff.x, diff.y)));

        if (diff.x != 0f || diff.y != 0f)
        {
            AnimManager.PlayAnimation(ResourcePath.AnimationStrings.WalkingAnim, playerDirection.CurDirection);
        }
        else
        {
            AnimManager.PlayAnimation(ResourcePath.AnimationStrings.IdleAnim, playerDirection.CurDirection);
        }

        transform.position += diff;
    }

    private void LateUpdate()
    {
        if (diff.x == 0f && diff.y == 0f)
        {
            //GetComponent<SpriteRenderer>().sprite = IdleSprites[(int)FacingDirection];
        }
    }

    private PlayerDirection.FacingDirections GetDirectionFromSpeed(Vector2 speed)
    {
        //Moving left
        if (speed.x < 0)
        {
            if (speed.y < 0) return PlayerDirection.FacingDirections.SouthWest;
            else if (speed.y > 0) return PlayerDirection.FacingDirections.NorthWest;
            return PlayerDirection.FacingDirections.West;
        }
        //Moving right
        else if (speed.x > 0)
        {
            if (speed.y < 0) return PlayerDirection.FacingDirections.SouthEast;
            if (speed.y > 0) return PlayerDirection.FacingDirections.NorthEast;
            return PlayerDirection.FacingDirections.East;
        }

        //Moving down
        if (speed.y < 0) return PlayerDirection.FacingDirections.South;

        //Moving up
        if (speed.y > 0) return PlayerDirection.FacingDirections.North;

        //Return the current direction if not moving
        return playerDirection.CurDirection;
    }
}
