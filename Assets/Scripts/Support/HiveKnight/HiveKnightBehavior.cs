using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveKnightBehavior : StateMachineBehaviour
{

    [SerializeField] float runSpeed;

    private Transform hiveKnightTransform;
    private Transform enemyTransform;
    private Rigidbody2D hiveknightRigidbody;
    private HiveKnightController hiveknightController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;

        hiveKnightTransform = animator.GetComponent<Transform>();
        hiveknightRigidbody = animator.GetComponent<Rigidbody2D>();
        hiveknightController = animator.GetComponent<HiveKnightController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hiveknightController.getIsSkill())
        {
            moveToPlayerPosition();
            skillControl(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Teleport");
        animator.ResetTrigger("DashTeleport");
        animator.ResetTrigger("DashRecover");
        animator.ResetTrigger("Evade");
        animator.ResetTrigger("Stomp");
        animator.ResetTrigger("Cast");
    }

    private void moveToPlayerPosition()
    {
        hiveknightController.lookAtPlayer();

        Vector2 target = new Vector2(enemyTransform.position.x, hiveKnightTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(hiveknightRigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        hiveknightRigidbody.MovePosition(newPosition);

    }


    private void skillControl(Animator animator)
    {
        int randSkill = Random.Range(0, 3);

        if (randSkill == 0)
        {
            animator.SetTrigger("Attack");
        }
        else if (randSkill == 1)
        {
            animator.SetTrigger("Evade");
        }
        else
        {
            animator.SetTrigger("Teleport");
        }

    }
}
