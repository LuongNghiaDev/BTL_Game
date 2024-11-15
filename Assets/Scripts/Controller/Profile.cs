using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quản lý resourses trong game như Gold hay Key
/// </summary>
public class Profile 
{

    public void AddGold(int goldBonus/*, string _analytic*/)
    {
        var playerdata = GameManager.Instance.PlayerDataManager;
        int _count = GetGold() + goldBonus;
        PlayerDataManager.Instance.SetGold(_count);

        /*if (playerdata.actionUITop != null)
        {
            playerdata.actionUITop(TypeItem.Coin);
        }*/
    }

    public void DeductGold(int goldBonus)
    {
        int _count = 0;
        if (GetGold() >= 0)
        {
            _count = GetGold() - goldBonus;
        }
        PlayerDataManager.Instance.SetGold(_count);
    }

    public int GetGold()
    {
        return PlayerDataManager.Instance.GetGold();
    }

    public void AddKey(int amount, string _analytic)
    {
        var playerdata = GameManager.Instance.PlayerDataManager;

        PlayerDataManager.Instance.SetKey(GetKey() + amount);

        if (playerdata.actionUITop != null && amount == 1)
        {
            playerdata.actionUITop(TypeItem.Key);
        }
    }

    public int GetKey()
    {
        return PlayerDataManager.Instance.GetKey();
    }
}
