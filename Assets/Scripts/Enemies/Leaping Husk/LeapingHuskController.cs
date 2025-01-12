using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class LeapingHuskController : BaseEnemy
{
    [SerializeField]
    private Rigidbody2D rb;
    private bool isBoss;

    protected virtual void Start()
    {
        healthController = GetComponent<HealthController>();
        isBoss = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        wake();
        DeathControl();
    }

    public void wake()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) <= activeDistance && isSleep)
        {
            animator.SetTrigger("Wake");
            isSleep = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Character>().takeDamage(1);
        }
    }

    //public void DeathControl()
    //{
    //    if (healthController.getHealthPoint() <= 0 && !isDeath)
    //    {
    //        animator.SetTrigger("Death");

    //        //MusicController musicController = FindObjectOfType<MusicController>();
    //        //musicController.fightEndCoroutine();
    //        isDeath = true;
    //        if (isBoss)
    //        {
    //            LobbyManager.Ins.OnActiveMainChangeScene?.Invoke();
    //        }
    //    }
    //}


}
