using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] int maxHealthPoint;

    private int healthPoint;
    public bool isBoss;

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
                Debug.Log("Change Scene");
                LobbyManager.Ins.OnActiveMainChangeScene?.Invoke();
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
