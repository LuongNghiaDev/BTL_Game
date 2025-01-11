using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInfo : UICanvas
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Start is called before the first frame update
    public override void Show(bool _isShown, bool isHideMain = true)
    {
        base.Show(_isShown, isHideMain);

        if (!isShow)
        {
            return;
        }
    }
}
