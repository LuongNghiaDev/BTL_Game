using Common.FSM;
using UnityEngine;

public class LobbyAction : ParentFSMAction
{
    public LobbyAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
    {
    }

    public override void OnEnter()
    {

        Debug.Log("OnEnter Lobby");

        base.OnEnter();

        //GameManager.UiManager.UiMainLobby.Show(true);
        //SoundManager.Instance.PlayFxSound(soundEnum: SoundManager.GameSound.Lobby);
    }

    public override void OnExit()
    {
        base.OnExit();
        //GameManager.UiManager.UiMainLobby.Show(false);
     //   SoundManager.Instance.StopSound(SoundManager.GameSound.Lobby);
    }


}