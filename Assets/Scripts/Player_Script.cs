using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour {
    public int lives;

    private void Start()
    {
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
    //if collide with falling objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "triangle")
        {
            lives--;
        }
    }
}
