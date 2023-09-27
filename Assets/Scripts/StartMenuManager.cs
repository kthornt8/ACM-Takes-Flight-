using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    // Function to handle the "Play" button click event
    public void PlayGame()
    {
        SceneManager.LoadScene("RocketLaunchScene"); 
    }

    // Function to handle the "Questions Screen" button click event
    public void GoToQuestionsScreen()
    {
        SceneManager.LoadScene("QuestionsScene"); 
    }
    public void GoToInstructionsScreen()
    {
        SceneManager.LoadScene("InstructionsScene");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QuitGame");
    }
}