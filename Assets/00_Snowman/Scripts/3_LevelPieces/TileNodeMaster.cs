using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNodeMaster : MyMonoBehaviour
{
    [SerializeField]
    protected GameObject TileNodePrefab;

    [SerializeField]
    protected Transform LeftBoundaryPoint;
    [SerializeField]
    protected Transform RightBoundaryPoint;

    [SerializeField]
    protected Transform BeltNodesRoot;

    [SerializeField]
    protected float BeltSpeed;
    [SerializeField]
    protected float MaxSpeed;

    protected int VisibleNodeCount;
    protected int LeftBufferCount;
    protected int RightBufferCount;
    protected List<TileNode> allNodes;
    protected Queue<TileNode> activeNodes;

    [SerializeField]
    protected float GenerationHeight;
    [SerializeField]
    protected float DisposalHeight;

    protected float RestingHeight;

    protected float RelativeZero;
    protected float DropToActivePoint;
    protected float DropToDiscardPoint;
    protected float RelativeMax;
    protected float RelativeBeltLength;

    protected float LeftMostActivePoint, RightMostActivePoint;

    protected const float Gravity = 10f;

    [SerializeField]
    protected Transform TestNodeRoot;

    private void Start()
    {
        StartCoroutine(GenerateBelt());
    }

    IEnumerator GenerateBelt()
    {
        var fulldistance = Vector3.Distance(LeftBoundaryPoint.position, RightBoundaryPoint.position);

        // Generate a testnode so we can see the dimensions of each tile
        var testnode = GameObject.Instantiate(TileNodePrefab, TestNodeRoot).GetComponent<TileNode>();

        // figure out how many nodes we wanna make
        var distanceInNodes = fulldistance / testnode.Width;
        var VisibleNodeCount = Mathf.FloorToInt(distanceInNodes);
        if (distanceInNodes % 1 > .5f)
        {
            VisibleNodeCount = Mathf.CeilToInt(distanceInNodes);
        }

        // figure out the starting point
        var nodesback = (VisibleNodeCount - ((VisibleNodeCount % 2 == 0) ? 0 : 1)) / 2;
        var midpoint = Vector3.Lerp(LeftBoundaryPoint.position, RightBoundaryPoint.position, .5f);
        LeftMostActivePoint = midpoint.x - (nodesback * testnode.Width);
        RightMostActivePoint = midpoint.x + (nodesback * testnode.Width);
        RestingHeight = midpoint.y;

        var buffer = 2;
        if (MaxSpeed > testnode.Width)
        {
            buffer = Mathf.CeilToInt(MaxSpeed / testnode.Width);
        }
        RelativeZero = RightMostActivePoint + (buffer * testnode.Width);
        var dropTime = (GenerationHeight - RestingHeight) / Gravity;
        DropToActivePoint = RightMostActivePoint + testnode.Width + (BeltSpeed * dropTime);
        DropToDiscardPoint = LeftMostActivePoint - testnode.Width;
        RelativeMax = DropToDiscardPoint - (buffer * testnode.Width);

        var realCount = (2 * buffer) + VisibleNodeCount;
        RelativeBeltLength = realCount * testnode.Width;

        allNodes = new List<TileNode>();

        var dropnode = new Queue<TileNode>();

        for (int nindex = 0; nindex < realCount; nindex++)
        {
            var newnode = GameObject.Instantiate(TileNodePrefab, BeltNodesRoot).GetComponent<TileNode>();
            newnode.RelativeX = nindex * testnode.Width;
            var x = RelativeZero - newnode.RelativeX;

            if (nindex < buffer)
            {
                newnode.transform.position = new Vector3(RightMostActivePoint + newnode.Width, GenerationHeight, midpoint.z);
                newnode.State = TileNodeState.QUEUED;
            }
            else if (nindex < (VisibleNodeCount + buffer))
            {
                newnode.transform.position = new Vector3(x, GenerationHeight, midpoint.z);
                newnode.State = TileNodeState.ACTIVE;
                StartCoroutine(FancyNodeDrop(newnode));
                dropnode.Enqueue(newnode);
            }
            else
            {
                newnode.transform.position = new Vector3(x, DisposalHeight, midpoint.z);
                newnode.State = TileNodeState.DISPOSED;
            }

            allNodes.Add(newnode);
            
            yield return new WaitForEndOfFrame();
        }
        while (dropnode.Count > 0)
        {
            if (!dropnode.Peek().IsTransitioning)
            {
                dropnode.Dequeue();
            }
            yield return new WaitForEndOfFrame();
        }

        var firstQueuedNode = allNodes[buffer - 1];
        if (firstQueuedNode.transform.position.x < DropToActivePoint)
        {
            var distanceToRecover = DropToActivePoint - firstQueuedNode.transform.position.x;
            var timediff = distanceToRecover * BeltSpeed;
            firstQueuedNode.State = TileNodeState.ACTIVE;
            StartCoroutine(FancyNodeDrop(firstQueuedNode));
            yield return new WaitForSeconds(timediff);
        }

        IsInitialized = true;
    }
    IEnumerator FancyNodeDrop(TileNode node, float lostTime = 0f)
    {
        node.IsTransitioning = true;
        var startpoint = node.transform.position;
        var endpoint = new Vector3(startpoint.x, RestingHeight, startpoint.z);
        var duration = (GenerationHeight - RestingHeight) / Gravity;
        var time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            var factor = Easings.BounceEaseOut(time / duration);
            node.transform.position = Vector3.Lerp(startpoint, endpoint, factor);
            yield return new WaitForEndOfFrame();
        }
        node.transform.position = endpoint;
        node.IsTransitioning = false;
    }

    IEnumerator FancyNodeDisposal(TileNode node, float lostTime = 0f)
    {
        node.IsTransitioning = true;
        var startpoint = node.transform.position;
        var endpoint = new Vector3(startpoint.x, DisposalHeight, startpoint.z);
        var duration = (RestingHeight - DisposalHeight) / Gravity;
        var time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            var factor = Easings.ExponentialEaseOut(time / duration);
            node.transform.position = Vector3.Lerp(startpoint, endpoint, factor);
            yield return new WaitForEndOfFrame();
        }
        node.transform.position = endpoint;
        node.IsTransitioning = false;
    }

    private void Update()
    {
        if (IsInitialized)
        {
            foreach (var node in allNodes)
            {
                MoveNode(node);
            }
        }
    }

    protected void MoveNode(TileNode node)
    {
        var rawX = node.RelativeX + (BeltSpeed * Time.deltaTime);
        var relativeX = RelativeZero - rawX;

        if (node.State == TileNodeState.DISPOSED &&
            rawX > RelativeBeltLength &&
            !node.IsTransitioning)
        { // Change node from Discarded to Queued
            node.State = TileNodeState.QUEUED;
            rawX = rawX % RelativeBeltLength;
            node.transform.position = new Vector3(RightMostActivePoint + node.Width, GenerationHeight, RightBoundaryPoint.position.z);
        }
        else if (node.State == TileNodeState.ACTIVE &&
            relativeX < DropToDiscardPoint)
        { // Change Node from Active to Discarded
            node.State = TileNodeState.DISPOSED;
            float lostTime = 0f;
            StartCoroutine(FancyNodeDisposal(node, lostTime));
        }
        else if (node.State == TileNodeState.QUEUED &&
            relativeX < DropToActivePoint &&
            !node.IsTransitioning)
        { // Change Node from Queued to Active
            node.State = TileNodeState.ACTIVE;
            float lostTime = 0f;
            StartCoroutine(FancyNodeDrop(node, lostTime));
        }
        node.RelativeX = rawX;
        if (node.State == TileNodeState.ACTIVE && !node.IsTransitioning)
        {
            node.transform.position = new Vector3(relativeX, node.transform.position.y, node.transform.position.z);
        }
    }
}
