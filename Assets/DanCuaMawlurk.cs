using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanCuaMawlurk : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Vector2 velocity;
    [SerializeField] float lifeInterval;
    [SerializeField] float breakSoundInterval;

    private Rigidbody2D projectitleRigidbody;
    private AudioSource audioSource;

    private void Awake()
    {
        projectitleRigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Start()
    {
        //StartCoroutine(destroyCoroutine(lifeInterval));
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator destroyCoroutine(float interval)
    {
        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        audioSource.Play();
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Character>().takeDamage(1);
        }
        else if (collider.gameObject.tag == "Grass")
        {
            projectitleRigidbody.gravityScale = 0;
            projectitleRigidbody.velocity = Vector2.zero;
            spriteRenderer.sprite = newSprite;
        }
    }
}
