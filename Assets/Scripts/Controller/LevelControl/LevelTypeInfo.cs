using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// Hỗ trợ cho <see cref="ParentDataLevel"/>, viết lại nếu cần.
/// </summary>
[Serializable]
public class LevelTypeInfo
{
    [JsonProperty] private LevelType levelType;
    [JsonProperty] private int currentLevel = 1;
    [JsonProperty] private int maxLevel = int.MinValue;

    [JsonIgnore]
    public LevelType LevelType
    {
        get => levelType;
        set => levelType = value;
    }

    [JsonIgnore]
    public int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    [JsonIgnore]
    public int MaxLevel => maxLevel;

    public LevelTypeInfo(LevelType levelType)
    {
        this.levelType = levelType;
    }

    public int IncreaseLevel(LevelConstraint levelConstraint)
    {
        var levelCount = levelConstraint.GetLevelCountResources(levelType);
        currentLevel++;
        maxLevel = Mathf.Max(maxLevel, currentLevel);

        if (maxLevel <= levelCount) return currentLevel;

        currentLevel = 1;
        maxLevel = 1;
        return currentLevel;
    }

    public void SetLevel(int level, LevelConstraint levelConstraint)
    {
        var levelCount = levelConstraint.GetLevelCountResources(levelType);
        currentLevel = Mathf.Clamp(level, 1, levelCount);
    }
}
