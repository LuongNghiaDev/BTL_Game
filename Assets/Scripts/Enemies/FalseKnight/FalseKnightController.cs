using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseKnightController : BaseEnemy
{
    public HealthController healthCtrl => healthController;
    private AudioManager audioManager;

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
