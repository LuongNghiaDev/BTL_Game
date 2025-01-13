using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{

    [SerializeField] private GameObject uiItem;
    [SerializeField] private Button btnNext;
    void Start()
    {
        btnNext.onClick.AddListener(OnClickBtnNext);
    }
    private void OnClickBtnNext()
    {
        Debug.Log("Next");
        PlayerPrefs.SetInt("DCCK", 1);
        uiItem.SetActive(false);
    }
}
