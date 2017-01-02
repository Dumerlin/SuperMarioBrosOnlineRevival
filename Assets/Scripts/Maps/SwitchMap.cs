using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SwitchMap : MonoBehaviour
{
    // The player has collided with the edge of the map
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contactPoint = collision.contacts.First<ContactPoint2D>().point;

        Map map = Map.Instance;
        Rect mapBounds = map.Bounds.DivideBy(32);

        int newMapID = 0;
        PlayerDirection.FacingDirections direction = PlayerDirection.FacingDirections.East;

        if (contactPoint.x <= mapBounds.xMin)
        {
            newMapID = map.LeftMapID;
            direction = PlayerDirection.FacingDirections.West;
        }
        else if (contactPoint.x >= mapBounds.xMax)
        {
            newMapID = map.RightMapID;
            direction = PlayerDirection.FacingDirections.East;
        }
        else if (contactPoint.y <= mapBounds.yMin)
        {
            newMapID = map.BottomMapID;
            direction = PlayerDirection.FacingDirections.South;
        }
        else if (contactPoint.y >= mapBounds.yMax)
        {
            newMapID = map.TopMapID;
            direction = PlayerDirection.FacingDirections.North;
        }

        Debug.Log(newMapID);

        // Check if we should transition to a new map
        if (newMapID > 0)
        {
            TransitionToMap(newMapID, direction);
        }
    }

    private void TransitionToMap(int mapID, PlayerDirection.FacingDirections playerDirection)
    {
        EnablePlayerColliderTrigger();

        // TODO: Start fade out
        MapFade.Create(true, false);
        DontDestroyOnLoad(MapFade.Instance);

        SetCameraFollow(false);
        LoadMap(mapID);

        // Fade in
        MapFade.Instance.FadeIn = false;
        MapFade.Instance.DestroyOnFinish = true;
        MapFade.Instance.ResetFade(true);

        SetCameraFollow(true);

        // Get the new map
        Map map = Map.Instance;
        Rect mapBounds = map.Bounds.DivideBy(32);

        // Note: The code below doesn't work but will work when we've stopped using scenes to load map data
        PlayerCharacter player = PlayerCharacter.Instance;

        // Used to move the player up one step after transitioning to a new map
        float speed = PlayerCharacter.Instance.BaseMoveSpeed;

        player.Direction.SetDirection(playerDirection);

        // Set player's position to the beginning of the next map
        switch (playerDirection)
        {
            case PlayerDirection.FacingDirections.West:
                player.transform.position = new Vector3(mapBounds.xMax - speed, player.transform.position.y);

                break;
            case PlayerDirection.FacingDirections.East:
                player.transform.position = new Vector3(mapBounds.xMin + speed, player.transform.position.y);

                break;
            case PlayerDirection.FacingDirections.South:
                player.transform.position = new Vector3(player.transform.position.x, mapBounds.yMax - speed);

                break;
            case PlayerDirection.FacingDirections.North:
                player.transform.position = new Vector3(player.transform.position.x, mapBounds.yMin + speed);

                break;
        }
    }

    private void EnablePlayerColliderTrigger()
    {
        BoxCollider2D playerCollider = PlayerCharacter.Instance.gameObject.GetComponent<BoxCollider2D>();
        playerCollider.isTrigger = true;
    }

    private void SetCameraFollow(bool enabled)
    {
        FollowPlayer followPlayer = Camera.main.GetComponent<FollowPlayer>();
        followPlayer.enabled = enabled;
    }

    private void LoadMap(int mapID)
    {
        // TODO: REMOVE!! We should not change scenes when transitioning maps
        // We should instead remove the existing map and load the new map data then add it to the existing scene
        SceneManager.LoadScene("Map" + mapID);
    }
}
