using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu_Script : MonoBehaviour {
    //This GameObject PauseUI will represent the actual object of the Menu.   
    public GameObject PauseUI;
    private bool GamePaused = false;
    public GameObject PauseButton;


	// Use this for initialization
	void Start () {
        //Initially sets pause menu as inactive at the beginning of the game.  
        PauseUI.SetActive(false);
        
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
        Time.timeScale = 1;
        PauseUI.SetActive(false);
        SceneManager.LoadScene("play");
    }

    public void Quit() {
        //loads the first scene which should be our Main Menu.
        SceneManager.LoadScene("MainMenu");
    } 
}


