using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public float speed = 5;
    public Transform[] moveSpots;

    private float waitTime;
    private int position = 0;

    public float startWaitTime;

    public GameObject head;
    public GameObject blood;

    public playerController player;
	public gameController GC;

    private void Start()
    {
        waitTime = startWaitTime;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[position].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[position].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                if (position == 0)
                    position = 1;
                else
                    position = 0;

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        head.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);

    }
    private void OnDisable()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    
        if (collision.gameObject.CompareTag("Player"))
        {
            print("KILLING PLAYER");
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
			//FindObjectOfType<gameController>()
			GC.endGame();
            
            //collision.gameObject.SetActive(false);

        }

        if (collision.gameObject.CompareTag("shuriken"))
        {
            gameObject.SetActive(false);
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }
}
