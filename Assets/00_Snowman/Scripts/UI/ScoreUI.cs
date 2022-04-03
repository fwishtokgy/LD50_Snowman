using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    protected TMP_Text RawScore;

    [SerializeField]
    protected TMP_Text NextMilestone;

    [SerializeField]
    protected ScoreWatcher Score;

    [SerializeField]
    protected RankWatcher Rank;

    private void Start()
    {
        Score.OnScoreChange += UpdateText;
        Rank.OnRankChanged += UpdateSubtext;
    }

    protected void UpdateText(int NewScore)
    {
        RawScore.text = NewScore.ToString("G3");
    }
    protected void UpdateSubtext(RankWatcher.RankData data, RankWatcher.RankData nextdata)
    {
        if (data.rank != PlayerRank.PLATINUM)
        {
            NextMilestone.text = string.Format(" /{0} TO {1}", nextdata.MinPointRequirement, nextdata.rank);
        }
        else
        {
            NextMilestone.text = "---";
        }
    }
}
