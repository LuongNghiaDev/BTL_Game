using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructorObj : MonoBehaviour
{
    [SerializeField]
    private float curClose = 1.2f;

    private void Update()
    {
        StartCoroutine(DelayClose());
        StopCoroutine(DelayClose());
    }

    IEnumerator DelayClose()
    {
        if (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(curClose);
            gameObject.SetActive(false);
        }
    }

}
