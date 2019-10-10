using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideBehaviour : StateMachineBehaviour
{
    float distanceToChase = 2f;
    Transform playerPos, enemyPos;

    public float speed = 3.0f;





    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 player = playerPos.position;

        float distance = Vector2.Distance(current, player);

        animator.SetFloat("playerDistance", distance);

       /* if (distance < distanceToChase)
        {
            
            Debug.Log("----------CHASING_--------");
            animator.transform.position = Vector2.MoveTowards(current, player, speed * Time.deltaTime);
        }*/


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
