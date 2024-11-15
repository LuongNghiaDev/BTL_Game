using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lưu giữ toàn bộ data game. 
/// </summary>
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    public Action<TypeItem> actionUITop;
    //public DataRewardEndGame DataRewardEndGame;

    private IDataLevel parentDataLevel;
    private Dictionary<int, float> redLightTimes;

    private void Awake()
    {
        Instance = this;
        parentDataLevel = null;
    }

    public void SetDataLevel(IDataLevel parentDataLevel)
    {
        this.parentDataLevel = parentDataLevel;
        PlayerPrefs.SetString(Helper.DataLevel, JsonConvert.SerializeObject(parentDataLevel));
    }

    public IDataLevel GetDataLevel(LevelConstraint levelConstraint)
    {
        var dataLevelJson = PlayerPrefs.GetString(Helper.DataLevel, string.Empty);
        parentDataLevel = dataLevelJson == string.Empty
            ? new ParentDataLevel(levelConstraint)
            : JsonConvert.DeserializeObject<ParentDataLevel>(dataLevelJson);

        return parentDataLevel ?? new ParentDataLevel(levelConstraint);
    }

    public int GetMaxLevelReached()
    {
        return PlayerPrefs.GetInt(Helper.DataMaxLevelReached, 1);
    }

    public int GetGold()
    {
        return PlayerPrefs.GetInt(Helper.GOLD, 0);
    }

    public void SetGold(int _count)
    {
        PlayerPrefs.SetInt(Helper.GOLD, _count);
    }

    public int GetKey()
    {
        return PlayerPrefs.GetInt(Helper.KEY, 0);
    }

    public void SetKey(int _count)
    {
        PlayerPrefs.SetInt(Helper.KEY, _count);
    }

    public int GetCurrentIndexRewardEndGame()
    {
        return PlayerPrefs.GetInt(Helper.CurrentRewardEndGame, 0);
    }

    public void SetCurrentIndexRewardEndGame(int index)
    {
        PlayerPrefs.SetInt(Helper.CurrentRewardEndGame, index);
    }

    public int GetProcessReceiveRewardEndGame()
    {
        return PlayerPrefs.GetInt(Helper.ProcessReceiveEndGame, 0);
    }

    public void SetProcessReceiveRewardEndGame(int number)
    {
        PlayerPrefs.SetInt(Helper.ProcessReceiveEndGame, number);
    }

    public bool GetFreeSpin()
    {
        return PlayerPrefs.GetInt("FreeSpin", 1) > 0 ? true : false;
    }

    public void SetFreeSpin(bool isFree)
    {
        int free = isFree ? 1 : 0;
        PlayerPrefs.SetInt("FreeSpin", free);
    }

    public void SetSoundSetting(bool isOn)
    {
        PlayerPrefs.SetInt(Helper.SoundSetting, isOn ? 1 : 0);
    }

    public bool GetSoundSetting()
    {
        return PlayerPrefs.GetInt(Helper.SoundSetting, 1) == 1;
    }

    public void SetMusicSetting(bool isOn)
    {
        PlayerPrefs.SetInt(Helper.MusicSetting, isOn ? 1 : 0);
    }

    public bool GetMusicSetting()
    {
        return PlayerPrefs.GetInt(Helper.MusicSetting, 1) == 1;

    }

    public bool IsNoAds()
    {
        return PlayerPrefs.GetInt("NoAds", 0) == 1;
    }

    public void SetNoAds()
    {
        PlayerPrefs.SetInt("NoAds", 1);
    }

    public void SetNumberPlay(int num)
    {
        PlayerPrefs.SetInt("NumberPlay", num);
    }

    public int GetNumberPlay()
    {
        return PlayerPrefs.GetInt("NumberPlay", 0);
    }

    public string GetTimeLoginOpenGift()
    {
        return PlayerPrefs.GetString("TimeLoginOpenGift", "");
    }

    public void SetTimeLoginOpenGift(string time)
    {
        PlayerPrefs.SetString("TimeLoginOpenGift", time);
    }
}
