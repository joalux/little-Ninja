using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endState : StateMachineBehaviour
{
    private float timer;
    public float starttime;

    Transform playerTransform;

    public float speed;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<finalBossController>();
        animator.GetComponent<finalBossController>().sword.SetActive(false);

        animator.GetComponent<finalBossController>().changeSprite();


        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        timer = starttime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<finalBossController>();

        Vector2 currentPos = animator.transform.position;
        Vector2 target = playerTransform.transform.position;

        float current = animator.transform.position.x;
        float player = playerTransform.transform.position.x;

        float distance = current - player;

        animator.SetFloat("playerDistance", distance);

        if (distance > 0)
        {
            animator.transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.gameObject.GetComponent<finalBossController>().facingRight = false;


        }
        if (distance < 0)
        {
   
            animator.GetComponent<finalBossController>().sword.SetActive(false);
            animator.transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.gameObject.GetComponent<finalBossController>().facingRight = true;
        }


        if (timer <= 0)
        {

            animator.GetComponent<finalBossController>().throwSaw();
            timer = starttime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        //Vector2 targetVector = new Vector2(playerTransform.position.x, playerTransform.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
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
