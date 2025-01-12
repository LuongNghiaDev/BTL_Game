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

    [SerializeField] float runSpeed = 5;
    [SerializeField] private float attackActiveRange = 15;
    private bool isTurnRight = false;
    private float lastWalkTime;
    private float limitedWalkTime = 3f;
    private bool canWalk = true;


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
        if (Vector2.Distance(playerTransform.position, LeapingHuskTransform.position) < attackActiveRange)
        {
            animator.SetTrigger("Attack");
            runSpeed = 10;
            moveToPlayerPosition();
        }
        else
        {
            runSpeed = 5;
            animator.SetTrigger("Walk");
            if (canWalk)
            {
                if (isTurnRight)
                {
                    lastWalkTime = Time.time;
                    walkAround(-1);
                    isTurnRight = false;
                }
                else
                {
                    lastWalkTime = Time.time;
                    walkAround(1);
                    isTurnRight = true;
                }
                canWalk = false;
            }    
        }
        if (Time.time - lastWalkTime >= limitedWalkTime && !canWalk)
        {
            canWalk = true;
        }
    }

    private void moveToPlayerPosition()
    {
        leapingHuskController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, LeapingHuskTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(LeapingHuskTransform.position, target, runSpeed * Time.fixedDeltaTime);
        LeapingHuskRigidbody.MovePosition(newPosition);
    }

    private void walkAround(int side)
    {
        Vector2 target = new Vector2(LeapingHuskTransform.position.x + 10 * side, LeapingHuskTransform.position.y);
        Vector2 newPosition = Vector2.Lerp(LeapingHuskTransform.position, target, 1);
        LeapingHuskRigidbody.MovePosition(newPosition);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Walk");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Land");
    }
}