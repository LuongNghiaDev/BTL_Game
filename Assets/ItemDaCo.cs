using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ItemDaCo : UICanvas
{
    [Title("Text")]
    [SerializeField] private Button btnWin;
    [SerializeField] private Button btnResetWinning;
    [SerializeField] private Button btnChuoiKiem;
    [SerializeField] private Button btnLuoiKiem;
    [SerializeField] private Button btnDCCK;
    [SerializeField] private Button btnDungCuRen;
    [SerializeField] private Image imgChuoiKiem;
    [SerializeField] private Image imgLuoiKiem;
    [SerializeField] private Image imgDCCK;
    [SerializeField] private Image imgDungCuRen;
    [SerializeField] private AudioClip endSound;
    private AudioSource audioSource;
    private bool isPlay = true;
    private int cntItem = 0;


    //[SerializeField] private UIStartLevel uIStartLevel;

    public UICanvas[] uiElements; // Mảng chứa các UI cần ẩn

    private void Start()
    {
        btnWin.onClick.AddListener(OnClickbtnWin);
        btnResetWinning.onClick.AddListener(OnClickbtnResetWinning);
        btnResetWinning.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("False Knight"))
        {
            cntItem++;
            imgDCCK.gameObject.SetActive(true);
            btnDCCK.gameObject.SetActive(true);
        }
        else 
        {
            imgDCCK.gameObject.SetActive(false);
            btnDCCK.gameObject.SetActive(false);
        }
        if (PlayerPrefs.HasKey("mapTuanNghia"))
        {
            cntItem++;
            imgLuoiKiem.gameObject.SetActive(true);
            btnLuoiKiem.gameObject.SetActive(true);
        }   
        else
        {
            imgLuoiKiem.gameObject.SetActive(false);
            btnLuoiKiem.gameObject.SetActive(false);
        }

        if (PlayerPrefs.HasKey("mapLuongNghia"))
        {
            cntItem++;
            imgChuoiKiem.gameObject.SetActive(true);
            btnChuoiKiem.gameObject.SetActive(true);
        }       
        else
        {
            imgChuoiKiem.gameObject.SetActive(false);
            btnChuoiKiem.gameObject.SetActive(false);
        } 
        if (PlayerPrefs.HasKey("mapTienManh"))
        {
            cntItem++;
            imgDungCuRen.gameObject.SetActive(true);
            btnDungCuRen.gameObject.SetActive(true);
        }
        else
        {
            imgDungCuRen.gameObject.SetActive(false);
            btnDungCuRen.gameObject.SetActive(false);
        }
        if (cntItem == 4 && !PlayerPrefs.HasKey("Winning"))
        {
            btnWin.gameObject.SetActive(true);
        }
        else
        {
            btnWin.gameObject.SetActive(false);
        }
    }

    private void OnClickbtnWin()
    {
        PlayerPrefs.SetInt("Winning", 1);
        PlayerPrefs.Save();
        btnWin.gameObject.SetActive(false);
        btnResetWinning.gameObject.SetActive(true);
        PlaySoundEnd();
    }

    private void OnClickbtnResetWinning()
    {
        PlayerPrefs.DeleteKey("Winning");
        PlayerPrefs.DeleteKey("mapLuongNghia");
        PlayerPrefs.DeleteKey("False Knight");
        PlayerPrefs.DeleteKey("mapTuanNghia");
        PlayerPrefs.DeleteKey("mapTienManh");
        PlayerPrefs.Save();
        btnResetWinning.gameObject.SetActive(false);
        imgChuoiKiem.gameObject.SetActive(false);
        imgLuoiKiem.gameObject.SetActive(false);
        imgDCCK.gameObject.SetActive(false);
        imgDungCuRen.gameObject.SetActive(false);
        btnChuoiKiem.gameObject.SetActive(false);
        btnLuoiKiem.gameObject.SetActive(false);
        btnDCCK.gameObject.SetActive(false);
        btnDungCuRen.gameObject.SetActive(false);
        audioSource.Stop();
    }

    private void PlaySoundEnd()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = endSound;
        audioSource.volume = 1.0f;
        audioSource.loop = false;
        audioSource.Play();
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
