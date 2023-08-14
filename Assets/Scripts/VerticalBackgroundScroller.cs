using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalBackgroundScroller : MonoBehaviour
{
    public Transform rocket; // Assign the rocket GameObject in the inspector
    public float backgroundScrollSpeed = 0.5f;
    private float backgroundHeight;
    private Vector3 lastRocketPosition;

    // Start is called before the first frame update
    void Start()
    {
        backgroundHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        lastRocketPosition = rocket.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the vertical distance the rocket moved since the last frame
        float rocketVerticalMovement = rocket.position.y - lastRocketPosition.y;

        // Scroll the background based on the rocket's movement
        Vector3 newPosition = transform.position;
        newPosition.y += rocketVerticalMovement * backgroundScrollSpeed;
        transform.position = newPosition;

        // Loop the background
        if (newPosition.y - backgroundHeight > rocket.position.y)
        {
            // If the background is too far up, move it down
            newPosition.y -= 2 * backgroundHeight;
            transform.position = newPosition;
        }
        else if (newPosition.y + backgroundHeight < rocket.position.y)
        {
            // If the background is too far down, move it up
            newPosition.y += 2 * backgroundHeight;
            transform.position = newPosition;
        }

        // Save the rocket's current position for the next frame
        lastRocketPosition = rocket.position;
    }
}