using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonInput : MonoBehaviour
{
    public delegate void ButtonPressed();
    public static event ButtonPressed moveRight, moveLeft, jump, stopMoving;
   
    public playerController player;

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

                if (moveLeft != null && hit.collider != null && hit.collider.tag == "Left")
                {
                    player.isJumping = false;
                    moveLeft();


                }
                else if (moveRight != null && hit.collider != null && hit.collider.tag == "Right")
                {
                    player.isJumping = false;
                    moveRight();
                    
                }
               

                else if (hit.collider != null && hit.collider.tag == "jump")
                {
                    jump();
                    player.isJumping = true;
                }

                else if (hit.collider != null && hit.collider.tag == "throw")
                {
                    if(player.canThrow == true)
                    {
                        player.throwShuriken();
                    }
                       
                }

              
            }
            if(touch.phase == TouchPhase.Ended)
            {

                Vector3 pos = Camera.main.ScreenToWorldPoint(touch.position);

                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (moveRight != null && hit.collider != null && hit.collider.tag == "Right")
                {

                    player.isJumping = false;
                    stopMoving();
                }

                else if (moveLeft != null && hit.collider != null && hit.collider.tag == "Left")
                {

                    player.isJumping = false;
                    stopMoving();
                }
               



            }



        }
    }

}
