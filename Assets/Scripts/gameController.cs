using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public float moveSpeed = 10;
    public float jumpForce = 10;

    public GameObject throwButton;

    public bool movingRight = false;

    public Text scoreText;

    public GameObject door, water, water1, shuriken;


    public Rigidbody2D playerBody;

    public GameObject enemyHead, enemy, levelComplete, triggerPoint;

    private float screenWidht, screenHeight;

    float moveInput;

    public playerController player;

    private AssetBundle levelBundle;
    private string[] scenePaths;

    private void OnEnable()
    {
        buttonInput.moveLeft += moveLeft;
        buttonInput.moveRight += moveRight;
        buttonInput.stopMoving += stopMoving;
        buttonInput.jump += jump;




    }
    private void OnDisable()
    {
        buttonInput.moveLeft -= moveLeft;
        buttonInput.moveRight -= moveRight;
        buttonInput.stopMoving -= stopMoving;
    }

    private void Start()
    {
      

        screenWidht = Screen.width;
        screenHeight = Screen.height;


        
    }

   
    public void moveLeft()
    {
        player.move(false);
    }
    public void moveRight()
    {
        player.move(true);
    }
    public void stopMoving()
    {
        
            player.stop();
        
    }
    public void jump()
    {
        player.jump();
    }


    private void Update()
    {

          
    }

    private void FixedUpdate()
    {
        scoreText.text = player.points.ToString();
        if(player.points == 40)
        {
            Destroy(door);
        }
    }

   

     }

    

