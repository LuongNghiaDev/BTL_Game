using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainLobby : UICanvas
{

    [Title("Text")]
    [SerializeField] private Button btnStart;
    [SerializeField] private UIStartLevel uIStartLevel;

    private void Start()
    {
        btnStart.onClick.AddListener(OnClickBtnStart);
    }

    private void OnClickBtnStart()
    {
        uIStartLevel.Show(true);
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
