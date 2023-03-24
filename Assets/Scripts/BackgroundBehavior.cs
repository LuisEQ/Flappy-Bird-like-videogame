using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public float resetPosition = -20.0f;
    public float startPosition = 20.0f;

    void Update()
    {
        // Move the background to the left
        transform.Translate(-speed * Time.deltaTime, 0, 0);

        // Reset the background to its starting position if it has moved off-screen
        if (transform.position.x < resetPosition)
        {
            Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
}
