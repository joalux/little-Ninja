using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class buttonInput : MonoBehaviour
{
    private static buttonInput instance;

    public delegate void ButtonPressed();
    //public static event ButtonPressed moveRight, moveLeft, jump, stopMoving;
   
    public playerController player;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
         
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        
    }

    void Update()
    {

        foreach(Touch touch in Input.touches)
        {
         
           if (touch.phase == TouchPhase.Began)
            {
               
                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

                if (hit.collider != null && hit.collider.tag == "Left")
                {
                    player.isJumping = false;
                    player.move(false);
                    //moveLeft();


                }
                else if (hit.collider != null && hit.collider.tag == "Right")
                {
                    player.isJumping = false;
                    player.move(true);
                    //moveRight();
                    
                }
               

                else if (hit.collider != null && hit.collider.tag == "jump")
                {
                    player.jump();
                    player.isJumping = true;
                }

                else if (hit.collider != null && hit.collider.tag == "throw" && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;

                    if (player.canThrow == true)
                    {
                        player.throwShuriken();
                        

                    }
                       
                }

              
            }
            if(touch.phase == TouchPhase.Ended)
            {

                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null && hit.collider.tag == "Right")
                {

                    player.isJumping = false;
                    player.stop();
                }

                else if (hit.collider != null && hit.collider.tag == "Left")
                {

                    player.isJumping = false;
                    player.stop();
                }
               



            }

           




        }
    }

}
