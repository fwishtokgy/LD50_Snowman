using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelInfo/MasterLevelStore", order = 3)]
public class MasterLevelStore : ScriptableObject
{
    public List<LevelData> Levels;

    public Season StartingSeason;
}
