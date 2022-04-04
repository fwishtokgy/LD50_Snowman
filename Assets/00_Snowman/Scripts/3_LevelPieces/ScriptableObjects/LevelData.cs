using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelInfo/LevelData", order = 3)]
public class LevelData : ScriptableObject
{
    public Season LevelSeason;
    public Season NextSeason;
    public List<DropItem> Drops;
    public List<PropItem> Props;
    public EnviroSettings Environment;
    public int LengthInTicks;
    public Color PrimaryColor;
    public float RiseRate;

    public int ChanceOfNoDrop;
    public int PropNoDensity;

    public RNGProfile ItemRNG;
    public RNGProfile BackPropRNG;
    public RNGProfile MidBackRNG;
    public RNGProfile MidFrontRNG;
    public RNGProfile FrontPropRNG;
}
