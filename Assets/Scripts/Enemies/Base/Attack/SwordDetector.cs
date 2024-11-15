using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDetector : MonoBehaviour
{   
    [SerializeField] int swordDamage;

    Character character;

    // Start is called before the first frame update
    private void Start()
    {
        character = FindObjectOfType<Character>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            character.takeDamage(swordDamage);
        }
    }
}