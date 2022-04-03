using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermometerUI : MainPlayMonoBehaviour
{
    [SerializeField]
    protected List<Renderer> renderers;



    [System.Serializable]
    public class PercentileStages
    {
        public float StartPercent;
        public Color color;
    }

    private void Awake()
    {
        foreach (var renderer in renderers)
        {
            renderer.gameObject.SetActive(false);
        }
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        foreach (var renderer in renderers)
        {
            renderer.gameObject.SetActive(true);
        }
    }
}
