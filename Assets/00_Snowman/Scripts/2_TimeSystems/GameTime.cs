using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MainPlayMonoBehaviour
{
    protected float RLSecondsCount;
    public float TotalRaw { get { return RLSecondsCount; } }

    protected float microseconds;
    protected int rawseconds, seconds, minutes, hours, leftovers;

    public float MicroSeconds { get { return microseconds; } }
    public int Seconds { get { return seconds; } }
    public int Minutes { get { return minutes; } }
    public int Hours { get { return hours; } }

    protected void Reset()
    {
        RLSecondsCount = 0f;
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        Reset();
    }

    protected override void OnStateEnd()
    {
        base.OnStateEnd();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            RLSecondsCount += Time.deltaTime;
            rawseconds = Mathf.FloorToInt(RLSecondsCount);
            microseconds = RLSecondsCount - rawseconds;

            hours = (rawseconds - (rawseconds % 3600)) / 3600;
            leftovers = rawseconds % 3600;
            minutes = (leftovers - (leftovers % 60)) / 60;
            leftovers = leftovers % 60;
            seconds = leftovers;
        }
    }
}
