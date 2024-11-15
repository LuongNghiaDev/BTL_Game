using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIStartLevel : UICanvas
{

    [Title("Button")]
    [SerializeField] private Button btn1;
    [SerializeField] private Button btn2;
    [SerializeField] private Button btn3;
    //[SerializeField] private GameObject ;

    private void Start()
    {
        btn1.onClick.AddListener(OnClickBtnStart1);
        btn2.onClick.AddListener(OnClickBtnStart2);
        btn3.onClick.AddListener(OnClickBtnStart3);
    }

    private void OnClickBtnStart1()
    {
        SceneManager.LoadScene("QueenGarden");
        OnBackPressed();
    }

    private void OnClickBtnStart2()
    {
        SceneManager.LoadScene("Grimm");
        OnBackPressed();
    }

    private void OnClickBtnStart3()
    {
        SceneManager.LoadScene("GodTamer");
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
