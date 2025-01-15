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
    // [SerializeField] private GameObject startPoint1;
    // [SerializeField] private GameObject startPoint2;
    // [SerializeField] private GameObject startPoint3;
    // [SerializeField] private GameObject startPoint4;
    // [SerializeField] private GameObject startPoint5;
    // [SerializeField] private GameObject startPoint6;
    [SerializeField] private GameObject[] startPoints;
    private bool isDeath = true;

    private void Start()
    {
        healthPoint = maxHealthPoint;
        // startPoints = new GameObject[] { startPoint1, startPoint2, startPoint3, startPoint4, startPoint5, startPoint6 };
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
            if (PlayerPrefs.GetInt("PlayDauTruong") == 1)
            {
                if (PlayerPrefs.GetInt(gameObject.name) == 1 || gameObject.name == "Player")
                {
                    return;
                }
                int randomIndex = Random.Range(0, startPoints.Length);
                Transform spawnPoint = startPoints[randomIndex].transform;
                Instantiate(gameObject, spawnPoint.position, spawnPoint.rotation);
                PlayerPrefs.SetInt(gameObject.name, 1);
                PlayerPrefs.Save();
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
