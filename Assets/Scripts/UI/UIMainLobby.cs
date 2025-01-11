using Sirenix.OdinInspector;
using System.Collections;
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
        StartCoroutine(LoadSceneAsync(1));
    }

    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            yield return null;
        }

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
