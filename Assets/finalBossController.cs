using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class finalBossController : MonoBehaviour
{
    public GameObject shurikenPrefab, player, blood, sawPrefab, sword;

    public Slider healthBar;

    public Transform target, firePoint;

    public float throwForce = 100f, speed = 5f;

    public int health = 10;

    public bool canThrow = false, isActive = true, facingRight = false;

    public playerController plyer;

    public Sprite angryState;

    private SpriteRenderer SR;

    public gameController GC;

    // Start is called before the first frame update
    void Start()
    {

        SR = GetComponent<SpriteRenderer>();

    }

    public void changeSprite()
    {

        print("CHANGING SPRITE!!!!!!!!!!!!");
        SR.sprite = angryState;
    }

    public void attack()
    {

            StartCoroutine(attackSequence());
       
           // throwShuriken();

    }

    public void throwSaw()
    {
        GameObject saw = Instantiate(sawPrefab);
        saw.transform.position = firePoint.transform.position;
        Rigidbody2D sawRB = saw.GetComponent<Rigidbody2D>();
        print(throwForce);
        if (facingRight)
        {
            print("throwing right");
            sawRB.velocity = new Vector2(throwForce, sawRB.velocity.y);
        }
        else
        {
            print("throwing left");
            sawRB.velocity = new Vector2(throwForce * -1, sawRB.velocity.y);

        }



    }

    public void throwShuriken()
    {
        
            print("TROWING");
            GameObject shuriken = Instantiate(shurikenPrefab);

            Physics2D.IgnoreCollision(shuriken.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());

            shuriken.transform.position = firePoint.transform.position;
            Rigidbody2D shuriRB = shuriken.GetComponent<Rigidbody2D>();
            Vector2 direction = target.position - firePoint.transform.position;

            print(direction);

            shuriRB.AddForce(direction * throwForce);
        
          
    }

    IEnumerator attackSequence()
    {
        print("is in FightSequence");
        if (health < 5 )
        {
            print("is in Final stage");

            throwSaw();

            yield return new WaitForSeconds(2.0f);

        }

    }

    /*
    IEnumerator waitTime()
    {
        print("Waiting");
        canThrow = false;
        yield return new WaitForSeconds(3);
        canThrow = true;
        print("WE WAITED");
        throwShuriken();
    }*/

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - firePoint.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        firePoint.transform.rotation = Quaternion.Slerp(firePoint.transform.rotation, rotation, speed * Time.deltaTime);

        healthBar.value = health;



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("shuriken"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            print("HEALTH");
            print(health);
            collision.gameObject.SetActive(false);
            health--;
            if (health == 0)
            {
                print("GAME COMPLETE");
                isActive = false;
                gameObject.SetActive(false);
                StopCoroutine(attackSequence());
                plyer.hasWon = true;
                GC.endGame();
            }
        }
       /* if (collision.gameObject.CompareTag("Player"))
        {
            print("KILLING PLAYER");
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
            plyer.isAlive = false;
            collision.gameObject.SetActive(false);
            StopCoroutine(attackSequence());


        }*/
    }

}
