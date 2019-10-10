using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
	void Update () 
	{

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            print("Collision!!!!!!!!!!!!!");

            gameObject.SetActive(false);
        }
    }
}
