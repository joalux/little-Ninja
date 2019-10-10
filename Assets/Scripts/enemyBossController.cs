using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyBossController : MonoBehaviour
{
    public int health = 10, throwForce = 10;
    public Rigidbody2D bossBody;
    public GameObject blood, sawPrefab;
    public Slider healthBar;
    public Image bossIcon;
    public bool isActive = false, startFignting = false;

    public playerController player;

    public Transform firePoint;

    private List<GameObject> sawList;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.gameObject.SetActive(false);
        bossIcon.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        healthBar.value = health;

       

    }

    public void startBossFight()
    {
        print("BOSS FIGHT COMMENCING!!!");

        StartCoroutine(attackSequence());
    }

    IEnumerator attackSequence()
    {
        print("is in FightSequence");
        while(player.isAlive == true)
        {
            GameObject saw = Instantiate(sawPrefab);


            saw.transform.position = firePoint.transform.position;
            Rigidbody2D sawRB = saw.GetComponent<Rigidbody2D>();
            print("throwFORCE");
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

            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            print("KILLING PLAYER");
            Instantiate(blood, collision.gameObject.transform.position, Quaternion.identity);
            player.isAlive = false;
            collision.gameObject.SetActive(false);

        }
    }
}
