using Common.FSM;
using UnityEngine;
using System.Collections;

public class EndgameAction : ParentFSMAction
{
    public EndgameAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("OnEnter Endgame");
        //SoundManager.Instance.StopFootStep();

        ProcessWinLose();

    }

    private void ProcessWinLose()
    {
        //Debug.Log("CurrentLevel_" + LevelManager.Instance.Result);

        switch (LevelManager.Instance.Result)
        {
            case LevelResult.Win:
                //GameManager.UiManager.OpenUiWin(Constants.GOLD_WIN);
                break;
            case LevelResult.Lose:
                Time.timeScale = 0f;
                //GameManager.UiManager.OpenUiLose();
                //SoundManager.Instance.playSound(SoundManager.Instance.soundData.lose);
                break;
            default:
                break;
        }

        //GameManager.Instance.StartCoroutine(IEShowInter());
    }

    private IEnumerator IEShowInter()
    {
        yield return new WaitForSeconds(0.4f);

        /*switch (GameManager.LevelManager.Result)
        {
            case LevelResult.Win:
                UnicornAdManager.ShowInterstitial(Helper.inter_end_game_win);
                break;
            case LevelResult.Lose:
                UnicornAdManager.ShowInterstitial(Helper.inter_end_game_lose);
                break;
            default:
                break;
        }*/
    }

    public override void OnExit()
    {
        base.OnExit();
        /*SoundManager.Instance.StopSound(GameManager.LevelManager.Result);
        PrefabStorage.Instance.fxWinPrefab.SetActive(false);*/
    }
}
