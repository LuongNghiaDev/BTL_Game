using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] int maxHealthPoint;
    [SerializeField] private string nameOfBoss;
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
        healthPoint = Mathf.Max(0, healthPoint - damage);
        if (healthPoint == 0)
        {
            if (isBoss)
            {
                openGate();
                PlayerPrefs.SetInt(nameOfBoss, 1);
                PlayerPrefs.Save();
                if (isDeath)
                {
                    AudioSource.PlayClipAtPoint(defaultDeathSound, transform.position);
                    isDeath = false;
                }
            }
        }
    }

    private void openGate()
    {
        LobbyManager.Ins.main.SetActive(true);
    }

    public void healing(int healthRecover)
    {
        healthPoint = Mathf.Min(healthRecover + healthPoint, maxHealthPoint);
    }

    public int getHealthPoint()
    {
        return healthPoint;
    }

    public int getMaxHealthPoint()
    {
        return maxHealthPoint;
    }
    public int getHealthPercent()
    {
        return healthPoint * 100 / maxHealthPoint;
    }
}
