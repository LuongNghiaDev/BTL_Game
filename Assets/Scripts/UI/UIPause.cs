using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : UICanvas
{

    [Title("Button")]
    [SerializeField] private Button btnExit;

    private void Start()
    {
        btnExit.onClick.AddListener(OnClickBtnExit);
    }

    private void OnClickBtnExit()
    {
        SceneManager.LoadScene("Lobby");
        OnBackPressed();
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
