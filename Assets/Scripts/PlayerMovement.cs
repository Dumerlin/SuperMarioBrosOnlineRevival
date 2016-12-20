using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple player movement class.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Character speed.
    /// </summary>
    public float Speed = .1f;

    private void Update()
    {
        Vector3 diff = Vector3.zero;

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

        transform.position += diff;
    }
}
