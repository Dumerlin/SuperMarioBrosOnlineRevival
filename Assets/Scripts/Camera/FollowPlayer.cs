﻿using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    private PlayerCharacter Player;

    private void Start()
    {
        // Get the player
        Player = PlayerCharacter.Instance;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);

        // Check if the player is past the middle of the screen
        //if (Player.transform.position.x > transform.position.x)
        //{
        //    Get the distance between the camera and the player
        //    float distance = (Player.transform.position.x - transform.position.x);

        //    Center the camera on the player by adding the distance to the camera's X position
        //    transform.position = new Vector3((transform.position.x + distance), transform.position.y, transform.position.z);
        //}
    }
}
