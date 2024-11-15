using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatSentryController : BaseEnemy
{

    [SerializeField] float evadeRange;
    [SerializeField] float takeDamageForce;
    //death
    [SerializeField] float airDeathInterval;
    [SerializeField] float landDeathInterval;

    [Header("Shoot Section")]
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject sickle;

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
        // create an instance of projectile
        GameObject instance = (GameObject)Instantiate(sickle, shotPoint.position, shotPoint.rotation);

        int direction = isFlipped == true ? 1 : -1;
        Vector2 velocity = new Vector2(15 * direction, 0);

        instance.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    public void slashEvent()
    {
        int randSound = Random.Range(1, 5);
        audioManager.playSound("AttackYell" + randSound);
        audioManager.playSound("Slash");
    }
    #endregion
}
