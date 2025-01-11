using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperController : BaseEnemy
{
    [Header("Attack")]
    [SerializeField] float attackDashRange;
    [SerializeField] float attackDashVelocity;

    private void Update()
    {
        wake();
        DeathControl();
    }

    #region event region

    // stomp section
    //private void stompControl()
    //{
    //    animator.SetTrigger("Stomp");
    //    objRigidbody.position = playerTransform.position + Vector3.up * stompHeight;
    //}

    //public void stompFallEvent()
    //{
    //    objRigidbody.velocity = Vector2.down * stompFallVelocity;
    //}
    #endregion
}
