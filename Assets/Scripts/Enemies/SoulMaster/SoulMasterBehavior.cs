using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMasterBehavior : StateMachineBehaviour
{
    [SerializeField] float runSpeed;

    private Transform soulMasterTransform;
    private Transform playerTransform;
    private Rigidbody2D soulMasterRigidbody;
    private SoulMasterController soulMasterController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        soulMasterTransform = animator.GetComponent<Transform>();
        soulMasterRigidbody = animator.GetComponent<Rigidbody2D>();
        soulMasterController = animator.GetComponent<SoulMasterController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!soulMasterController.getIsSkill())
        {
            moveToPlayerPosition();
            skillControl(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Teleport");
        animator.ResetTrigger("Quake");
    }

    private void moveToPlayerPosition()
    {
        soulMasterController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, soulMasterTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(soulMasterRigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        soulMasterRigidbody.MovePosition(newPosition);

    }


    private void skillControl(Animator animator)
    {
        animator.SetTrigger("Teleport");

    }
}
