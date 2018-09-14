using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu_Script : MonoBehaviour {
    public GameObject Play, HighScore, Picture, CreateCharacter, Quit;
    public GameObject HighScoreUI;
    public int[] score = new int[5];
    public Text HighScoreText;
    public GameObject ReturnMainButton;

	// Use this for initialization
	void Start () {
        HighScoreUI.SetActive(false);
        HighScoreText.gameObject.SetActive(false);
        ReturnMainButton.SetActive(false);
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
        HighScoreUI.SetActive(true);
        HighScoreText.gameObject.SetActive(true);
        ReturnMainButton.SetActive(true);
        //Debug.Log(score[0]);
        for (int i = 0; i < 5; i++)
        {
            score[i] = PlayerPrefs.GetInt("highscore" + i);
        }
        HighScoreText.text = "1. " + score[0].ToString() + "\n\n2. " + score[1].ToString()
            + "\n\n3. " + score[2].ToString() + "\n\n4. " + score[3].ToString() + "\n\n5. " + score[4].ToString();
    }
    public void returntoMenu()
    {
        HighScoreUI.SetActive(false);
        HighScoreText.gameObject.SetActive(false);
        ReturnMainButton.SetActive(false);

    }

    //go to picture taking
    public void takePicture()
    {
        Debug.Log("will go to picture screen");
        SceneManager.LoadScene("Camera");
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
