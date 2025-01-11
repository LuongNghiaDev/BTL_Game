using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIInfo : UICanvas
{
    [SerializeField] private Button btnOpenGit;
    private string url = "https://github.com/LuongNghiaDev/BTL_Game";

    // Start is called before the first frame update
    void Start()
    {
        btnOpenGit.onClick.AddListener(OnClickBtnOpenGit);
    }

    private void OnClickBtnOpenGit()
    {
        Application.OpenURL(url);
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
