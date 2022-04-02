using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MainPlayMonoBehaviour
{
    [SerializeField]
    protected GameTime gameTime;

    [SerializeField]
    protected TMP_Text timeText;

    protected const string format = "{0:D2}:{1:D2}:{2:D2}.{3:G3}";

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            timeText.text = ParsedTime();
        }
    }

    protected string ParsedTime()
    {
        return string.Format(format, gameTime.Hours, gameTime.Minutes, gameTime.Seconds, (int)(gameTime.MicroSeconds * 1000));
    }
}
