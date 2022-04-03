using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanState : MainPlayMonoBehaviour
{
    [SerializeField]
    protected ScoreWatcher Score;

    [SerializeField]
    protected MeltWatcher Melt;

    public delegate void SnowmanEvent();
    public SnowmanEvent OnDeath;

    protected override void OnInit()
    {
        base.OnInit();
        Melt.OnMeltPercentChanged += OnMelt;
    }

    // maybe give snowman shades if good score, crown if really good score?
    protected void OnMelt(float meltPercent)
    {
        if (meltPercent >= 1f)
        {
            OnDeath?.Invoke();
        }
    }
    
}
