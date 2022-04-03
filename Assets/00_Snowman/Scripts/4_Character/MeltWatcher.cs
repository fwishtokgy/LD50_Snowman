using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltWatcher : MainPlayMonoBehaviour
{
    [SerializeField]
    protected List<PercentileStage> Stages;

    [System.Serializable]
    public class PercentileStage
    {
        public float StartPercent;
        public Color color;
        public string StateLabel;
    }

    protected int currentStage;

    public delegate void MeltStage(PercentileStage stageData);
    public MeltStage OnNewMeltStage;

    [SerializeField]
    protected AmbientTemp Temperature;

    protected float Melt;

    [SerializeField]
    protected float FullMeltValue;

    public delegate void MeltEvent(float percentile);
    public MeltEvent OnMeltPercentChanged;

    protected override void OnInit()
    {
        base.OnInit();
        Temperature.OnTemperatureChange += ApplyAmbient;
        Reset();
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        Reset();
    }

    public void Reset()
    {
        Melt = 0f;
        currentStage = 0;
        OnNewMeltStage?.Invoke(Stages[0]);
    }

    protected void ApplyAmbient(float heat)
    {
        Apply(heat);
    }

    public void ApplyHeat(int heatUnits)
    {
        Apply(heatUnits);
    }
    public void RefreshCold(int coldUnits)
    {
        ApplyHeat(-coldUnits);
    }
    protected void Apply(float units)
    {
        Melt += units;
        if (Melt < 0) Melt = 0f;
        if (Melt > FullMeltValue) Melt = FullMeltValue;
        var percentile = Melt / FullMeltValue;
        OnMeltPercentChanged?.Invoke(percentile);
        CheckStage(percentile);
    }
    protected void CheckStage(float percentile)
    {
        if (IsRunning)
        {
            if (currentStage > 0 &&
                percentile < Stages[currentStage - 1].StartPercent)
            {
                Demote();
            }
            else if (percentile >= Stages[currentStage].StartPercent)
            {
                Promote();
            }
        }
    }
    protected void Demote()
    {
        currentStage += -1;
        if (currentStage < 0) currentStage = 0;
        OnNewMeltStage?.Invoke(Stages[currentStage]);
    }
    protected void Promote()
    {
        OnNewMeltStage?.Invoke(Stages[currentStage]);
        currentStage++;
        var isMaxRank = currentStage >= Stages.Count;
        if (isMaxRank) currentStage = Stages.Count - 1;
    }
}
