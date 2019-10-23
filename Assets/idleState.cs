using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : StateMachineBehaviour
{
    Transform playerTransform;

    int distanceHash = Animator.StringToHash("playerDistance");

    private float timer;

    public float minTime;
    public float maxTime;



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timer = Random.Range(minTime, maxTime);
        animator.gameObject.GetComponent<finalBossController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 current = animator.transform.position;
        Vector2 target = playerTransform.transform.position;

        int health = animator.GetComponent<finalBossController>().health;
        animator.SetInteger("bossHealth", health);


        float distance = Vector2.Distance(current, target);
        animator.SetFloat("playerDistance", distance);

        if (timer <= 0)
        {
            animator.SetTrigger("fighting");


        }
        else
        {
            timer -= Time.deltaTime;
        }

        

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
