using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UISetting : UICanvas
{
    //[Title("Button")]
    [SerializeField] private Button btnShowFPS;

    private void Start()
    {
        btnShowFPS.onClick.AddListener(OnClickBtnShowFPS);
    }

    private void OnClickBtnShowFPS()
    {

    }

    public void OnClose()
    {
        gameObject.SetActive(false);
    }

    public void OnBackHome()
    {
        SceneManager.LoadScene(0);
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
