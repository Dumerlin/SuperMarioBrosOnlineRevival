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

    public class IdleState : FSMState
    {
        public override void Enter()
        {
            StateMachine.AnimManager.PlayAnimation(ResourcePath.AnimationStrings.IdleAnim);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            //TODO: Add jumping input
        }
    }

    public class WalkingState : FSMState
    {
        public override void Enter()
        {
            StateMachine.AnimManager.PlayAnimation(ResourcePath.AnimationStrings.WalkingAnim);
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            //TODO: Add jumping input
        }
    }

    #endregion
}
