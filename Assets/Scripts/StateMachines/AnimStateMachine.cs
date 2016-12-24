using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Finite State Machine for animations.
/// </summary>
public class AnimStateMachine : MonoBehaviour
{
    /// <summary>
    /// Animation manager reference.
    /// </summary>
    public AnimationManager AnimManager { get; private set; }

    /// <summary>
    /// The current state.
    /// </summary>
    public FSMState CurState { get; private set; }

    /// <summary>
    /// The name of the current state.
    /// </summary>
    public string CurStateName { get; private set; }

    private void Start()
    {
        AnimManager = GetComponent<AnimationManager>();

        Transition(new CharacterStates.IdleState());
    }

    /// <summary>
    /// Transitions to a new state.
    /// </summary>
    /// <param name="newState">The new FSMState to transition to.</param>
    public void Transition(FSMState newState)
    {
        //Exit current state
        if (CurState != null)
            CurState.Exit();

        CurState = newState;
        CurState.StateMachine = this;
        CurStateName = CurState.GetType().Name;

        //Enter the new state
        if (CurState != null)
        {
            CurState.Enter();
        }
        else
        {
            Debug.LogWarning("Current state passed in is null!");
        }
    }

    public void Update()
    {
        if (CurState != null)
            CurState.Update();
    }
}

public abstract class FSMState
{
    /// <summary>
    /// The state machine this state belongs to.
    /// This is set automatically.
    /// </summary>
    public AnimStateMachine StateMachine = null;

    /// <summary>
    /// What happens when entering the state.
    /// </summary>
    public abstract void Enter();

    /// <summary>
    /// What happens when exiting the state.
    /// </summary>
    public abstract void Exit();

    /// <summary>
    /// What happens while the state is updating.
    /// </summary>
    public abstract void Update();
}
