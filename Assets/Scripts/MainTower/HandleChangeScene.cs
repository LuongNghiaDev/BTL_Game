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
            LobbyManager.Ins.indexMap++;
            if(SceneManager.sceneCountInBuildSettings 
               > LobbyManager.Ins.indexMap)
            {
                LobbyManager.Ins.indexMap = 1;
                SceneManager.LoadScene(0);
            }
            SceneManager.LoadScene(LobbyManager.Ins.indexMap);
        }
    }
}
