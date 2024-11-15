using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulTwisterBehavior : StateMachineBehaviour
{

    [SerializeField] float runSpeed;

    private Transform soulTwisterTransform;
    private Transform playerTransform;
    private Rigidbody2D soulTwisterRigidbody;
    private SoulTwisterController soulTwisterController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        soulTwisterTransform = animator.GetComponent<Transform>();
        soulTwisterRigidbody = animator.GetComponent<Rigidbody2D>();
        soulTwisterController = animator.GetComponent<SoulTwisterController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!soulTwisterController.getIsSkill())
        {
            moveToPlayerPosition();
            skillControl(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Teleport");
        animator.ResetTrigger("Cast");
    }


    private void moveToPlayerPosition()
    {
        soulTwisterController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, soulTwisterTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(soulTwisterRigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        soulTwisterRigidbody.MovePosition(newPosition);

    }


    private void skillControl(Animator animator)
    {
        int randSkill = Random.Range(0, 6);

        if (randSkill == 4)
        {
            animator.SetTrigger("Teleport");
        }
        else
        {
            animator.SetTrigger("Run");
        }
    }
}
