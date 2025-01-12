using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLanceSentry : MonoBehaviour
{
    [SerializeField] Vector2 velocity;
    [SerializeField] float lifeInterval;
    [SerializeField] float breakSoundInterval;

    private Rigidbody2D projectitleRigidbody;
    private AudioSource audioSource;

    private Transform playerTransform;

    private void Awake()
    {
        projectitleRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Fly();   
    }

    private void Fly()
    {

        //float distance = Mathf.Abs(transform.position.x - playerTransform.position.x);

        Vector2 target = new Vector2(playerTransform.position.x, playerTransform.position.y);
        Vector2 newPosition = Vector2.MoveTowards(projectitleRigidbody.position, target, 20 * Time.fixedDeltaTime);
        projectitleRigidbody.MovePosition(newPosition);
    }

    private void Start()
    {
        StartCoroutine(destroyCoroutine(lifeInterval));
    }


    private IEnumerator destroyCoroutine(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (audioSource)
        {
            audioSource.Play();
        }
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Character>().takeDamage(1);
            StartCoroutine(destroyCoroutine(breakSoundInterval));
        }
        else if (collider.gameObject.tag == "Grass")
        {
            projectitleRigidbody.gravityScale = 0;
            projectitleRigidbody.velocity = Vector2.zero;
            StartCoroutine(destroyCoroutine(breakSoundInterval));
        }
    }

}
