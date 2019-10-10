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
    
    public SpriteRenderer sr;

    public bool faceingRight = true;
    public bool notMoving = true;
    public bool isGrounded, isJumping;
    public bool canThrow = false;
    public bool isAlive = true;

    public Transform groundCheck;
   
    public LayerMask whatIsGround;

    public enemyChaseController[] enemies;
    public enemyController enemy;
    public enemyBossController boss;
    public gameController GC;

    public Rigidbody2D rb;

    private int extraJumps;
    public int extraJumpValue;
    public int points = 0;
    public int poolSize = 10;

    private float ScreenWidht;

    public GameObject blood;
    public GameObject shurikenPrefab;
    public GameObject bossTrigger;

    public enemySpawner enemSpawner;

    public List<Rigidbody2D> shurikenPool = new List<Rigidbody2D>();

    public Transform firePoint;
    public float throwForce = 10f;

    private int throwIndex = 0;

    private void Start()
    {
        faceingRight = true;
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
       InitObjectPool();
        ScreenWidht = Screen.width;
      

    }
    

    void InitObjectPool()
    {

        for (int i = 0; i < poolSize; i++)
        {
            GameObject shuriken = Instantiate(shurikenPrefab);
            shuriken.SetActive(false);

            Rigidbody2D shuriRB = shuriken.GetComponent<Rigidbody2D>();
            shurikenPool.Add(shuriRB);
        }


    }
    public void throwShuriken()
    {
        if(canThrow == true && isAlive == true)
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

    }

    private void Update()
    {
        checkGround();
        if (isGrounded == true)
        {
            extraJumps = extraJumpValue;

        }
    }

    void FixedUpdate()
    {
      
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("saw"))
        {
            gameObject.SetActive(false);

        }
            

        if (other.gameObject.CompareTag("enemyHead"))
        {
            other.gameObject.SetActive(false);
            Instantiate(blood, other.gameObject.transform.position, Quaternion.identity);

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
        if (other.gameObject.CompareTag("water"))
        {
            Debug.Log("----------DROWNED-----------");

            gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("powerup"))
        {
            Debug.Log("----------Power up!-----------");
            canThrow = true;
            other.gameObject.SetActive(false);

        }
        if (other.gameObject.CompareTag("spawnTrigger"))
        {
            if (enemSpawner.isSpawning == false)
            {
                enemSpawner.startSpawning();
            }
        }
        if (other.gameObject.CompareTag("spawnStopper"))
        {
            if (enemSpawner.isSpawning == true)
            {
                enemSpawner.stopSpawning();
            }
        }

        if (other.gameObject.CompareTag("bossTrigger"))
        {
            Debug.Log("BOSS BATTLE STARTING!!!");
            boss.healthBar.gameObject.SetActive(true);
            boss.bossIcon.gameObject.SetActive(true);
            bossTrigger.SetActive(false);
            
            boss.startBossFight();
        }
       
    }
   
}
