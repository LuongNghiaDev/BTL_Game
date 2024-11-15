using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulTwisterController : BaseEnemy
{

    [Header("Cast Bullet Point")]
    [SerializeField] Transform castPoint;
    [SerializeField] GameObject orb;

    protected override void Start()
    {
        base.Start();
        gravityScale = 1;
    }

    private void Update()
    {
        wake();
        DeathControl();
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
