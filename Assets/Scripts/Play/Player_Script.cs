﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour {
    //declare
    public int lives, score;
    public int[] highscores = new int[5];
    public GameObject heart1, heart2, heart3;
    public Text CountText;
    public Pause_Menu_Script GetScript;
    [System.Serializable]
    public class myScores
    {
        public int score;
    }

    private void Start()
    {
        enabled = true;
        //get the list of high scores
        for(int i = 0;i < 5;i++)
        {
            highscores[i] = PlayerPrefs.GetInt("highscore" + i);
        }
  
        //starts at 3
        lives = 3;
        //score starts at 0
        score = 0;
        //create reference to Pause_Menu_Script so we can access its functions
        GetScript = FindObjectOfType<Pause_Menu_Script>();
    }
    
    void Update()
    {
        //run out of lives
        if (lives == 0)
        {
            //check to see if it is a high score
            for(int i = 0;i < 5;i++)
            {
                if(score > highscores[i])
                {
                    for(int j = 4;j > i;j--)
                    {
                        highscores[j] = highscores[j - 1];
                    }
                    highscores[i] = score;
                    break;
                }
            }
            //store into playerpref
            for(int k = 0;k < 4;k++)
            {
                PlayerPrefs.SetInt("highscore" + k, highscores[k]);
            }

            //store in JSON file to analyze
            myScores myScore = new myScores();
            myScore.score = score;
            string json = JsonUtility.ToJson(myScore);

            //stop updating
            enabled = false;
            //call the function of Pause_Menu_Script
            GetScript.GameOver();
        }
        //move the player left and right
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(new Vector2(touch.position.x, 30));
                    transform.position = Vector2.Lerp(transform.position, touchPos, 2.0f *Time.deltaTime);
            }
        }
    }
    //if collide with triangle, lose a life
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "triangle(Clone)")
        {
            lives--;
        }
        else if(collision.gameObject.name == "Circle(Clone)")
        {
            score++;
            SetScore();
        }
        else
        {
            Debug.Log("Problem with Scoring");
        }

        //Display hearts. CHANGE LATER TO OPTIMIZE BUT NEED ACTUAL SIZE OF HEARTS
        if(lives == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        else if(lives == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
        }
        else if(lives == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else if(lives == 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
        }
        else
        {
            Debug.Log("error with lives");
        }
    }
    private void SetScore()
    {
        CountText.text = "Score: " + score.ToString();
    }
}