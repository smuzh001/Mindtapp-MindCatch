using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Script : MonoBehaviour {
    public GameObject Play, HighScore, Picture, Quit;

	// Use this for initialization
	void Start () {
		
	}

    //load the game
    public void PlayGame()
    {
        SceneManager.LoadScene("play");
    }
    public void Score()
    {
        Debug.Log("will go to score screen");
    }
    public void takePicture()
    {
        Debug.Log("will go to picture screen");
    }
    //Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
