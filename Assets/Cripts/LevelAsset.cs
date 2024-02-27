using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelAsset", order = 1)]
public class LevelAsset : ScriptableObject
{
    public List<LevelConfig> levelConfigs = new();
}
