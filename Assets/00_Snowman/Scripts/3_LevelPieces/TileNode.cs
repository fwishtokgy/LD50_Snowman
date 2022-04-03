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

    public TileNodeState State;

    public bool IsTransitioning;
}
public enum TileNodeState { DISPOSED, ACTIVE, QUEUED }