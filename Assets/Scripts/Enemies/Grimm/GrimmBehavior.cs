using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimmBehavior : StateMachineBehaviour
{
    [SerializeField] float runSpeed;

    private Transform grimmTransform;
    private Transform playerTransform;
    private Rigidbody2D grimmRigidbody;
    private GrimmController grimmController;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        grimmTransform = animator.GetComponent<Transform>();
        grimmRigidbody = animator.GetComponent<Rigidbody2D>();
        grimmController = animator.GetComponent<GrimmController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!grimmController.getIsSkill())
        {
            moveToPlayerPosition();
            skillControl(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
        animator.ResetTrigger("Attack3");
        animator.ResetTrigger("AttackNail");
        animator.ResetTrigger("Teleport");
        animator.ResetTrigger("Evade");
        animator.ResetTrigger("Stomp");
        animator.ResetTrigger("Cast");
    }

    private void moveToPlayerPosition()
    {
        grimmController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, grimmTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(grimmRigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        grimmRigidbody.MovePosition(newPosition);

    }


    private void skillControl(Animator animator)
    {
        int randSkill = Random.Range(0, 6);

        if (randSkill == 0)
        {
            animator.SetTrigger("Attack1");
        }
        else if (randSkill == 1)
        {
            animator.SetTrigger("Evade");
        }
        else if (randSkill == 2)
        {
            animator.SetTrigger("Attack2");
        }
        else if (randSkill == 3)
        {
            animator.SetTrigger("Attack3");
        }
        else if (randSkill == 4)
        {
            animator.SetTrigger("AttackNail");
        }
        else
        {
            animator.SetTrigger("Teleport");
        }

    }
}
