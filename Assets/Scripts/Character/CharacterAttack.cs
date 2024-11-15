using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Character : IAttack
{

    [Header("Sphere Section")]
    [SerializeField] float airSphereInterval;
    [SerializeField] float groundSphereInterval;
    [SerializeField] float sphereCooldownInterval;
    private bool isSphereable;

    [Header("Javelin Throw Section")]
    [SerializeField] float javelinThrowAnticipateInterval;
    [SerializeField] float javelinThrowInterval;
    [SerializeField] float javelinThrowCooldownInterval;
    [SerializeField] GameObject javelin;
    [SerializeField] Transform javelinStartPoint;
    private bool isJavelinThrowable;

    #region javelin throw character
    public void JavelinThrow()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJavelinThrowable && !isInSkillInterval && isInputable && !isClimb)
        {
            StartCoroutine(javelinThowCoroutine());
        }
    }

    private IEnumerator javelinThowCoroutine()
    {
        isJavelinThrowable = false;
        animator.SetTrigger("JavelinThrow");
        audioManager.playSound("JavelinThrow");
        audioManager.playSound("JavelinThrowYell");

        yield return new WaitForSeconds(javelinThrowAnticipateInterval);

        // create an instance of javelin
        Instantiate(javelin, javelinStartPoint.position, javelinStartPoint.rotation);

        yield return new WaitForSeconds(javelinThrowInterval);
        animator.ResetTrigger("JavelinThrow");

        yield return new WaitForSeconds(javelinThrowCooldownInterval);
        isJavelinThrowable = true;
    }

    #endregion

    #region sphere character

    public void Sphere()
    {
        if (Input.GetKeyDown(KeyCode.E) && isSphereable && !isInSkillInterval && isInputable)
        {
            StartCoroutine(sphereCoroutine());
        }
    }

    private IEnumerator sphereCoroutine()
    {

        freezeGravity();
        isInSkillInterval = true;
        audioManager.playSound("Sphere");

        isSphereable = false;
        if (isGround)
        {
            audioManager.playSound("GroundSphereYell");
            animator.SetTrigger("GroundSphere");
            yield return new WaitForSeconds(groundSphereInterval);
        }
        else
        {
            audioManager.playSound("AirSphereYell");
            animator.SetTrigger("AirSphere");
            yield return new WaitForSeconds(airSphereInterval);
        }

        unFreezeGravity();

        animator.ResetTrigger("GroundSphere");
        animator.ResetTrigger("AirSphere");
        isInSkillInterval = false;

        yield return new WaitForSeconds(sphereCooldownInterval);
        isSphereable = true;
    }

    #endregion
}
