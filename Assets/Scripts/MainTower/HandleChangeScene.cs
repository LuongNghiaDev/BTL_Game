using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleChangeScene : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LobbyManager.Ins.SetIndexMap();
            if(SceneManager.sceneCountInBuildSettings
               <= LobbyManager.Ins.GetIndexMap())
            {
                StartCoroutine(LoadSceneAsync(0));
            } else
            {
                StartCoroutine(LoadSceneAsync(LobbyManager.Ins.GetIndexMap()));
            }
        }
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
