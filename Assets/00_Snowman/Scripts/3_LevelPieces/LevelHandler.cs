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

    [SerializeField]
    protected SnowmanState Snowman;

    public List<LevelData> Levels { get { return LevelDataStorage.Levels; } }

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
        Snowman.OnDeath += ClearNodes;

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
        DaysPassedInSeason = 0;
        currentSeason = LevelDataStorage.StartingSeason;
        OnNewSeason?.Invoke(LevelData[currentSeason]);
    }

    protected void ClearNodes()
    {
        TileMaster.ClearAllNodes();
    }

    protected void OnNodePassed(TileNode node)
    {
        if (IsRunning && IsInitialized)
        {
            var data = LevelData[currentSeason];

            StartCoroutine(SetupNode(node, data));

            node.TileRenderer.material = data.Environment.NormalMaterial;
        }
    }

    IEnumerator SetupNode(TileNode node, LevelData data)
    {
        print("SetupNode for " + data.LevelSeason);
        var gridmap = node.GridMap;
        var pointsPerFrame = node.GridMap.Rows;
        var count = 0;
        print("-> points " + gridmap.GridPoints.Count);
        foreach (var point in gridmap.GridPoints)
        {
            count++;
            GameObject possibleItem = null;

            print("point " + point.itemType + " " + point.Placement);

            if (point.itemType == ItemType.DROP)
            {
                possibleItem = data.ItemRNG.RetrieveRandomItem();
            }
            else
            {
                if (point.Placement == PropPlacement.BACKGROUND)
                {
                    possibleItem = data.BackPropRNG.RetrieveRandomItem();
                }
                else if (point.Placement == PropPlacement.MIDBACK)
                {
                    possibleItem = data.MidBackRNG.RetrieveRandomItem();
                }
                else if (point.Placement == PropPlacement.MIDFRONT)
                {
                    possibleItem = data.MidFrontRNG.RetrieveRandomItem();
                }
                else if (point.Placement == PropPlacement.FRONT)
                {
                    possibleItem = data.FrontPropRNG.RetrieveRandomItem();
                }
            }

            if (possibleItem != null)
            {
                print("Adding new item " + possibleItem.name);
                var newItem = Instantiate(possibleItem, node.transform);
                node.AddItem(newItem);
                newItem.transform.localPosition = point.Position;
            }

            if (count > pointsPerFrame)
            {
                count = 0;
                yield return new WaitForEndOfFrame();
            }
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
