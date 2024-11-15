using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamer2Controller : BaseEnemy
{

    [Header("Cast Bullet Point")]
    [SerializeField] Transform castPoint;
    [SerializeField] GameObject orb;

    private void Update()
    {
        wake();
        DeathControl();
    }

    private void RayCasting()
    {
        Physics2D.IgnoreLayerCollision(19, 13);
    }

    #region event region
    public void teleportOutEvent()
    {
        castControl();
    }

    // cast section
    private void castControl()
    {
        animator.SetTrigger("Cast");
    }

    public void castOrbEvent()
    {
        // create an instance of orb
        Instantiate(orb, castPoint.position, castPoint.rotation);
    }

    #endregion

}
