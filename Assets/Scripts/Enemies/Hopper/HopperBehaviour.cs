using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperBehaviour : StateMachineBehaviour
{

    private Transform HopperTransform;
    private Transform playerTransform;
    private Rigidbody2D HopperRigidbody;
    private HopperController hopperController;
    private float evadeActiveRange = 5;
    private float runSpeed = 3;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        HopperTransform = animator.GetComponent<Transform>();
        HopperRigidbody = animator.GetComponent<Rigidbody2D>();
        hopperController = animator.GetComponent<HopperController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(playerTransform.position, HopperTransform.position) < evadeActiveRange)
        {
            // animator.Play("Evade");
        }
        else
        {
            hopperController.Jump();
            if(!hopperController.IsJump)
            {
                moveToPlayerPosition();
            }
        }
    }

    private void moveToPlayerPosition()
    {
        hopperController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, HopperTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(HopperTransform.position, target, runSpeed * Time.fixedDeltaTime);
        HopperRigidbody.MovePosition(newPosition);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }

}
