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

    /// <summary>
    /// The amount the player previously moved.
    /// </summary>
    private Vector3 PrevMoveAmt = Vector3.zero;

    /// <summary>
    /// The amount the player currently moved.
    /// </summary>
    private Vector3 CurMoveAmt = Vector3.zero;

    protected void Awake()
    {
        AnimManager = GetComponent<AnimationManager>();
        playerDirection = GetComponent<PlayerDirection>();
    }

    private void Update()
    {
        PrevMoveAmt = CurMoveAmt;

        CurMoveAmt = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            CurMoveAmt.y = Speed;
        }
        if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            CurMoveAmt.y = -Speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) == true)
        {
            CurMoveAmt.x = -Speed;
        }
        if (Input.GetKey(KeyCode.RightArrow) == true)
        {
            CurMoveAmt.x = Speed;
        }

        //Set the direction to face
        playerDirection.SetDirection(GetDirectionFromSpeed(new Vector2(CurMoveAmt.x, CurMoveAmt.y)));

        if (CurMoveAmt != PrevMoveAmt)
        {
            if (CurMoveAmt.x != 0f || CurMoveAmt.y != 0f)
            {
                AnimManager.PlayAnimation(ResourcePath.AnimationStrings.WalkingAnim, playerDirection.CurDirection);
            }
            else
            {
                AnimManager.PlayAnimation(ResourcePath.AnimationStrings.IdleAnim, playerDirection.CurDirection);
            }
        }

        //Testing new animation
        if (Input.GetKeyDown(KeyCode.T)) AnimManager.PlayAnimation(ResourcePath.AnimationStrings.GetItem, playerDirection.CurDirection);

        transform.position += CurMoveAmt;
    }

    private void LateUpdate()
    {
        if (CurMoveAmt.x == 0f && CurMoveAmt.y == 0f)
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
