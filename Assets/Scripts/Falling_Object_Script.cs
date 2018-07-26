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
        //where the object starts
        transform.position = new Vector2(randomXstart, 8.0f);   
    }

    // Update is called once per frame
    void Update()
    {
        //move out of bounds on y axis
        if (transform.position.y <= -5.5f)
        {
            Destroy(gameObject);
        }
        //drop speed
        transform.Translate(Vector2.down * Time.deltaTime * speed);
    }

    //if collide with player, then move back up top
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if it touches player, destroy object
        if (collision.gameObject.name == "Square")
        {
            Destroy(gameObject);
        }
    }
}





