using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapingHuskBehavior : StateMachineBehaviour
{
    // Start is called before the first frame update
    private Transform LeapingHuskTransform;
    private Transform playerTransform;
    private Rigidbody2D LeapingHuskRigidbody;
    private LeapingHuskController leapingHuskController;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        LeapingHuskTransform = animator.GetComponent<Transform>();
        LeapingHuskRigidbody = animator.GetComponent<Rigidbody2D>();
        leapingHuskController = animator.GetComponent<LeapingHuskController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Attack");
        //if (Vector2.Distance(playerTransform.position, LeapingHuskTransform.position) < attackRange && !isActive)
        //{

        //}
        //else
        //{

        //}
    }

    private void moveToPlayerPosition()
    {
        //leapingHuskController.lookAtPlayer();

        //Vector2 target = new Vector2(playerTransform.position.x, LeapingHuskTransform.position.y);
        //Vector2 newPosition = Vector2.MoveTowards(LeapingHuskTransform.position, target, runSpeed * Time.fixedDeltaTime);
        //LeapingHuskRigidbody.MovePosition(newPosition);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
    }
}
