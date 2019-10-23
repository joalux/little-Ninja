using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public float checkRadius;

    private float moveInput;
    private float jumpSpeed;
    
    //public SpriteRenderer sr;

    public bool faceingRight = true;
    public bool notMoving = true;
    public bool isGrounded, isJumping, isOnWindow;
    public bool canThrow = false;
    public bool isAlive = true;
    public bool hasWon = false;

    public Transform groundCheck;
   
    public LayerMask whatIsGround, whatIsWindow;

    public enemyChaseController[] enemies;
    public enemyController enemy;
    public enemyBossController boss;
    public gameController GC;
    public finalBossController finalBoss;

    public Rigidbody2D rb;

    private int extraJumps;
    public int extraJumpValue;
    public int points = 0;
    public int poolSize = 10;
    public int shuriAmmo = 0;

    public int lives = 10;

    private float ScreenWidht;

    public GameObject blood;
    public GameObject shurikenPrefab;
    public GameObject bossTrigger;

    public enemySpawner enemSpawner, enemSpawner2;

    public List<Rigidbody2D> shurikenPool = new List<Rigidbody2D>();

    public Transform firePoint;
    public float throwForce = 10f;

    private int throwIndex = 0;

    public float shootingRate = 10f;
    private float shootCooldown;

    private void Start()
    {
        faceingRight = true;
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        //InitObjectPool();
        ScreenWidht = Screen.width;
        isOnWindow = false;
        shootCooldown = shootingRate;

    }

    public void respawn()
    {
        
            print("RESPAWNING!!!!!!!!!!!!");
            print(GC.activeCheckpoint);
            transform.position = GC.checkpoints[GC.activeCheckpoint].transform.position;
      
            
    }
    /*void InitObjectPool()
    {

        for (int i = 0; i < poolSize; i++)
        {
            GameObject shuriken = Instantiate(shurikenPrefab);
            shuriken.SetActive(false);

            Rigidbody2D shuriRB = shuriken.GetComponent<Rigidbody2D>();
            shurikenPool.Add(shuriRB);
        }
    }*/
    public void throwShuriken()
    {
      
        if(canThrow == true && isAlive == true && shuriAmmo > 0)
        {
           
            GameObject shuriken = Instantiate(shurikenPrefab);
            shuriken.transform.position = firePoint.transform.position;
            Rigidbody2D shuriRB = shuriken.GetComponent<Rigidbody2D>();
            throwForce = 1000f;
            if (faceingRight == true)
            {

                shuriRB.AddForce(Vector2.right * throwForce);
            }
            else
            {
                shuriRB.AddForce(Vector2.left * throwForce);

            }
            shuriAmmo--;
        }
      

    }

    public void move(bool right)
    {
        notMoving = false;
        if(right == false)
        {
            
            if (faceingRight == true)
            {
                transform.Rotate(0f, 180f, 0f);
                firePoint.transform.Rotate(0f, 180f, 0f);
            }
            faceingRight = false;
            if (moveSpeed > 0)
                moveSpeed = moveSpeed * -1;

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        if(right == true)
        {

            if(faceingRight == false)
            {
               transform.Rotate(0f, 180f, 0f);
                firePoint.transform.Rotate(0f, 180f, 0f);
            }
            faceingRight = true;

            if (moveSpeed < 0)
                moveSpeed = moveSpeed * -1;

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

        }
    }


    public void jump()
    {
        isGrounded = false;
        if(notMoving == true)
        {
            jumpSpeed = 0;
        }
        else
        {

            jumpSpeed = moveSpeed;
        }
        print(jumpSpeed);
        Vector2 jumpVector = new Vector2(jumpSpeed, jumpForce);
        Vector2 extraJumpVecttor = new Vector2(jumpSpeed, jumpForce);

        if (extraJumps > 0)
            {
                
                 rb.velocity = jumpVector;
            }
        
        extraJumps--;
    }

    public void stop()
    {

        notMoving = true;
        rb.velocity = new Vector2(0, 0);
        notMoving = true;
  
       
    }

    public void checkGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isOnWindow = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsWindow);

    }

    private void Update()
    {
        checkGround();
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;

        }
        if(isOnWindow == true)
        {
            extraJumps = 1;
        }
    }

    void FixedUpdate()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("bossShuri"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            print("HEALTH");
            GC.endGame();

        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("shuriAmmo"))
        {
            print("MORE SHURIS");

            shuriAmmo = shuriAmmo + 10;

           other.gameObject.SetActive(false);

        }

        if (other.gameObject.CompareTag("sword"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            print("HEALTH");
            GC.endGame();


        }

        if (other.gameObject.CompareTag("bossShuri"))
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            print("HEALTH");
            GC.endGame();

            gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("enemyHead"))
        {
            other.gameObject.SetActive(false);
            Instantiate(blood, other.gameObject.transform.position, Quaternion.identity);
            points = points + 10;
            other.transform.parent.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("PickUp"))
        {
            Debug.Log("collecting!!!!!!!!!!");
            other.gameObject.SetActive(false);


            points = points + 10;
        }
        if (other.gameObject.CompareTag("levelComplete"))
        {
            Debug.Log("LEVEL COMPLETE");
            SceneManager.LoadScene("Level2");

        }
        if (other.gameObject.CompareTag("level2Complete"))
        {
            Debug.Log("LEVEL COMPLETE");
            SceneManager.LoadScene("Level3");

        }
        if (other.gameObject.CompareTag("water"))
        {
            respawn();

            //FindObjectOfType<gameController>().endGame();

            //gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("powerup"))
        {
            Debug.Log("----------Power up!-----------");
            canThrow = true;
            shuriAmmo = 100;
            other.gameObject.SetActive(false);


        }
        if (other.gameObject.CompareTag("spawnTrigger"))
        {
            if (enemSpawner.isSpawning == false)
            {
                enemSpawner.startSpawning();
            }
        }
        if (other.gameObject.CompareTag("spawnTrigger2"))
        {
            Debug.Log("SPAWNING");
            enemSpawner2.startSpawning();
            other.gameObject.SetActive(false);

            
        }
        if (other.gameObject.CompareTag("spawnStopper"))
        {

            Debug.Log("STOPPING SPAWNING");

            enemSpawner.stopSpawning();
                enemSpawner2.stopSpawning();
            
        }
   

    }
   
}
