using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_Menu_Script : MonoBehaviour {
    //This GameObject PauseUI will represent the actual object of the Menu.   
    public GameObject PauseUI;
    private bool GamePaused = false;



	// Use this for initialization
	void Start () {
        //Initially sets pause menu as inactive at the beginning of the game.  
        PauseUI.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update () {
        //Checks if the button assigned for Pause has been pressed. Check Edit > Project Settings > Input
        if (Input.GetButtonDown("Pause")) {
            GamePaused = !GamePaused;
        }
        if (GamePaused) {
            PauseUI.SetActive(true);
            //Time.timeScale controls flow of the game. When 0, no time has been allocated and thus the game is paused
            Time.timeScale = 0;
        }
        if (!GamePaused) {
            PauseUI.SetActive(false);
            Time.timeScale = 1;
            //when Time.timeScale is 1, the game is back to flowing at a regular time flow.
        }
	}

    public void Resume_Game(){
        GamePaused = false;
    }

    public void Restart() {
        Application.LoadLevel(Application.loadedLevel); 
    }

    public void Quit() {
        //loads the first scene which should be our Main Menu.
        //Application.LoadLevel(1);
        Application.Quit();
    } 
}


