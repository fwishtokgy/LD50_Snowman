using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPointPerPassedTile : MainPlayMonoBehaviour
{
    [SerializeField]
    protected ScoreWatcher Score;

    [SerializeField]
    protected GameNodeTick GameTick;

    [SerializeField]
    protected int PointsPerTile;

    // Start is called before the first frame update
    protected override void OnInit()
    {
        GameTick.OnPassedTicksIncrement += IncrementScore;
    }
    protected void IncrementScore(int passedTicks)
    {
        Score.Increment(PointsPerTile);
    }
}
