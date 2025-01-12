using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperBehaviour : StateMachineBehaviour
{

    private Transform HopperTransform;
    private Transform playerTransform;
    private Rigidbody2D HopperRigidbody;
    private HopperController hopperController;
    [SerializeField] private float attackRange = 5;
    [SerializeField] private float runSpeed = 3;
    private bool isActive = false;
    private bool isNotAttacked = true;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        HopperTransform = animator.GetComponent<Transform>();
        HopperRigidbody = animator.GetComponent<Rigidbody2D>();
        hopperController = animator.GetComponent<HopperController>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Jump");
        if (Vector2.Distance(playerTransform.position, HopperTransform.position) < attackRange && !isActive ||
            (hopperController.getHealthController().getMaxHealthPoint() != hopperController.getHealthController().getHealthPoint() && isNotAttacked))
        {
            if (hopperController.getHealthController().getMaxHealthPoint() != hopperController.getHealthController().getHealthPoint())
            {
                isNotAttacked = false;
            }
            isActive = true;
        }
        else
        {
            if (!isActive)
            {
                return;
            }
            hopperController.Jump();
            if (!hopperController.IsJumping)
            {
                moveToPlayerPosition();
            }
        }
    }

    private void moveToPlayerPosition()
    {
        hopperController.lookAtPlayer();

        Vector2 target = new Vector2(playerTransform.position.x, HopperTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(HopperTransform.position, target, runSpeed * Time.fixedDeltaTime);
        HopperRigidbody.MovePosition(newPosition);
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Idle");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Wake");
    }

}
