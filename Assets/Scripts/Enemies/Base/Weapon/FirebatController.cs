using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebatController : WeaponBase
{

    private Vector3 previousPosition;
    private bool isFacingRight = true;

    private void Update()
    {
        if (!isHit)
        {
            firebatFollowPlayer();
        }
    }

    private void OnEnable()
    {
        CheckForDirectionChange();
    }

    public void firebatFollowPlayer()
    {
        Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(orbRigidbody.position, target, velocity * Time.fixedDeltaTime);
        orbRigidbody.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            animator.SetTrigger("Impact");
            collider.gameObject.GetComponent<Character>().takeDamage(1);
        }
        else if (collider.gameObject.layer == LayerMask.NameToLayer("Terrain")
        || collider.gameObject.layer == LayerMask.NameToLayer("WeaponPlayer"))
        {
            animator.SetTrigger("Impact");
            orbRigidbody.velocity = Vector2.zero;
            isHit = true;
        }
    }

    //flip
    private void CheckForDirectionChange()
    {
        Vector3 newPosition = transform.position;

        Vector3 movementVector = newPosition - previousPosition;

        if (movementVector.x < 0 && isFacingRight)
        {
            FlipObject();
        }
        else if (movementVector.x > 0 && !isFacingRight)
        {
            FlipObject();
        }

        previousPosition = newPosition;
    }

    private void FlipObject()
    {
        isFacingRight = !isFacingRight;

        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
