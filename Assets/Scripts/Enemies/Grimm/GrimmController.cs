using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimmController : BaseEnemy
{
    [Header("Attack")]
    [SerializeField] float attackDashRange;
    [SerializeField] float attackDashVelocity;

    [Header("Stomp")]
    [SerializeField] float stompHeight;
    [SerializeField] float stompFallVelocity;

    [Header("Evade")]
    [SerializeField] float evadeRange;
    [SerializeField] float evadeVelocity;

    [Header("Cast Bullet Point")]
    [SerializeField] Transform castPoint;
    [SerializeField] Transform castPoint2;
    [SerializeField] GameObject orb;
    public bool isBoss;

    private void Update()
    {
        wake();
        DeathControl();
    }

    #region event region
    public void teleportOutEvent()
    {
        int randSkill = Random.Range(0, 2);

        if (randSkill == 0)
        {
            stompControl();
        }
        else
        {
            castControl();
        }
    }

    public override void OnDie()
    {
        base.OnDie();
    }

    //attack slash section
    public void attackDashEvent()
    {
        int direction = isFlipped == true ? 1 : -1;
        Vector2 newPosition = Vector2.MoveTowards(objRigidbody.position, objRigidbody.position + Vector2.right * attackDashRange * direction,
            attackDashVelocity * Time.fixedDeltaTime);
        objRigidbody.MovePosition(newPosition);
    }

    // stomp section
    private void stompControl()
    {
        animator.SetTrigger("Stomp");
        objRigidbody.position = playerTransform.position + Vector3.up * stompHeight;
    }

    public void stompFallEvent()
    {
        objRigidbody.velocity = Vector2.down * stompFallVelocity;
    }


    // Evade section
    public void evadeControl()
    {
        int direction = isFlipped == true ? -1 : 1;
        Vector2 newPosition = Vector2.MoveTowards(objRigidbody.position, objRigidbody.position + Vector2.right * evadeRange * direction, evadeVelocity * Time.fixedDeltaTime);
        objRigidbody.MovePosition(newPosition);
    }


    // cast section
    private void castControl()
    {
        animator.SetTrigger("Cast1");
    }

    public void castOrbEvent()
    {
        // create an instance of orb
        Instantiate(orb, castPoint.position, castPoint.rotation);
    }

    public void castOrbEvent1()
    {
        // create an instance of orb
        Instantiate(orb, castPoint2.position, castPoint2.rotation);
    }

    #endregion
}
