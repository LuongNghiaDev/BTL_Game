using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelInfo
{
    LevelType LevelType { get; }
    int DisplayLevel { get; }
    int GetCurrentLevel();
}
/// <summary>
/// Thông tin về level
/// </summary>
