using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChangeScene : MonoBehaviour
{
    [SerializeField]
    private GameObject main;

    private void Start()
    {
        main.SetActive(false);
    }

    private void OnEnable()
    {
        LobbyManager.Ins.OnActiveMainChangeScene += ActiveObj;
    }

    private void ActiveObj()
    {
        //main.SetActive(true);
    }

    private void OnDisable()
    {
        LobbyManager.Ins.OnActiveMainChangeScene -= ActiveObj;
    }
}
