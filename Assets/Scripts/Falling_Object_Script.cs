using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Object_Script : MonoBehaviour
{
    public float speed;
    // Use this for initialization
    void Start()
    {
        speed = 7;
        //start randomly
        float randomXstart = Random.Range(-2.5f, 2.5f);
        float randomYstart = Random.Range(8.0f, 12.0f);
        transform.position = new Vector2(randomXstart, randomYstart);   
    }

    // Update is called once per frame
    void Update()
    {
        //move out of bounds on y axis
        if (transform.position.y <= -5.5f)
        {
            MoveToTop();
        }
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    void MoveToTop()
    {
        //start back at the top of the screen plus new random x and y coordinate.
        float randomNumber = Random.Range(-2.5f, 2.5f);
        float randomY = Random.Range(8.0f, 12.0f);
        transform.position = new Vector2(randomNumber, randomY);
    }

    //if collide with player, then move back up top
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Square")
        {
            MoveToTop();
        }
    }
}





