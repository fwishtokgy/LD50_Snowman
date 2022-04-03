using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanState : MainPlayMonoBehaviour
{
    protected float MeltPercentile;

    [SerializeField]
    protected ScoreWatcher Score;

    // maybe give snowman shades if good score, crown if really good score?

    public void ApplyHeat(int heatUnits)
    {

    }
    public void RefreshCold(int coldUnits)
    {
        ApplyHeat(-coldUnits);
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
    }
    protected override void OnStateEnd()
    {
        base.OnStateEnd();
    }
}
