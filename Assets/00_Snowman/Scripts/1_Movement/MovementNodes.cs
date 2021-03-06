using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNodes : MyMonoBehaviour
{
    protected List<MovementNode> nodes;

    protected int nodePointer;

    void Start()
    {
        nodes = new List<MovementNode>();
        var index = 0;
        // nodes should be from farthest to nearest
        foreach (var node in this.GetComponentsInChildren<MovementNode>())
        {
            nodes.Add(node);
            node.Init(index, this);
            index++;
        }

        nodePointer = (nodes.Count + ((nodes.Count % 2 == 0) ? 0 : -1)) / 2;

        IsInitialized = true;
    }

    public MovementNode MoveToNextNode()
    {
        nodePointer++;
        nodePointer = StaticUtilities.CapInt(nodePointer, 0, nodes.Count - 1);
        return GetCurrentNode();
    }
    public MovementNode MoveToPrevNode()
    {
        nodePointer = nodePointer - 1;
        nodePointer = StaticUtilities.CapInt(nodePointer, 0, nodes.Count - 1);
        return GetCurrentNode();
    }
    public MovementNode GetCurrentNode()
    {
        return nodes[nodePointer];
    }
    
}
