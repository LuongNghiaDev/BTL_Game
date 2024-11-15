using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Title("Popup")]
    public GameObject popupWarning;
    public GameObject popupWarningBoss;

    [Title("Other")]
    public GameObject Loading;

    [SerializeField]
    private Button txtPlayGame;

    public void Init()
    {
    }

    public void OpenPopupWarning()
    {
        popupWarning.SetActive(true);
    }

    public void OpenPopupWarningBoss()
    {
        popupWarningBoss.SetActive(true);
    }

    public void DisableAndActiveTxtPlay(bool isActive)
    {
        if (!isActive)
        {
            txtPlayGame.gameObject.SetActive(false);
        }
        else
        {
            txtPlayGame.gameObject.SetActive(true);
        }
    }
}
