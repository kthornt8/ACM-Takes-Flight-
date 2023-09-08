using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrollingController : MonoBehaviour
{
    public SpriteRenderer image1;
    public SpriteRenderer image2;
    public SpriteRenderer image3;

    public float scrollSpeed1;
    public float scrollSpeed2;
    public float scrollSpeed3;
    public bool loop = false;

    private float screenWidth;

    private void Start()
    {
        screenWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
    }

    private void Update()
    {
        float offsetX1 = Time.time * scrollSpeed1;
        float offsetX2 = Time.time * scrollSpeed2;
        float offsetX3 = Time.time * scrollSpeed3;

        float newPositionX1 = offsetX1;
        float newPositionX2 = offsetX2;
        float newPositionX3 = offsetX3;

        image1.transform.position = new Vector2(newPositionX1, image1.transform.position.y);
        image2.transform.position = new Vector2(newPositionX2, image2.transform.position.y);
        image3.transform.position = new Vector2(newPositionX3, image3.transform.position.y);

        // Check if the images have scrolled off the screen
        CheckImageLooping();
    }

    private void CheckImageLooping()
    {
    do{
        if (image1.transform.position.x <= -screenWidth)
        {
            image1.transform.position += new Vector3(screenWidth * 3f, 0f, 0f);
        }

        if (image2.transform.position.x <= -screenWidth)
        {
            image2.transform.position += new Vector3(screenWidth * 3f, 0f, 0f);
        }

        if (image3.transform.position.x <= -screenWidth)
        {
            image3.transform.position += new Vector3(screenWidth * 3f, 0f, 0f);
        }
        }
        while(loop = true);
    }
}