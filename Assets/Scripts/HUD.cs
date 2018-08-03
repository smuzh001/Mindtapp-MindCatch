using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {



    public Sprite[] HeartSprites;

    public Image HeartUI;
    private Player_Script player;

	// Use this for initialization
	void Start () {
        //Accesses the Player_Script Object of our player which is the Square
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Script>();
        
	}
	
	// Update is called once per frame
	void Update () {
        HeartUI.sprite = HeartSprites[player.lives];
	}
}
