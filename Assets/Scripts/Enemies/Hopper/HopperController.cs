using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperController : BaseEnemy
{
    private HealthController healthCtrl => healthController;
    [SerializeField]
    private bool isJumping = true;
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpHeight = 5f;

    protected bool isBoss;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grass"))
        {
            isJumping = true;
        }
    }

    public HealthController getHealthController()
    {
        return healthController;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    public new void lookAtPlayer()
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
}
