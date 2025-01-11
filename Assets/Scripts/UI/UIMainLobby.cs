using Sirenix.OdinInspector;
//using System.Runtime.Hosting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class UIMainLobby : UICanvas
{

    [Title("Text")]
    [SerializeField] private Button btnStart;
    [SerializeField] private UIStartLevel uIStartLevel;
    [SerializeField] private Button btnSetting;
    [SerializeField] private UISetting uISetting;
    [SerializeField] private Button btnInfo;
    [SerializeField] private UIInfo uIInfo;
    [SerializeField] private Button btnQuit;
    [SerializeField] private Button btnBack;
    //[SerializeField] private UIStartLevel uIStartLevel;

    public UICanvas[] uiElements; // Mảng chứa các UI cần ẩn

    private void Start()
    {
        btnStart.onClick.AddListener(OnClickBtnStart);
        btnSetting.onClick.AddListener(OnClickBtnSetting);
        btnInfo.onClick.AddListener(OnClickBtnInfo);
        btnQuit.onClick.AddListener(OnClickBtnQuit);
        btnBack.onClick.AddListener(OnClickBtnBack);
        btnBack.gameObject.SetActive(false);
    }

    private void OnClickBtnStart()
    {
        uIStartLevel.Show(true);
        btnBack.gameObject.SetActive(true);
    }

    private void OnClickBtnSetting()
    {
        uISetting.Show(true);
        btnBack.gameObject.SetActive(true);
    }

    private void OnClickBtnInfo()
    {
        btnBack.gameObject.SetActive(true);
        uIInfo.Show(true);
    }

    private void OnClickBtnBack()
    {
        uIStartLevel.Show(false);
        uISetting.Show(false);
        uIInfo.Show(false);
        btnBack.gameObject.SetActive(false);
    }

    private void OnClickBtnQuit()
    {
        Application.Quit();
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
