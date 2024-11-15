using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Key và Analytic strings để hết ở đây
/// </summary>
public static class Helper
{
    public const string DataLevel = "DataLevel";
    public const string DataMaxLevelReached = "DataMaxLevelReached";
    public const string GOLD = "GOLD";

    public const string KEY = "KEY";
    public const string CurrentRewardEndGame = "CurrentRewardEndGame";
    public const string ProcessReceiveEndGame = "ProcessReceiveEndGame";

    public const string inter_end_game_lose = "inter_end_game_lose";
    public const string inter_end_game_win = "inter_end_game_win";


    public const string SoundSetting = "SoundSetting";
    public const string MusicSetting = "MusicSetting";

    public const string PathResources = "Assets/Resources/LevelGame";
    public const string PathPlayerResources = "Player/Player";
    public const string PathLevelResources = "LevelGame/Wave";
    public const string PathBGResources = "BG/BG";

    public static string FormatTime(int minute, int second, bool isSpaceSpecial = false)
    {
        StringBuilder sb = new StringBuilder();
        if (minute < 10)
        {
            sb.Append("0");
        }

        sb.Append(minute);
        if (isSpaceSpecial)
        {
            sb.Append("M");
            sb.Append(" ");
        }
        else
        {
            sb.Append(":");
        }

        if (second < 10)
        {
            sb.Append("0");
        }

        sb.Append(second);
        if (isSpaceSpecial)
        {
            sb.Append("S");
        }

        return sb.ToString();
    }

    public static int GetRandomGoldReward()
    {
        return Random.Range(100, 250);
    }

    public static bool CheckNewDay(string stringTimeCheck, bool isUnbiasedTime)
    {
        if (string.IsNullOrEmpty(stringTimeCheck))
        {
            return true;
        }

        try
        {
            DateTime timeNow = DateTime.Now;
            DateTime timeOld = DateTime.Parse(stringTimeCheck);
            DateTime timeOldCheck = new DateTime(timeOld.Year, timeOld.Month, timeOld.Day, 0, 0, 0);
            long tickTimeNow = timeNow.Ticks;
            long tickTimeOld = timeOldCheck.Ticks;

            long elapsedTicks = tickTimeNow - tickTimeOld;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            double totalDay = elapsedSpan.TotalDays;

            if (totalDay >= 1)
            {
                return true;
            }
        }
        catch
        {
            return true;
        }

        return false;
    }

    public static Vector3 WithAxis(this Vector3 vector3, Axis axis, float value)
    {
        return new Vector3(
            axis == Axis.X ? value : vector3.x,
            axis == Axis.Y ? value : vector3.y,
            axis == Axis.Z ? value : vector3.z
            );
    }
}

