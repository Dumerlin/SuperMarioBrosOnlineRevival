using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The current player playing the game on this client.
/// There's only one instance if it as it's the only character the player controls.
/// <para>This is a Special Instance. It behaves like a Singleton, except it isn't instantiated in the getter.</para>
/// </summary>
[DisallowMultipleComponent]
public class PlayerCharacter : MonoBehaviour
{
    #region Special Instance Methods

    /// <summary>
    /// Gets the instance of the PlayerCharacter.
    /// </summary>
    public static PlayerCharacter Instance { get { return instance; } }

    /// <summary>
    /// Tells whether there is a PlayerCharacter instance.
    /// </summary>
    public static bool HasInstance { get { return (instance != null); } }

    /// <summary>
    /// Instance reference.
    /// </summary>
    private static PlayerCharacter instance = null;

    #endregion

    public Constants.Characters Character = Constants.Characters.Mario;
    public string AccountName = "TestAccount";
    public string CharacterName = "Test";

    private void Awake()
    {
        //Ensure only one instance exists
        if (instance == null)
        {
            instance = this;
        }
        //Destroy other instances
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
