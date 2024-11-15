using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSentryBehavior : StateMachineBehaviour
{
    [SerializeField] float runSpeed;
    [SerializeField] float attackActiveRange;
    [SerializeField] float evadeActiveRange;


    private Rigidbody2D greatSentryRigidbody;
    private Transform greatSentryTransform;
    private GreatSentryController greatSentryController;
    private Transform playerTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        greatSentryTransform = animator.GetComponent<Transform>();
        greatSentryRigidbody = animator.GetComponent<Rigidbody2D>();
        greatSentryController = animator.GetComponent<GreatSentryController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(playerTransform.position, greatSentryTransform.position) < evadeActiveRange)
        {
            animator.Play("Evade");
        }
        else
        {
            moveToPlayerPosition();
            attackControl(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slash");
        animator.ResetTrigger("Shoot");
        animator.ResetTrigger("Evade");
    }

    private void moveToPlayerPosition()
    {
        greatSentryController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, greatSentryTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(greatSentryRigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        greatSentryRigidbody.MovePosition(newPosition);
    }


    private void attackControl(Animator animator)
    {
        if (Vector2.Distance(playerTransform.position, greatSentryTransform.position) < attackActiveRange
        && !greatSentryController.getIsSkill())
        {
            int skillRandom = Random.Range(0, 3);
            greatSentryController.setIsSkill();

            if (skillRandom == 0)
            {
                animator.SetTrigger("Slash");
            }
            else if (skillRandom == 1)
            {
                animator.SetTrigger("Evade");
            }
            else
            {
                animator.SetTrigger("Shoot");
            }
        }
    }
}
