using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelInfo/PropItem", order = 1)]
public class PropItem : ScriptableObject
{
    public GameObject ItemPrefab;

    public PropPlacement Placements;

    public NumRange<float> SizeRange;

    public List<Color> ColorPool;

    public int Chance;
}
