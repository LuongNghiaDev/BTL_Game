using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightController : BaseEnemy
{
    [Header("Attack")]
    [SerializeField] float attackDashRange;
    [SerializeField] float attackDashVelocity;

    private void Update()
    {
        wake();
        DeathControl();
    }

    //attack slash section
    public void attackDashEvent()
    {
        int direction = isFlipped == true ? 1 : -1;
        Vector2 newPosition = Vector2.MoveTowards(objRigidbody.position, objRigidbody.position + Vector2.right * attackDashRange * direction,
            attackDashVelocity * Time.fixedDeltaTime);
        objRigidbody.MovePosition(newPosition);
    }
}
