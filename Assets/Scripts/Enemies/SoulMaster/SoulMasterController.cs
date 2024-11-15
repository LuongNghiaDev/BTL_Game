using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulMasterController : BaseEnemy
{

    [Header("Quake")]
    [SerializeField] float quakeHeight;
    [SerializeField] float quakeFallVelocity;

    [Header("FX")]
    [SerializeField] GameObject land;
    [SerializeField] GameObject hitGround1;
    [SerializeField] GameObject hitGround2;

    private void Update()
    {
        wake();
        DeathControl();
    }

    #region event region
    public void teleportOutEvent()
    {
        quakeControl();
    }

    // quake section
    private void quakeControl()
    {
        animator.SetTrigger("Quake");
        objRigidbody.position = playerTransform.position + Vector3.up * quakeHeight;
    }

    public void quakeFallEvent()
    {
        objRigidbody.velocity = Vector2.down * quakeFallVelocity;
    }

    public void cameraShakeEvent()
    {
        Instantiate(land, transform.position, Quaternion.identity);
        Instantiate(hitGround1, transform.position, Quaternion.identity);
        Instantiate(hitGround2, transform.position, Quaternion.identity);
        CineController.Instance.ShakeTrigger();
    }

    #endregion
}
