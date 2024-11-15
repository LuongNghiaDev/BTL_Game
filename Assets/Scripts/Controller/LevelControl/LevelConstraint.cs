using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Điều kiện để nối Level với Scene Build Index
/// </summary>
[Serializable]
public class LevelConstraint
{
    [SerializeField] private int[] bonusStep = { 5 };

    [SerializeField] private List<int> startIndexes;

    private int startIndex = -1;

    public int GetStartIndex()
    {
        if (startIndex != -1) return startIndex;

        startIndex = int.MaxValue;

        foreach (int startIndex in startIndexes)
        {
            this.startIndex = Mathf.Min(this.startIndex, startIndex);
        }

        return startIndex;
    }

    public int GetStartIndex(LevelType levelType)
    {
        try
        {
            return startIndexes[(int)levelType];
        }
        catch (ArgumentOutOfRangeException)
        {
            int levelTypeCount = Enum.GetValues(typeof(LevelType)).Length;
            if (levelTypeCount > startIndexes.Count)
                throw new ArgumentOutOfRangeException(
                    $"{nameof(startIndexes)} has less values than {nameof(LevelType)}. There should be {levelTypeCount} values.");

            throw;
        }
    }

    public int GetLevelCount()
    {
        startIndex = GetStartIndex();
        return SceneManager.sceneCountInBuildSettings - startIndex + 1;
    }

    public int GetLevelCount(LevelType levelType)
    {
        int levelTypeIntValue = (int)levelType;
        if (levelTypeIntValue > startIndexes.Count)
        {
            throw new ArgumentOutOfRangeException($"{nameof(startIndexes)} lacks a value for {levelType}");
        }

        int levelStartIndex = startIndexes[levelTypeIntValue];

        if (levelStartIndex == -1)
        {
            return -1;
        }

        if (levelTypeIntValue + 1 >= startIndexes.Count)
        {
            return SceneManager.sceneCountInBuildSettings - levelStartIndex;
        }

        return startIndexes[levelTypeIntValue] - levelStartIndex;
    }

    /// <summary>
    /// get Level count trong Resource
    /// </summary>
    /// <returns></returns>
    public int GetLevelCountResources()
    {
        startIndex = GetStartIndex();
        return CountResourceObjects(Helper.PathResources) - startIndex + 1;
    }

    public int CountResourceObjects(string path)
    {
        int count = 18;

        /*string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
        count = guids.Length;*/

        return count;
    }

    /// <summary>
    /// get Level count theo type trong Resource
    /// </summary>
    /// <returns></returns>
    public int GetLevelCountResources(LevelType levelType)
    {
        int levelTypeIntValue = (int)levelType;
        if (levelTypeIntValue > startIndexes.Count)
        {
            throw new ArgumentOutOfRangeException($"{nameof(startIndexes)} lacks a value for {levelType}");
        }

        int levelStartIndex = startIndexes[levelTypeIntValue];

        if (levelStartIndex == -1)
        {
            return -1;
        }

        if (levelTypeIntValue + 1 >= startIndexes.Count)
        {
            return CountResourceObjects("Assets/Resources/Level") - levelStartIndex;
        }

        return startIndexes[levelTypeIntValue] - levelStartIndex;
    }

    public LevelType GetLevelTypeFromBuildIndex(int buildIndex)
    {
        return (LevelType)(buildIndex / GetStartIndex());
    }

    public int GetLevelIndexFromBuildIndex(int buildIndex)
    {
        LevelType levelType = GetLevelTypeFromBuildIndex(buildIndex);
        int startIndex = GetStartIndex(levelType);
        return buildIndex - startIndex;
    }
}