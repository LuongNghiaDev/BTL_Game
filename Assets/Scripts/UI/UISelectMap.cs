using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISelectMap : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> lockMap;


    private void OnEnable()
    {
        CheckOpenLockMap();
    }

    private void CheckOpenLockMap()
    {
        int indexMap = LobbyManager.Ins.GetIndexMap();

        if(indexMap >= 2)
        {
            lockMap[0].SetActive(false);
        }
        if (indexMap >= 3)
        {
            lockMap[1].SetActive(false);
        }
        if (indexMap >= 4)
        {
            lockMap[2].SetActive(false);
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
