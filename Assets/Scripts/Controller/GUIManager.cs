using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : Singleton<GUIManager>
{

    [SerializeField]
    private GameObject UISetting;
    [SerializeField]
    private GameObject UIAllMap;
    [SerializeField]
    private GameObject UiMainLobby;
    [SerializeField]
    private GameObject UiGamePlay;
    [SerializeField]
    private GameObject uiHome;

    private void Update()
    {
        if(Input.GetKey(KeyCode.M))
        {
            ActiveUIAllMap(true);
        }
    }

    public void OpenHome()
    {
        uiHome.SetActive(true);
        UiGamePlay.SetActive(false);
    }

    public void OpenUIGamePlay()
    {
        SceneManager.LoadScene(0);
        uiHome.SetActive(false);
        UiGamePlay.SetActive(true);
    }

    public void ActiveUIAllMap(bool isActive)
    {
        UIAllMap.SetActive(isActive);
    }

    public void ActiveUIMainLobby(bool isActive)
    {
        UiMainLobby.SetActive(isActive);
    }

    public void ActiveUISetting(bool isActive)
    {
        UISetting.SetActive(isActive);
    }
}
