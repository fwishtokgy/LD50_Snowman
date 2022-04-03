using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "LevelInfo/EnviroSettings", order = 2)]
public class EnviroSettings : ScriptableObject
{
    public float sunSize;
    public Color TopSky;
    public Color MidSky;    // also affects fog
    public Color BotSky;
    public Color Lighting;

    public Material NormalMaterial;
    public Material LastTileMaterial;
}
