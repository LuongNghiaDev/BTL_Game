using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameAction : ParentFSMAction
{
    public InGameAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("OnEnter Ingame");
        base.OnEnter();
        //GameManager.Instance.UiManager.OpenUiGamePlay();

        //SoundManager.Instance.playSound(SoundManager.Instance.soundData.trainRunning);
        Time.timeScale = 1f;
        //LevelManager.Instance.StartLevel();
    }

    public override void OnExit()
    {
        base.OnExit();
        //SoundManager.Instance.StopSound(SoundManager.GameSound.BGM);
    }
}