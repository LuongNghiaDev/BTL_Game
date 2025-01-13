using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] int maxHealthPoint;

    private int healthPoint;
    public bool isBoss;
    [SerializeField]
    private AudioManager audioManager;
    public AudioClip defaultDeathSound;
    private bool isDeath = true;

    private void Start()
    {
        healthPoint = maxHealthPoint;
    }

    public void takeDamage(int damage)
    {
        healthPoint = Mathf.Max(0, healthPoint-damage);
        if(healthPoint == 0)
        {
            if (isBoss)
            {
                LobbyManager.Ins.main.SetActive(true);
                if (isDeath)
                {
                    AudioSource.PlayClipAtPoint(defaultDeathSound, transform.position);
                    isDeath = false;
                }
            }
        }
    }

    public void healing(int healthRecover)
    {
        healthPoint = Mathf.Min(healthRecover+healthPoint, maxHealthPoint);
    }

    public int getHealthPoint()
    {
        return healthPoint;
    }

    public int getMaxHealthPoint()
    {
        return maxHealthPoint;
    }
}
