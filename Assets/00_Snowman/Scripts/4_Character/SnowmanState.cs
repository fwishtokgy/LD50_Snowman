using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanState : MonoBehaviour
{
    protected float MeltPercentile;
    protected int Score;



    public void ApplyHeat(int heatUnits)
    {

    }
    public void RefreshCold(int coldUnits)
    {
        ApplyHeat(-coldUnits);
    }


}
