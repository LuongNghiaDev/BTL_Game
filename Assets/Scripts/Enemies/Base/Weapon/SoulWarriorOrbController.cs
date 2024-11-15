using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulWarriorOrbController : WeaponBase
{

    private void Update()
    {
        if (!isHit)
        {
            followPlayer();
        }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            animator.SetTrigger("Impact");
            collider.gameObject.GetComponent<Character>().takeDamage(1);
        }
        else if(collider.gameObject.layer == LayerMask.NameToLayer("Terrain")
        || collider.gameObject.layer == LayerMask.NameToLayer("WeaponPlayer"))
        {
            animator.SetTrigger("Impact");
            orbRigidbody.velocity = Vector2.zero;
            isHit = true;
        }
    }
}
