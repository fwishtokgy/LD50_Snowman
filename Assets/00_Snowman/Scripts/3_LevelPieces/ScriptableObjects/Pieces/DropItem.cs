using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelInfo/DropItem", order = 0)]
public class DropItem : ScriptableObject
{
    public GameObject ItemPrefab;

    public HitType hitType;

    public bool IsStoreableInBag;

    public int Value;

    public int Chance;
}
