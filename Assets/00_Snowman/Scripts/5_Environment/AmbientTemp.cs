using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientTemp : MainPlayMonoBehaviour
{
    public float RiseRate;

    public delegate void TempChangeEvent(float increment);
    public TempChangeEvent OnTemperatureChange;

    protected override void OnInit()
    {
        base.OnInit();
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        RiseRate = 0f;
    }

    private void Update()
    {
        if (IsRunning && RiseRate > 0f)
        {
            OnTemperatureChange?.Invoke(RiseRate * Time.deltaTime);
        }
    }
}
