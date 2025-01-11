using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperBehaviour : StateMachineBehaviour
{

    private Transform HopperTransform;
    private Transform playerTransform;
    private Rigidbody2D HopperRigidbody;
    private HopperController hopperController;


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

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }

}
