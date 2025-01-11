using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawlurkBehaviour : StateMachineBehaviour
{

    private Transform MawlurkTransform;
    private Transform playerTransform;
    private Rigidbody2D MawlurkRigidbody;
    private MawlurkController mawlurkController;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        MawlurkTransform = animator.GetComponent<Transform>();
        MawlurkRigidbody = animator.GetComponent<Rigidbody2D>();
        mawlurkController = animator.GetComponent<MawlurkController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Active");
    }

}