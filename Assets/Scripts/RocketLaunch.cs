using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketLaunch : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float MaxHeight;
    public float MaxSpeed; // Maximum speed the rocket can have
    public float StrafeSpeed; // Speed for moving rocket left and right

    [Header("Quiz Settings")]
    public int CorrectAnswers;

    private float targetHeight;
    private bool launch = false;
    private bool falling = false;
    private Vector3 startPos;
    private Rigidbody2D rb;
    private float speed; // This is now private as its value is calculated based on MaxSpeed and CorrectAnswers
    private float leftBound; // To store the leftmost x value the rocket can be
    private float rightBound; // To store the rightmost x value the rocket can be

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.gravityScale = 0;

        // Calculate camera bounds:
        float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        leftBound = Camera.main.transform.position.x - camHalfWidth;
        rightBound = Camera.main.transform.position.x + camHalfWidth;
    }

    void Update()
    {
        CorrectAnswers = GameData.Instance.score;

        if (Input.GetKeyDown(KeyCode.Space) && !launch)
        {
            targetHeight = (MaxHeight / 21) * CorrectAnswers;
            speed = (MaxSpeed / 20) * CorrectAnswers; // Calculate the speed
            launch = true;
        }

        if (launch && transform.position.y < targetHeight)
        {
            rb.velocity = Vector2.up * speed; // Use the calculated speed here
        }
        else if (launch)
        {
            launch = false;
            falling = true;
            rb.velocity = Vector2.zero;
            rb.gravityScale = 1;
        }

        // If the rocket is falling and has reached or passed its starting y position
        if (falling && transform.position.y <= startPos.y)
        {
            falling = false;
            // Stop the rocket completely
            rb.velocity = Vector2.zero;
            // Turn off gravity so the rocket remains stationary
            rb.gravityScale = 0;
            // Force the rocket's position back to the start
            transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
            // Wait for 3 seconds, then load the "QuestionsScene"
            StartCoroutine(LoadQuestionScene());
        }

        // Strafing:
        if (launch || falling) // Check if the rocket is in the air
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            if(Mathf.Abs(horizontalInput) > 0.1f)
            {
                Vector3 newPosition = transform.position + new Vector3(horizontalInput * StrafeSpeed * Time.deltaTime, 0, 0);
                newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound); // Make sure the rocket doesn't go out of the camera bounds
                transform.position = newPosition;
            }
        }
    }

    IEnumerator LoadQuestionScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("QuestionsScene");
    }
}