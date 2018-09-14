using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Script : MonoBehaviour {
    //This GameObject PauseUI will represent the actual object of the Menu.   
    public GameObject PauseUI, gameOverUI;
    private bool GamePaused = false;
    public GameObject PauseButton;


	// Use this for initialization
	void Start () {
        //Initially sets pause menu and game over menu as inactive at the beginning of the game.  
        PauseUI.SetActive(false);
        gameOverUI.SetActive(false);
	}

    public void PauseGame () {
        //make pause button disappear and make pause screen appear
        PauseButton.gameObject.SetActive(false);
        PauseUI.SetActive(true);
        Time.timeScale = 0;
	}

    //resumes the game
    public void Resume_Game(){
        //make pause button reappear and pause screen disappear
        PauseButton.gameObject.SetActive(true);
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }

    //restarts the game
    public void Restart() {
        //restart the timer
        Time.timeScale = 1;

        //take out all overlays(pause or defeat)
        if (PauseUI.activeInHierarchy == true)
        {
            PauseUI.SetActive(false);
        }
        if (gameOverUI.activeInHierarchy == true)
        {
            gameOverUI.SetActive(false);
        }
        //reload the game
        SceneManager.LoadScene("play");
    }

    public void Quit() {
        //loads the first scene which should be our Main Menu.
        //stop the time
        Time.timeScale = 0;
        SceneManager.LoadScene("MainMenu");
    }

    //game is over (lost all lives)
    public void GameOver()
    {
        //pause game and set gameover UI over the top of the game
        Time.timeScale = 0;
        PauseButton.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }
}


