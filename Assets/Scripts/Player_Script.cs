using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour {
    //number of lives
    public int lives;

    private void Start()
    {
        //starts at 3
        lives = 3; 
    }
    // Use this for initialization
    void Update()
    {
        //gameover
        if(lives == 0)
        {
            Time.timeScale = 0;
            Application.Quit();
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
    }
}
