using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.SceneManagement; //Used to restart the game.

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUI;
    private bool isGameOver = false;

    void Start()
    {
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }
    }

    public void gameOver()
    {
        if (!isGameOver)//Prevent Game Over from being called again              
        {
            isGameOver = true;
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            } 
            ControlData.Score = 0; //Reset score before starting ove
        }
    }

    public void RestartGame()
    {
        ControlData.Score = 0;//Reset score before starting ove
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reload the current scene
    }

    public void QuitGame()
    {
        Debug.Log("🚪 Quit Game!");
        Application.Quit();
    }
}
