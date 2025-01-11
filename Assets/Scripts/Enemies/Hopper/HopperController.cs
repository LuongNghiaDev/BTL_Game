using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperController : BaseEnemy
{
    [Header("Attack")]
    //[SerializeField] private float attackRange;
    [SerializeField]

    private bool isJumping = true;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpHeight = 5f;

    public bool IsJumping => isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        wake();
        DeathControl();
    }

    public void Jump()
    {
        
        if (!isJumping) return;
        isJumping = false;
        Vector2 jumpVelocity = new Vector2(rb.velocity.x,
            Mathf.Sqrt(jumpHeight * (-2) * (Physics2D.gravity.y * rb.gravityScale)));

        rb.velocity = jumpVelocity;
    }

    public void lookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < playerTransform.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > playerTransform.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Grass"))
        {
            isJumping = true;
        }
    }
}
