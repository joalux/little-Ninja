using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {
    public GameObject blood;
    public playerController player;

    void Update () 
	{
		Destroy(gameObject, 5);
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            print("Collision!!!!!!!!!!!!!");
            
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
            gameObject.SetActive(false);
          
            FindObjectOfType<gameController>().endGame();
        }
		



	}
    

}
