using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankWatcher : MainPlayMonoBehaviour
{
    [SerializeField]
    protected List<RankData> PossibleRanks;

    protected int currentRank;

    [SerializeField]
    protected ScoreWatcher Score;

    public delegate void RankEvent(RankData value, RankData upcoming);
    public RankEvent OnRankChanged;

    protected override void OnInit()
    {
        Score.OnScoreChange += CheckRank;
        currentRank = (int) PlayerRank.POO;
    }
    protected void CheckRank(int currentScore)
    {
        if (IsRunning)
        {
            if (currentScore < PossibleRanks[currentRank].MinPointRequirement)
            {
                Demote();
            }
            else if (currentRank < PossibleRanks.Count - 1 &&
                currentScore > PossibleRanks[currentRank + 1].MinPointRequirement)
            {
                Promote();
            }
            else if (currentScore == 0)
            {
                currentRank = (int)PlayerRank.POO;
                OnRankChanged?.Invoke(PossibleRanks[currentRank], PossibleRanks[currentRank + 1]);
            }
        }
    }
    protected void Demote()
    {
        currentRank += -1;
        if (currentRank < 0) currentRank = 0;
        OnRankChanged?.Invoke(PossibleRanks[currentRank], PossibleRanks[currentRank + 1]);
    }
    protected void Promote()
    {
        currentRank++;
        var isMaxRank = currentRank >= PossibleRanks.Count;
        if (isMaxRank) currentRank = PossibleRanks.Count - 1;
        OnRankChanged?.Invoke(PossibleRanks[currentRank], PossibleRanks[currentRank + (isMaxRank ? 0 : 1)]);
    }

    [System.Serializable]
    public class RankData
    {
        public PlayerRank rank;
        public int MinPointRequirement;
    }
}
