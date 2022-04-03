using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNode : MyMonoBehaviour
{
    /// <summary>
    /// We use this to determine the size of each node
    /// </summary>
    [SerializeField]
    protected Transform ScaledStandardUnityCube;
    public float Width { get { return ScaledStandardUnityCube.localScale.x; } }
    public float Depth { get { return ScaledStandardUnityCube.localScale.z; } }

    public float RelativeX;

    public Season season;

    public bool isTailEnd;

    [SerializeField]
    protected Renderer tileRenderer;
    public Renderer TileRenderer { get { return tileRenderer; } }

    protected TileNodeState state;
    public TileNodeState State
    {
        get
        {
            return state;
        }
        set
        {
            if (state != value)
            {
                switch (value)
                {
                    case TileNodeState.ACTIVE:
                        OnActivated?.Invoke();
                        break;
                    case TileNodeState.DISPOSED:
                        OnDeactivated?.Invoke();
                        break;
                }
                state = value;
            }
        }
    }

    public bool IsTransitioning;

    public delegate void SingleTileStateEvent();
    public SingleTileStateEvent OnActivated;
    public SingleTileStateEvent OnDeactivated;

    [SerializeField]
    protected TileNodeGrid gridMap;
    public TileNodeGrid GridMap { get { return gridMap; } }
}
public enum TileNodeState { DISPOSED, ACTIVE, QUEUED }