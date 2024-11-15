using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NailController : MonoBehaviour
{
    [SerializeField] float lifeInterval;
    [SerializeField] GameObject parent;

    private AudioSource audioSource;
    private Animator animator;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(destroyCoroutine());
    }

    private IEnumerator destroyCoroutine()
    {
        yield return new WaitForSeconds(lifeInterval);
        animator.SetTrigger("Down");
    }

    public void destroyEvent()
    {
        parent.SetActive(false);
    }

    public void playSound()
    {
        audioSource.Play();
    }


}
