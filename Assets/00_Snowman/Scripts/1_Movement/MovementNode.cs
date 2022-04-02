using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementNode : MyMonoBehaviour
{
    public int Index { get; protected set; }
    public MovementNodes Manager { get; protected set; }
    public void Init(int index, MovementNodes manager)
    {
        Index = index;
        Manager = manager;
        IsInitialized = true;
    }
}
