using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightBehavior : StateMachineBehaviour
{
    [SerializeField] private float attackActiveRange = 15;
    [SerializeField] float runSpeed;

    private Transform false9Transform;
    private Transform playerTransform;
    private Rigidbody2D false9Rigidbody;
    private FalseKnightController false9Controller;
    private bool isFlipped = false;
    private bool isActive = false;
    private bool isNotAttacked = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        false9Transform = animator.GetComponent<Transform>();
        false9Rigidbody = animator.GetComponent<Rigidbody2D>();
        false9Controller = animator.GetComponent<FalseKnightController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(playerTransform.position, false9Transform.position) < attackActiveRange ||
                (false9Controller.healthCtrl.getMaxHealthPoint() != false9Controller.healthCtrl.getHealthPoint() && isNotAttacked))
        {
            //Debug.Log("Attack");
            if (false9Controller.healthCtrl.getMaxHealthPoint() != false9Controller.healthCtrl.getHealthPoint())
            {
                isNotAttacked = false;
            }
            if (!isActive)
            {
                isActive = true;
                attackActiveRange = 7;
            }
            animator.Play("Attack");
            Vector3 flipped = false9Transform.localScale;
            flipped.z *= -1f;
            if (false9Transform.position.x > playerTransform.position.x && !isFlipped)
            {
                false9Transform.localScale = flipped;
                false9Transform.Rotate(0f, 180f, 0f);
                isFlipped = true;
            }
            else if (false9Transform.position.x < playerTransform.position.x && isFlipped)
            {
                false9Transform.localScale = flipped;
                false9Transform.Rotate(0f, 180f, 0f);
                isFlipped = false;
            }
        }
        else
        {
            if (!isActive)
            {
                return;
            }
            //Debug.Log("Here");
            if(false9Controller.healthCtrl.getHealthPoint() == 0)
            {
                animator.SetTrigger("Death");
            } else
            {
                animator.SetTrigger("Run");
                moveToPlayerPosition();
            }
            //skillControl(animator);
            //shieldControl(animator);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Death");
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Run");
    }

    private void moveToPlayerPosition()
    {
        //false9Controller.lookAtPlayer();
        Vector3 flipped = false9Transform.localScale;
        flipped.z *= -1f;

        if (false9Transform.position.x > playerTransform.position.x && isFlipped)
        {
            false9Transform.localScale = flipped;
            false9Transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (false9Transform.position.x < playerTransform.position.x && !isFlipped)
        {
            false9Transform.localScale = flipped;
            false9Transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
        Vector2 target = new Vector2(playerTransform.position.x, false9Transform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(false9Rigidbody.position, target, runSpeed * Time.fixedDeltaTime);
        false9Rigidbody.MovePosition(newPosition);
    }

}
