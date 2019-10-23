using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBossController : MonoBehaviour
{
    public int health = 20, throwForce = 10, jumpForce = 10;
    public float distance;
    public Rigidbody2D bossBody;
    public GameObject blood, sawPrefab;
    public Slider healthBar;
    public Image bossIcon;
    public bool isActive = false, startFignting = false;

    public playerController player;
    public gameController GC;

    public Transform firePoint;

    Animator animator;


    private List<GameObject> sawList;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();



        distance = Vector2.Distance(transform.position, player.transform.position);
        print("distance");
        print(distance);

        healthBar.gameObject.SetActive(false);
        bossIcon.gameObject.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {

        healthBar.value = health;
        distance = Vector2.Distance(transform.position, player.transform.position);

        animator.SetFloat("playerDistance", distance);

    }

    public void startBossFight()
    {
        print("BOSS FIGHT COMMENCING!!!");
        healthBar.gameObject.SetActive(true);
        bossIcon.gameObject.SetActive(true);
        StartCoroutine(attackSequence());

        


    }

    IEnumerator attackSequence()
    {
        print("is in FightSequence");
       while(health > 0)
        {
            GameObject saw = Instantiate(sawPrefab);
            saw.transform.position = firePoint.transform.position;
            Rigidbody2D sawRB = saw.GetComponent<Rigidbody2D>();
            print(throwForce);
            sawRB.velocity = new Vector2(throwForce * -1, sawRB.velocity.y);

            yield return new WaitForSeconds(3.0f);

        }

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
            if(health == 0)
            {
                isActive = false;
                gameObject.SetActive(false);
                StopCoroutine(attackSequence());

            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            print("KILLING PLAYER");
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
            player.isAlive = false;
            
            //collision.gameObject.SetActive(false);
            StopCoroutine(attackSequence());
            GC.endGame();


        }
    }
}

