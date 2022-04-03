using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNodeGrid : MonoBehaviour
{
    [SerializeField]
    protected int columns;
    public int Columns { get { return columns; } }

    protected const int rows = 3;
    public int Rows { get { return rows; } }

    public float DropFactor;
}
