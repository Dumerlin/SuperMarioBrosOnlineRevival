using UnityEngine;
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
        Rect mapBounds = Map.Instance.Bounds;

        float xMin = mapBounds.xMin / 32;
        float xMax = mapBounds.xMax / 32;
        float yMin = mapBounds.yMin / 32;
        float yMax = mapBounds.yMax / 32;

        if (Player.transform.position.x < xMin)
        {
            Player.transform.position = new Vector3(xMin, Player.transform.position.y);
        }
        else if (Player.transform.position.x > xMax)
        {
            Player.transform.position = new Vector3(xMax, Player.transform.position.y);
        }

        if (Player.transform.position.y < yMin)
        {
            Player.transform.position = new Vector3(Player.transform.position.x, yMin);
        }
        else if (Player.transform.position.y > yMax)
        {
            Player.transform.position = new Vector3(Player.transform.position.x, yMax);
        }

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
