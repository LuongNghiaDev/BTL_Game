using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightController : BaseEnemy
{
    public HealthController healthCtrl => healthController;
    private AudioManager audioManager;
/*
    protected Transform playerTransform;
    protected Rigidbody2D objRigidbody;
    protected Animator animator;
    public HealthController healthController;

    protected bool isSleep;
    protected bool isDeath;
    protected bool isBoss;*/

/*    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        healthController = GetComponent<HealthController>();
        objRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        audioManager = GetComponent<AudioManager>();

        isSleep = true;
        isBoss = true;
        isDeath = false;
    }*/

    private void Update()
    {
        wake();
        DeathControl();
    }

/*    public void wake()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) <= activeDistance && isSleep)
        {
            animator.SetTrigger("Wake");
            isSleep = false;

            MusicController musicController = FindObjectOfType<MusicController>();
            StartCoroutine(musicController.fightPrepareCoroutine());
        }
    }

    public void DeathControl()
    {
        if (healthController.getHealthPoint() <= 0 && !isDeath)
        {
            animator.SetTrigger("Death");

            MusicController musicController = FindObjectOfType<MusicController>();
            musicController.fightEndCoroutine();
            isDeath = true;
            if (isBoss)
            {
                LobbyManager.Ins.OnActiveMainChangeScene?.Invoke();
            }
        }
    }*/
}
