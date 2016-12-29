using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character states for their animation state machines.
/// </summary>
public static class CharacterStates
{
    #region Overworld

    /// <summary>
    /// A base state that allows character movement.
    /// </summary>
    public abstract class MovementState : FSMState
    {
        /// <summary>
        /// The animation to play.
        /// </summary>
        protected abstract string AnimToPlay { get; }

        /// <summary>
        /// The direction the character is facing. This determines the sprites the character will use.
        /// </summary>
        protected PlayerDirection playerDirection = null;

        /// <summary>
        /// The grounded state of the player.
        /// </summary>
        protected PlayerGrounded playerGrounded = null;

        /// <summary>
        /// The amount the player currently moved.
        /// </summary>
        protected Vector3 CurMoveAmt = Vector3.zero;

        /// <summary>
        /// The amount the player previously moved.
        /// </summary>
        protected Vector3 PrevMoveAmt = Vector3.zero;

        public override void Enter()
        {
            playerDirection = StateMachine.GetComponent<PlayerDirection>();
            playerGrounded = StateMachine.GetComponent<PlayerGrounded>();
            StateMachine.AnimManager.PlayAnimation(AnimToPlay, playerDirection.CurDirection);

            playerDirection.DirectionChangedEvent += PlayerDirectionChangedEvent;
            playerGrounded.GroundStateChangedEvent += GroundedStateChangedEvent;
        }

        public override void Exit()
        {
            playerDirection.DirectionChangedEvent -= PlayerDirectionChangedEvent;
            playerGrounded.GroundStateChangedEvent -= GroundedStateChangedEvent;
        }

        public override void Update()
        {
            PrevMoveAmt = CurMoveAmt;

            CurMoveAmt = Vector3.zero;

            //Kimimaru - NOTE: Fetch the speed value from somewhere else
            float speed = .1f;

            if (Input.GetKey(KeyCode.UpArrow) == true)
            {
                CurMoveAmt.y = speed;
            }
            if (Input.GetKey(KeyCode.DownArrow) == true)
            {
                CurMoveAmt.y = -speed;
            }
            if (Input.GetKey(KeyCode.LeftArrow) == true)
            {
                CurMoveAmt.x = -speed;
            }
            if (Input.GetKey(KeyCode.RightArrow) == true)
            {
                CurMoveAmt.x = speed;
            }

            //Test jumping state
            if (Input.GetKeyDown(KeyCode.Z))
            {
                playerGrounded.SetGroundState(playerGrounded.GroundedState == PlayerGrounded.GroundedStates.Grounded 
                    ? PlayerGrounded.GroundedStates.Airborne : PlayerGrounded.GroundedStates.Grounded);
            }

            //Set the direction to face
            playerDirection.SetDirection(PlayerDirection.GetDirectionFromSpeed(new Vector2(CurMoveAmt.x, CurMoveAmt.y), playerDirection.CurDirection));

            StateMachine.transform.position += CurMoveAmt;
        }

        protected void PlayerDirectionChangedEvent(PlayerDirection.FacingDirections newDirection)
        {
            //Debug.Log("NEW DIRECTION: " + newDirection);

            StateMachine.AnimManager.PlayAnimation(AnimToPlay, newDirection);
        }

        protected virtual void GroundedStateChangedEvent(PlayerGrounded.GroundedStates newGroundState)
        {
            
        }
    }

    public class IdleState : MovementState
    {
        protected override string AnimToPlay { get { return ResourcePath.AnimationStrings.IdleAnim; } }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (CurMoveAmt.x != 0f || CurMoveAmt.y != 0f)
            {
                StateMachine.Transition(new WalkingState());
            }
        }

        protected override void GroundedStateChangedEvent(PlayerGrounded.GroundedStates newGroundState)
        {
            if (newGroundState == PlayerGrounded.GroundedStates.Airborne)
            {
                StateMachine.Transition(new JumpingState());
            }
        }
    }

    public class WalkingState : MovementState
    {
        protected override string AnimToPlay { get { return ResourcePath.AnimationStrings.WalkingAnim; } }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (CurMoveAmt.x == 0f && CurMoveAmt.y == 0f)
            {
                StateMachine.Transition(new IdleState());
            }
        }

        protected override void GroundedStateChangedEvent(PlayerGrounded.GroundedStates newGroundState)
        {
            if (newGroundState == PlayerGrounded.GroundedStates.Airborne)
            {
                StateMachine.Transition(new JumpingState());
            }
        }
    }

    public class JumpingState : MovementState
    {
        protected override string AnimToPlay { get { return ResourcePath.AnimationStrings.JumpAnim; } }

        public override void Enter()
        {
            base.Enter();

            //Test
            AudioClip sound = Resources.Load<AudioClip>(ResourcePath.Audio.SFXPath + "Mario_Jump");
            AudioManager.Instance.PlaySound(sound);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();   
        }

        protected override void GroundedStateChangedEvent(PlayerGrounded.GroundedStates newGroundState)
        {
            if (newGroundState == PlayerGrounded.GroundedStates.Grounded)
            {
                StateMachine.Transition(new IdleState());
            }
        }
    }

    #endregion
}
