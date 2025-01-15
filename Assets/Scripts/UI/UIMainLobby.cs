using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainLobby : UICanvas
{

    [Title("Text")]
    [SerializeField] private Button btnStart;
    [SerializeField] private UISelectMap uiSelectMap;
    [SerializeField] private Button btnSetting;
    [SerializeField] private UISetting uISetting;
    [SerializeField] private Button btnInfo;
    [SerializeField] private UIInfo uIInfo;    
    [SerializeField] private Button btnItem;
    [SerializeField] private ItemDaCo uIItem;
    [SerializeField] private Button btnQuit;
    [SerializeField] private Button btnBack;
    [SerializeField] private UIIntro uIIntro;
    [SerializeField] private GameObject[] subUIs;
    private bool isShowedIntro = false;
    private void Start()
    {
        PlayerPrefs.SetInt("PlayDauTruong", -1);
        btnStart.onClick.AddListener(OnClickBtnStart);
        btnSetting.onClick.AddListener(OnClickBtnSetting);
        btnInfo.onClick.AddListener(OnClickBtnInfo);
        btnItem.onClick.AddListener(OnClickBtnItem);
        btnQuit.onClick.AddListener(OnClickBtnQuit);
        btnBack.onClick.AddListener(OnClickBtnBack);
        btnBack.gameObject.SetActive(false);
        if (!isShowedIntro)
        {
            ShowIntro();
            isShowedIntro = true;
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnClickBtnBack();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                foreach (GameObject menu in subUIs)
                {
                    if (hitObject != menu)
                    {
                        menu.SetActive(false);
                        btnBack.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void ShowIntro()
    {
        if (PlayerPrefs.HasKey("IntroCucVui"))
        {
            return;
        }
        uIIntro.Show(true);
        PlayerPrefs.SetInt("IntroCucVui", 1);
        PlayerPrefs.Save();
    }

    private void OnClickBtnStart()
    {
        uiSelectMap.gameObject.SetActive(true);
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

    private void OnClickBtnItem()
    {
        btnBack.gameObject.SetActive(true);
        uIItem.Show(true);
    }

    private void OnClickBtnBack()
    {
        Debug.Log("Back");
        uiSelectMap.Show(false);
        uISetting.Show(false);
        uIInfo.Show(false);
        uIItem.Show(false);
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
