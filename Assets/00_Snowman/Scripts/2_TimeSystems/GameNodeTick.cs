using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNodeTick : MainPlayMonoBehaviour
{
    [SerializeField]
    protected TileNodeMaster TileMaster;

    public int NodeTicksGenerated { get; protected set; }
    public int NodeTicksPassed { get; protected set; }

    public delegate void GameNodeTickEvent(int value);
    public GameNodeTickEvent OnPassedTicksIncrement;
    public GameNodeTickEvent OnGeneratedTicksIncrement;

    protected override void OnInit()
    {
        TileMaster.OnQueuedNode += OnNewNode;
        TileMaster.OnDroppedNode += OnNodePassed;
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        NodeTicksGenerated = 0;
        NodeTicksPassed = 0;
    }

    protected void OnNewNode(TileNode node)
    {
        if (IsRunning)
        {
            NodeTicksGenerated++;
            OnGeneratedTicksIncrement?.Invoke(NodeTicksGenerated);
        }
    }
    protected void OnNodePassed(TileNode node)
    {
        if (IsRunning)
        {
            NodeTicksPassed++;
            OnPassedTicksIncrement?.Invoke(NodeTicksPassed);
        }
    }
}
