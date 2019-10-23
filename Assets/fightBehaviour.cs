using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightBehaviour : StateMachineBehaviour
{
    public float distance;
    public float attackTime = 10;
    public Transform playerTransform;

    int distanceHash = Animator.StringToHash("playerDistance");
    int fightingHash = Animator.StringToHash("isFighting");
    int healthHash = Animator.StringToHash("bossHealth");



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        enemyBossController enemyBoss = animator.GetComponent<enemyBossController>();

        enemyBoss.startBossFight();

        healthHash = Animator.StringToHash("health");


        if (playerTransform == null)
        {
            Debug.Log("finding player transform");
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }


        
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        enemyBossController enemyBoss = animator.GetComponent<enemyBossController>();

        int health = enemyBoss.health;

        Vector2 current = animator.transform.position;
        Vector2 player = playerTransform.position;

        float playerDistance = Vector2.Distance(current, player);
        animator.SetFloat(distanceHash, playerDistance);

        animator.SetFloat(healthHash, health);




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
