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
                audioManager.playSound("Die");
                LobbyManager.Ins.main.SetActive(true);
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
}
