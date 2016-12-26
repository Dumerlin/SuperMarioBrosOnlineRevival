using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tells the player's current grounded state.
/// This tells whether the player is on the ground or not.
/// </summary>
public class PlayerGrounded : MonoBehaviour
{
    public delegate void GroundedChanged(GroundedStates newGroundState);

    /// <summary>
    /// The event for when the player changes ground states.
    /// </summary>
    public event GroundedChanged GroundStateChangedEvent = null;

    public enum GroundedStates
    {
        Grounded, Airborne
    }

    public GroundedStates GroundedState = GroundedStates.Grounded;

    private void Awake()
    {
        //Default to Grounded
        GroundedState = GroundedStates.Grounded;
    }

    private void OnDestroy()
    {
        GroundStateChangedEvent = null;
    }

    public void SetGroundState(GroundedStates newGroundState)
    {
        //Fire the event if the grounded state was changed
        if (GroundedState != newGroundState)
        {
            if (GroundStateChangedEvent != null)
            {
                GroundStateChangedEvent(newGroundState);
            }
        }

        GroundedState = newGroundState;
    }
}
