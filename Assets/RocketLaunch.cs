using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketLaunch : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float MaxHeight;
    public float MaxSpeed; // Maximum speed the rocket can have

    [Header("Quiz Settings")]
    public int CorrectAnswers;
    
    private float targetHeight;
    private bool launch = false;
    private bool falling = false;
    private Vector3 startPos;
    private Rigidbody2D rb;
    private float speed; // This is now private as its value is calculated based on MaxSpeed and CorrectAnswers

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        rb.gravityScale = 0;
    }

    void Update()
    {
        //CorrectAnswers = GameData.Instance.score;
        
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
    }

    IEnumerator LoadQuestionScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("QuestionsScene");
    }
}