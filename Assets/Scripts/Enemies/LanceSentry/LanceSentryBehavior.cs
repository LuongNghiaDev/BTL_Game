using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceSentryBehavior : StateMachineBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float attackActiveRange;
    //[SerializeField] float shieldActiveRange;
    [SerializeField] float evadeActiveRange;


    private Rigidbody2D mossKnightRigidbody;
    private Transform mossKnightTransform;
    private LanceSentryCtrl mossKnightController;
    private Transform playerTransform;


    private Vector3 curPosPlayer;
    private Vector3 newPosPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        mossKnightTransform = animator.GetComponent<Transform>();
        mossKnightRigidbody = animator.GetComponent<Rigidbody2D>();
        mossKnightController = animator.GetComponent<LanceSentryCtrl>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        curPosPlayer = playerTransform.position;

        /*if (Vector2.Distance(playerTransform.position, mossKnightTransform.position) < evadeActiveRange)
        {
            *//*if (IsPlayerIdle())
            {
                moveToPlayerPosition();
            }
            else
            {
                mossKnightController.shootEvent();
            }*//*
            animator.SetTrigger("Attack");
            newPosPlayer = playerTransform.position;
        }
        else
        {
            animator.SetTrigger("Fly");
            moveToPlayerPosition();
            newPosPlayer = playerTransform.position;
        }*/
        attackControl(animator);
        moveToPlayerPosition();
    }

    private bool IsPlayerIdle()
    {
        if (curPosPlayer == newPosPlayer)
        {
            return true;
        }
        return false;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    private void moveToPlayerPosition()
    {
        mossKnightController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, mossKnightTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(mossKnightRigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        mossKnightRigidbody.MovePosition(newPosition);
    }


    private void attackControl(Animator animator)
    {
        if (Vector2.Distance(playerTransform.position,
               mossKnightTransform.position) < attackActiveRange)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

}
