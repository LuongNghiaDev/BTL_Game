using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamerBehavior : StateMachineBehaviour
{

    [SerializeField] float runSpeed;
    [SerializeField] float attackActiveRange;
    [SerializeField] float evadeActiveRange;


    private Rigidbody2D rigidbody;
    private Transform enemyTransform;
    private TamerCtrl controller;
    private Transform playerTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        enemyTransform = animator.GetComponent<Transform>();
        rigidbody = animator.GetComponent<Rigidbody2D>();
        controller = animator.GetComponent<TamerCtrl>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(playerTransform.position, enemyTransform.position) < evadeActiveRange)
        {
            //animator.Play("Dash");
        }
        else
        {
            animator.SetTrigger("SlashAnti");
            //moveToPlayerPosition();
            //attackControl(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Slash");
        animator.ResetTrigger("SlashRecover");
        animator.ResetTrigger("EvadeAnti");
        animator.ResetTrigger("Death");
        animator.ResetTrigger("Dash");
    }

    private void moveToPlayerPosition()
    {
        controller.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, enemyTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPosition);
    }


    private void attackControl(Animator animator)
    {
        if (Vector2.Distance(playerTransform.position, enemyTransform.position) < attackActiveRange
        && !controller.getIsSkill())
        {
            int skillRandom = Random.Range(0, 3);
            controller.setIsSkill();

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
