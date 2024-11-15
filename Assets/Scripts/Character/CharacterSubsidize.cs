using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character: ISubsidize
{

    [Header("Evade Section")]
    [SerializeField] float evadeDistance;
    [SerializeField] float evadeAnticipateInterval;
    [SerializeField] float evadeInterval;
    [SerializeField] float evadeCooldownInterval;
    private bool isEvadeable;

    [Header("Dash Section")]
    [SerializeField] float groundDashAnticipateInterval;
    [SerializeField] float groundDashInterval;
    [SerializeField] float groundDashRecoverInterval;
    [SerializeField] float groundDashDistance;
    [SerializeField] float groundDashEffectInterval;

    [SerializeField] float airDashAnticipateInterval;
    [SerializeField] float airDashVelocity;
    [SerializeField] float airDashEffectInterval;
    [SerializeField] float airDashRecoverInterval;

    [SerializeField] float dashCooldownInterval;
    private bool isDashable;
    [SerializeField]
    private bool isAirDash;

    [Header("Recoil Section")]
    [SerializeField] float recoilHeight;
    [SerializeField] float recoilCooldownInterval;

    #region dash character
    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isDashable && !isInSkillInterval && isInputable)
        {
            if (isClimb)
            {
                Flip();
            }

            StartCoroutine(dashCoroutine());
        }
    }

    private IEnumerator dashCoroutine()
    {
        isDashable = false;
        isInSkillInterval = true;
        freezeGravity();

        audioManager.playSound("Dash");
        if (isGround)
        {
            audioManager.playSound("GroundDashYell");
            animator.SetTrigger("GroundDash");
            yield return new WaitForSeconds(groundDashAnticipateInterval);

            effect.dashEffect("GroundDash", groundDashEffectInterval, isFacingLeft, 0);

            int direction = isFacingLeft == true ? -1 : 1;
            Vector2 dashVelocity = Vector2.right * direction * (groundDashDistance / groundDashInterval);
            controllerRigidbody.velocity = dashVelocity;
            yield return new WaitForSeconds(groundDashInterval);


            // add a force to stop the character
            // the force is calculated from the first kinematic formula and Newton's second law
            controllerRigidbody.AddForce(controllerRigidbody.mass * (-dashVelocity * 2 / groundDashRecoverInterval));
            animator.SetTrigger("DashRecover");
            yield return new WaitForSeconds(groundDashRecoverInterval);


            unFreezeGravity();
            animator.ResetTrigger("GroundDash");
            animator.ResetTrigger("DashRecover");
            isInSkillInterval = false;

            yield return new WaitForSeconds(dashCooldownInterval);

            isDashable = true;
        }
        else
        {
            audioManager.playSound("AirDashYell");
            isAirDash = true;
            animator.SetTrigger("AirDash");
            yield return new WaitForSeconds(airDashAnticipateInterval);

            int direction = isFacingLeft == true ? -1 : 1;
            controllerRigidbody.velocity = new Vector2(direction * airDashVelocity, -airDashVelocity);

            controllerTransform.rotation = Quaternion.Euler(0f, 0f, -direction * 45);

            effect.dashEffect("AirDash", airDashEffectInterval, isFacingLeft, 45);
        }
    }

    public IEnumerator airDashRecoverCoroutine(bool isRecoil)
    {

        controllerTransform.rotation = Quaternion.Euler(0f, 0f, 0f);

        unFreezeGravity();

        isAirDash = false;
        animator.ResetTrigger("AirDash");


        if (isRecoil)
        {
            // recoil a specific height using Velocity
            // Using time-independent acceleration formula to calculate velocity 
            Vector2 recoilVelocity = new Vector2(controllerRigidbody.velocity.x, Mathf.Sqrt(recoilHeight * (-2) * (Physics2D.gravity.y * controllerRigidbody.gravityScale)));
            controllerRigidbody.velocity = recoilVelocity;

            // if air dash hit the enemy or trap will reset the skill
            animator.SetTrigger("AirRecoil");
            isDashable = true;
            isInSkillInterval = false;
            isJumping = true;

            yield return new WaitForSeconds(recoilCooldownInterval);
            animator.ResetTrigger("AirRecoil");
            isJumping = false;
        }
        else
        {
            // recoil a specific height using Velocity
            // Using time-independent acceleration formula to calculate velocity 
            Vector2 recoilVelocity = new Vector2(controllerRigidbody.velocity.x, Mathf.Sqrt((-2f) * (Physics2D.gravity.y * controllerRigidbody.gravityScale)));
            controllerRigidbody.velocity = recoilVelocity;

            animator.SetTrigger("DashRecover");
            isInSkillInterval = false;
            isJumping = true;

            yield return new WaitForSeconds(airDashRecoverInterval);
            animator.ResetTrigger("DashRecover");
            isJumping = false;

            yield return new WaitForSeconds(dashCooldownInterval);
            isDashable = true;
        }
    }

    #endregion

    #region evade character
    public void Evade()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && isGround && isEvadeable && isInputable)
        {
            StartCoroutine(evadeCoroutine());
        }
    }

    private IEnumerator evadeCoroutine()
    {
        freezeGravity();

        isEvadeable = false;
        audioManager.playSound("EvadeLaugh");
        audioManager.playSound("Dash");
        animator.SetTrigger("Evade");

        yield return new WaitForSeconds(evadeAnticipateInterval);

        int direction = isFacingLeft == true ? 1 : -1;
        controllerRigidbody.velocity = Vector2.right * direction * (evadeDistance / evadeInterval);

        yield return new WaitForSeconds(evadeInterval);
        unFreezeGravity();
        animator.ResetTrigger("Evade");

        yield return new WaitForSeconds(evadeCooldownInterval);
        isEvadeable = true;
    }

    #endregion
}
