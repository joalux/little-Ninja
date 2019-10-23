using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChaseController : MonoBehaviour
{

    public GameObject head, blood;

    private Transform playerTransform;

    public float speed, jumpSpeed, jumpForce;

    public bool isHiding;

    public Rigidbody2D rb;
   
    // Start is called before the first frame update
    void Start()
    {
        head.transform.position = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(isHiding == false)
        {
           transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

        }

    }
    private void FixedUpdate()
    {
        head.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("platform"))
        {
            Vector2 jumpVector = new Vector2(jumpSpeed, jumpForce);

            rb.velocity = jumpVector;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            print("KILLING PLAYER");
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
       
            FindObjectOfType<gameController>().endGame();

           // collision.gameObject.SetActive(false);

        }
        if (collision.gameObject.CompareTag("shuriken"))
        {
            gameObject.SetActive(false);
            Instantiate(blood, transform.position, Quaternion.identity);

        }
    }
}
