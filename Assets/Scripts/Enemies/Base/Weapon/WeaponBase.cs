using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] protected float velocity;
    [SerializeField] float lifeInterval;

    protected Rigidbody2D orbRigidbody;
    private AudioSource audioSource;
    protected Animator animator;
    protected Transform playerTransform;

    protected bool isHit;

    protected virtual void Start()
    {
        orbRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("PlayerCentreOfGravity").transform;
        isHit = false;
        StartCoroutine(destroyCoroutine());
    }

    public void followPlayer()
    {
        Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(orbRigidbody.position, target, velocity * Time.fixedDeltaTime);
        orbRigidbody.MovePosition(newPosition);
    }


    private IEnumerator destroyCoroutine()
    {
        yield return new WaitForSeconds(lifeInterval);
        animator.SetTrigger("Impact");
    }

    public void destroyEvent()
    {
        Destroy(gameObject);
    }

    public void playSound()
    {
        audioSource.Play();
    }
}
