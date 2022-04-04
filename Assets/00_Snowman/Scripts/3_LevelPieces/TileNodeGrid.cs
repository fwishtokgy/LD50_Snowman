using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNodeGrid : MonoBehaviour
{
    [SerializeField]
    protected int columns;
    public int Columns { get { return columns; } }

    protected const int rows = 7;

    protected const float RestingY = .25f;
    public int Rows { get { return rows; } }

    public float DropFactor;

    protected List<Node> gridPoints;
    public List<Node> GridPoints { get { return gridPoints; } }

    public bool IsInitialized { get; protected set; }

    public void Initialize(float width, float depth)
    {
        print("initializing grid");
        gridPoints = new List<Node>();

        var xunits = width/(float)columns;
        var startX = (-width / 2f) + (xunits/2f);

        var zunits = depth/rows;
        var startZ = depth/2f;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                var node = new Node();
                var z = startZ - (i * zunits);
                var x = (j * xunits) + startX;
                var Type = (i % 2 == 1) ? ItemType.DROP : ItemType.PROP;
                node.itemType = Type;

                if (Type == ItemType.PROP)
                {
                    var propindex = (PropPlacement)((i / 2) % 4);
                    node.Placement = propindex;
                }

                node.Position = new Vector3(x, RestingY, z);
                print("--- " + node.Position.ToString());
                gridPoints.Add(node);
            }
        }

        IsInitialized = true;
    }
    public class Node
    {
        public ItemType itemType;
        public PropPlacement Placement;
        public Vector3 Position;
    }
}
