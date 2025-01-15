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

    private float limitTime = 1.5f;
    private float lastActiveTime;
    private bool canAttive = true;

    [SerializeField] private float attackActiveRange;


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
        if (Mathf.Abs(playerTransform.position.x - MawlurkTransform.position.x) < attackActiveRange && canAttive)
        {
            lastActiveTime = Time.time;
            animator.Play("Active");
            mawlurkController.shootEvent();
            canAttive = false;
        }
        if (!canAttive && Time.time - lastActiveTime > limitTime)
        {
            canAttive = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Active");
        animator.ResetTrigger("Sleep");
    }

}