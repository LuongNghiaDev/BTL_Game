using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopperController : BaseEnemy
{
    private HealthController healthController;
    [SerializeField]
    private bool isJumping = true;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float jumpHeight = 5f;

    protected bool isBoss;

    public bool IsJumping => isJumping;

    protected virtual void Start()
    {
        healthController = GetComponent<HealthController>();
        isBoss = false;
    }

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

    public void wake()
    {
        if (Vector2.Distance(playerTransform.position, transform.position) <= activeDistance && isSleep)
        {
            animator.SetTrigger("Wake");
            isSleep = false;

            MusicController musicController = FindObjectOfType<MusicController>();
            StartCoroutine(musicController.fightPrepareCoroutine());
        }
    }

    public void DeathControl()
    {
        if (healthController.getHealthPoint() <= 0 && !isDeath)
        {
            animator.SetTrigger("Death");
            Debug.Log("Death");

            isDeath = true;
            if (isBoss)
            {
                LobbyManager.Ins.OnActiveMainChangeScene?.Invoke();
            }
        }
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

    public HealthController getHealthController()
    {
        return healthController;
    }
}
