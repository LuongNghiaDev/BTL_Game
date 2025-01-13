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
                // GetComponent<ItemDisplay>().ShowObtainedItem(itemSprite, Camera.main);
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

//  public void ShowObtainedItem(Sprite itemSprite, Camera camera)
//     {
//         itemImage.sprite = itemSprite;
//         StartCoroutine(ShowAndHideItem(camera));
//     }

//     public void HideObtainedItem()
//     {
//         gameObject.SetActive(false);
//     }

//     private IEnumerator ShowAndHideItem(Camera camera)
//     {
//         gameObject.SetActive(true); // Hiển thị vật phẩm

//         // Lấy vị trí của Camera trong không gian thế giới
//         Vector3 screenPos = camera.WorldToScreenPoint(transform.position);

//         // Đặt vị trí của vật phẩm trên màn hình Camera
//         transform.position = screenPos;

//         yield return new WaitForSeconds(5f); // Chờ trong 5 giây

//         gameObject.SetActive(false); // Ẩn vật phẩm
//     }

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
}
