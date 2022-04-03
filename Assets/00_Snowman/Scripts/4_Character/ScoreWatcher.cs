using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreWatcher : MainPlayMonoBehaviour
{
    protected int score;
    public int Score { get { return score; } }

    public void Increment(int amount)
    {
        if (IsRunning)
        {
            score += amount;
            OnScoreChange?.Invoke(score);
        }
    }
    public void Decrement(int amount)
    {
        Increment(-amount);
    }

    public void Reset()
    {
        score = 0;
        OnScoreChange?.Invoke(score);
    }

    protected override void OnStateStart()
    {
        base.OnStateStart();
        Reset();
    }

    public delegate void ScoreChangeEvent(int value);
    public ScoreChangeEvent OnScoreChange;
}
