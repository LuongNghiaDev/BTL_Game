using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISelectMap : UICanvas
{
    [SerializeField]
    private List<GameObject> lockMap;


    private void OnEnable()
    {
        //CheckOpenLockMap();
    }

    private void CheckOpenLockMap()
    {
        int indexMap = LobbyManager.Ins.GetIndexMap();

        for (int i = 0; i < lockMap.Count; i++)
        {
            lockMap[i].SetActive(indexMap < i + 2);
        }
    }

    public void OpenMap(int indexScene)
    {
        SceneManager.LoadScene(indexScene);
        OnClose();
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

}
