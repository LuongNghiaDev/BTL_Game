using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamerCtrl : BaseEnemy
{

    [SerializeField] private float evadeRange;
    [SerializeField] private float takeDamageForce;

    private AudioManager audioManager;

    protected override void Start()
    {
        audioManager = GetComponent<AudioManager>();
        base.Start();
    }

    private void Update()
    {
        wake();
        DeathControl();
    }

    #region event region
    public void evadeEvent()
    {
        int randSound = Random.Range(1, 5);
        int direction = isFlipped == true ? -1 : 1;
        objRigidbody.AddForce(new Vector2(direction * evadeRange, 0));

        audioManager.playSound("AttackYell" + randSound);
    }

    public void shootEvent()
    {
        
    }

    public void slashEvent()
    {
        int randSound = Random.Range(1, 5);
        audioManager.playSound("AttackYell" + randSound);
        audioManager.playSound("Slash");
    }
    #endregion
}
