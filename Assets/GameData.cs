using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    public int score = 1;  // Player starts with 1 correct answer by default
    public Color[] buttonColors;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize the buttonColors array with default color
            int numberOfQuestions = 20;
            buttonColors = new Color[numberOfQuestions];
            for (int i = 0; i < numberOfQuestions; i++)
            {
                buttonColors[i] = Color.white; // Set default color
            }
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
