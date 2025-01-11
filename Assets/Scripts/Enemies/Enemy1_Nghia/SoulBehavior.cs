using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulBehavior : StateMachineBehaviour
{
    [SerializeField] float runSpeed;

    private Transform soulTransform;
    private Transform playerTransform;
    //private Rigidbody2D playerRigid;
    private Rigidbody2D soulRigidbody;
    private Soul soulCtrl;
    [SerializeField] private float evadeActiveRange;
    private float shootingRange = 8f;

    private Vector3 curPosPlayer;
    private Vector3 newPosPlayer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        soulTransform = animator.GetComponent<Transform>();
        soulRigidbody = animator.GetComponent<Rigidbody2D>();
        soulCtrl = animator.GetComponent<Soul>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(IsSkill())
        {
            PlaySkill(animator);
        }
        curPosPlayer = playerTransform.position;
        if (Vector2.Distance(playerTransform.position, soulTransform.position) < evadeActiveRange)
        {
            if(IsPlayerIdle())
            {
                PlaySkill(animator);
            }
            else
            {
                animator.Play("Evade");
            }
            newPosPlayer = playerTransform.position;
        }
        else
        {
            animator.SetTrigger("Run");
            moveToPlayerPosition();
            newPosPlayer = playerTransform.position;
        }
    }

    private bool IsPlayerIdle()
    {
        if(curPosPlayer == newPosPlayer)
        {
            return true;
        }
        return false;
    } 

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Death");
        animator.ResetTrigger("Evade");
        animator.ResetTrigger("Shoot");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");

    }

    private void moveToPlayerPosition()
    {
        soulCtrl.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(soulRigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        soulRigidbody.MovePosition(newPosition);

    }


    private void PlaySkill(Animator animator)
    {
        animator.SetTrigger("Shoot");
    }

    private bool IsSkill()
    {
        if (Vector2.Distance(playerTransform.position, soulTransform.position) > shootingRange)
        {
            return true;
        }
        return false;
    }
}
