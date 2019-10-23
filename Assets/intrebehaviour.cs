using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intrebehaviour : StateMachineBehaviour
{
    private int rand;
    Transform playerTransform;

    int distanceHash = Animator.StringToHash("playerDistance");


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Finding player");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        /* rand = Random.Range(0, 2);

         Debug.Log("-------------SETTING TRIGGER____________");

         if (rand == 0)
         {
             animator.SetTrigger("idle");
         }
         else
         {
             animator.SetTrigger("fighting");
         }*/
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 player = playerTransform.transform.position;


        float distance = Vector2.Distance(current, player);

        animator.SetFloat("playerDistance", distance);


        if (distance < 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if (player.x > 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
