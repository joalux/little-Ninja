using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    private static gameController instance;

    public float moveSpeed = 10;
    public float jumpForce = 10;

    public GameObject throwButton, moveR, moveL, jum;

    public bool movingRight = false, gameHasEnded = false;

    public Text scoreText, playerLives;

    public GameObject door, water1, shuriken;

    public Rigidbody2D playerBody;

    public GameObject enemyHead, enemy, levelComplete, triggerPoint, checkpoint;

    public GameObject[] checkpoints;

    public int activeCheckpoint;

    private float screenWidht, screenHeight;

    float moveInput;

    public playerController player;

    public enemySpawner[] spawners;

    private AssetBundle levelBundle;
    private string[] scenePaths;

    public Vector2 lastCheckPos;

    


    private void OnEnable()
    {
        print("ENABLING!!!!!");
        print(activeCheckpoint);
       /* buttonInput.moveLeft += moveLeft;
        buttonInput.moveRight += moveRight;
        buttonInput.stopMoving += stopMoving;
        buttonInput.jump += jump;*/
        playerBody.position = checkpoints[activeCheckpoint].transform.position;
    }
    private void OnDisable()
    {
        /*buttonInput.moveLeft -= moveLeft;
        buttonInput.moveRight -= moveRight;
        buttonInput.stopMoving -= stopMoving;
        buttonInput.jump -= jump;*/
    }

    private void Start()
    {
      

        screenWidht = Screen.width;
        screenHeight = Screen.height;


        
    }

   
   

    public void endGame()
    {
        print("GameController ending game");
        if(player.hasWon == true)
        {

            print("GameComplete!!");
            SceneManager.LoadScene("winScene");

        }


        player.lives--;
     
         if(player.lives > 0)
         {

                player.respawn();
            }
            else
            {
                Debug.Log("GAME OVER MAN!");

                SceneManager.LoadScene("endScene");
            }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }


    private void Update()
    {

          
    }

    private void FixedUpdate()
    {
        scoreText.text = player.points.ToString();
        playerLives.text = player.lives.ToString();
       
        if(player.points == 40)
        {
            Destroy(door);
        }
    }

   

     }

    

