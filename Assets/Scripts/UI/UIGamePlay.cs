using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : UICanvas
{
    [SerializeField]
    private UIPause uIPause;

    /*private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            uIPause.Show(true);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            uIPause.Show(false);
        }
    }*/

    public void OpenAllMap()
    {
        GUIManager.Instance.ActiveUIAllMap(true);
    }

    public void OpenUiSetting()
    {
        GUIManager.Instance.ActiveUISetting(true);
    }
}
