using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIIntro : UICanvas
{
    [SerializeField] private Button btnNext;
    [SerializeField] private UICanvas uiIntro;
    [SerializeField] private AudioClip introSound;
    [SerializeField] private Button btnBack;
    private bool isPlay = true;
    private Coroutine txtEffectCoroutine;
    public Text textComponent;
    private string fullText = "Hỡi ôi, quê hương của ta. Ta thật không thể tưởng tượng được những gì mà ngươi đã trải qua. Ta rất đau khổ khi hằng ngày phải chứng kiến cảnh người dân nơi đây phải gồng mình lao động khổ sai cho bọn Harknonnen kia. Với tất cả lòng căm phẫn cùng tình yêu quê hương, ta nguyện chiến đấu đến hơi thở cuối cùng để đánh tan quân xâm lăng và mang bình yên trở về mảnh đất yêu thương này…";
    private string currentText = "";
    private AudioSource audioSource;
    private bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        btnBack.gameObject.SetActive(false);
        btnNext.onClick.AddListener(OnClickBtnNext);
        StartCoroutine(PlaySoundIntro());
        txtEffectCoroutine = StartCoroutine(ShowTextEffect());
    }

    public void StopTextEffect()
    {
        if (txtEffectCoroutine != null)
        {
            StopCoroutine(txtEffectCoroutine);
        }
    }

    private void StopAudio() 
    {
        audioSource.Stop();
    }

    private void StopSoundAndTextEffect()
    {
        StopAudio();
        StopTextEffect();
    }

    private void OnClickBtnNext()
    {
        if (!isClicked)
        {
            isClicked = true;
            StopTextEffect();
            ShowFullText();
            return;
        }
        uiIntro.gameObject.SetActive(false);
        StopSoundAndTextEffect();
    }

    IEnumerator PlaySoundIntro()
    {
        if (!isPlay)
        {
            yield return null;
        }
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = introSound;
        audioSource.Play();

        while (audioSource.isPlaying)
        {
            yield return null;
        }
    }

    private void ShowFullText()
    {
        textComponent.text = fullText;
    }

    IEnumerator ShowTextEffect()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = currentText + fullText[i];
            textComponent.text = currentText;
            yield return new WaitForSeconds(0.1f);
        }
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
