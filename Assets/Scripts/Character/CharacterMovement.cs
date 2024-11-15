using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : IMovement
{
    [Header("Movement Section")]
    [SerializeField] float runSpeed;


    [Header("Climb Jump Section")]
    [SerializeField] float climbJumpRange;
    [SerializeField] float climbJumpHeight;
    [SerializeField] float climbJumpInterval;
    private float xVelocity;


    [Header("Jump Section")]
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpAnticipateInterval;


    #region movement character
    public void Movement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");

        if (!isClimb && !isFreezeInputMovement)
        {
            // xVelocity of climb jump
            controllerRigidbody.velocity = new Vector2(runSpeed * horizontalMovement * Time.fixedDeltaTime + xVelocity, controllerRigidbody.velocity.y);

            // Flip character
            if (horizontalMovement > 0 && isFacingLeft)
            {
                Flip();
            }
            else if (horizontalMovement < 0 && !isFacingLeft)
            {
                Flip();
            }
        }

        if (horizontalMovement == 0 || !isGround)
        {
            animator.SetBool("isRunning", false);
            audioManager.stopSound("Run");
            isRunning = false;
        }

        if (isGround && horizontalMovement != 0 && !isRunning)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);

            audioManager.playSound("Run");
        }
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 theScale = controllerTransform.localScale;
        theScale.x *= -1;
        controllerTransform.localScale = theScale;

        javelinStartPoint.Rotate(0, 180f, 0);
        GameObject.Find("Air_Dash_Effect_Point").GetComponent<Transform>().Rotate(0, 180f, 0);
    }

    #endregion

    #region jump character
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isInputable)
        {
            if (isClimb)
            {
                StartCoroutine(climbJumpCoroutine());
            }
            else if (isGround)
            {
                StartCoroutine(jumpCoroutine());
            }
        }

        if (isJumping)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                isJumping = false;
            }
        }
    }

    private IEnumerator jumpCoroutine()
    {
        isJumping = true;
        animator.SetTrigger("Jump");
        audioManager.playSound("Jump");

        // wait for Jump_Anticipate animation to be finished before playing Jump animation
        yield return new WaitForSeconds(jumpAnticipateInterval);

        // Jumping a specific height using Velocity
        // Using time-independent acceleration formula to calculate velocity 
        Vector2 jumpVelocity = new Vector2(controllerRigidbody.velocity.x, Mathf.Sqrt(jumpHeight * (-2) * (Physics2D.gravity.y * controllerRigidbody.gravityScale)));

        controllerRigidbody.velocity = jumpVelocity;
    }

    private IEnumerator climbJumpCoroutine()
    {
        // Jumping a specific range and time interval
        // Using projectile motion equations to calculate velocity
        float yVelocity = climbJumpHeight / climbJumpInterval;

        xVelocity = climbJumpRange / climbJumpInterval;

        // Jump opposite direction
        xVelocity = isFacingLeft == true ? xVelocity : -xVelocity;

        animator.SetTrigger("ClimbJump");
        audioManager.playSound("ClimbJump");
        controllerRigidbody.velocity = new Vector2(xVelocity, yVelocity);

        yield return new WaitForSeconds(climbJumpInterval);

        xVelocity = 0f;
    }

    #endregion

    // other section
    private void freezeGravity()
    {
        controllerRigidbody.gravityScale = 0;
        isFreezeInputMovement = true;
        controllerRigidbody.velocity = Vector2.zero;
    }

    private void unFreezeGravity()
    {
        isFreezeInputMovement = false;
        controllerRigidbody.gravityScale = gravityScale;
    }
}
