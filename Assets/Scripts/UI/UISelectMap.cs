using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISelectMap : MonoBehaviour
{

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
