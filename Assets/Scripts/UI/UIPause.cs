using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : UICanvas
{


    private void Start()
    {
    }

    public void OnClickBackHome()
    {
        GUIManager.Instance.OpenHome();
        OnClose();
    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);

        if (!isShow)
        {
            return;
        }
    }
}
