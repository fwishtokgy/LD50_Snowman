using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MainPlayMonoBehaviour
{
    [SerializeField]
    protected TileNodeMaster TileMaster;

    [SerializeField]
    protected MasterLevelStore LevelDataStorage;

    protected Dictionary<Season, LevelData> LevelData;

    protected Season currentSeason;

    public LevelData CurrentSeason
    {
        get
        {
            if (LevelData != null && LevelData.ContainsKey(currentSeason))
            {
                return LevelData[currentSeason];
            }
            return null;
        }
    }

    [SerializeField]
    protected GameNodeTick GameTick;

    protected int DaysPassedInSeason;

    public delegate void SeasonChangeEvent(LevelData SeasonData);
    public SeasonChangeEvent OnNewSeason;

    protected override void OnInit()
    {
        TileMaster.OnStartedActivatingNode += OnNodePassed;
        GameTick.OnGeneratedTicksIncrement += CheckSeason;

        LevelData = new Dictionary<Season, LevelData>();
        foreach (var leveldata in LevelDataStorage.Levels)
        {
            LevelData.Add(leveldata.LevelSeason, leveldata);
        }
        currentSeason = LevelDataStorage.StartingSeason;
        OnNewSeason?.Invoke(LevelData[currentSeason]);
        IsInitialized = true;
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        Reset();
    }

    public void Reset()
    {
        currentSeason = LevelDataStorage.StartingSeason;
        OnNewSeason?.Invoke(LevelData[currentSeason]);
    }

    protected void OnNodePassed(TileNode node)
    {
        if (IsRunning && IsInitialized)
        {
            var gridmap = node.GridMap;
            var data = LevelData[currentSeason];

            node.TileRenderer.material = data.Environment.NormalMaterial;
        }
    }

    protected void CheckSeason(int currentTick)
    {
        DaysPassedInSeason++;
        if (DaysPassedInSeason > LevelData[currentSeason].LengthInTicks)
        {
            DaysPassedInSeason = 0;
            currentSeason = LevelData[currentSeason].NextSeason;
            OnNewSeason?.Invoke(LevelData[currentSeason]);
        }
    }
}
