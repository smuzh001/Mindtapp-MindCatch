using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Script : MonoBehaviour {
    public GameObject Play, HighScore, Picture, CreateCharacter, Quit;

	// Use this for initialization
	void Start () {
		
	}

    //load the game
    public void PlayGame()
    {
        //start the time
        Time.timeScale = 1;
        SceneManager.LoadScene("play");
    }
    //go to high score
    public void Score()
    {
        Debug.Log("will go to score screen");
    }
    //go to picture taking
    public void takePicture()
    {
        Debug.Log("will go to picture screen");
    }
    //go to character creater
    public void createChar()
    {
        Debug.Log("will go to character creater");
    }
    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
