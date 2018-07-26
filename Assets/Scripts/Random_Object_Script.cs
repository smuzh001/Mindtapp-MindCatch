using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Object_Script : MonoBehaviour {
    //gameobjects that will drop
    public GameObject prefab1, prefab2;
    //spawns every 2 seconds
    public float spawnRate = 2f;
	public float nextSpawn = 0f;
    //use this to get random number
    int randNumb;
 
	// Update is called once per frame
	void Update () {
		if(Time.time > nextSpawn)
        {
            randNumb = Random.Range(1, 3);

            //instantiate the number that was randomized
            switch(randNumb)
            {
                case 1:
                    Instantiate(prefab1);
                    break;
                case 2:
                    Instantiate(prefab2);
                    break;
            }
            nextSpawn = Time.time + spawnRate;
        }
	}
}
