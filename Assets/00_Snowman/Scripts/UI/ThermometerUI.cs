using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThermometerUI : MainPlayMonoBehaviour
{
    [SerializeField]
    protected List<Renderer> renderers;

    [SerializeField]
    protected Transform Filling;

    [SerializeField]
    protected TMP_Text TemperatureLabel;

    [SerializeField]
    protected TMP_Text StatusLabel;

    [SerializeField]
    protected MeltWatcher Melt;

    private void Awake()
    {
        Melt.OnMeltPercentChanged += OnMelt;
        Melt.OnNewMeltStage += OnNewMeltStage;
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
    }

    protected void OnMelt(float percentile)
    {
        Filling.localScale = new Vector3(percentile, 1, 1);
        var percent = Mathf.CeilToInt(percentile * 100);
        TemperatureLabel.text = string.Format("{0}° Celsius", percent);
    }
    protected void OnNewMeltStage(MeltWatcher.PercentileStage stageData)
    {
        StatusLabel.text = stageData.StateLabel;
        foreach (var renderer in renderers)
        {
            renderer.material.color = stageData.color;
        }
    }
}
