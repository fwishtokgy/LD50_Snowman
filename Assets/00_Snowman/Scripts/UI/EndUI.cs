using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndUI : MyStateMonoBehaviour
{
    [SerializeField]
    protected TMP_Text Score;

    [SerializeField]
    protected TMP_Text Rank;

    [SerializeField]
    protected TMP_Text Era;

    [SerializeField]
    protected ScoreWatcher scoreWatcher;

    [SerializeField]
    protected RankWatcher rankWatcher;

    [SerializeField]
    protected LevelHandler levelHandler;

    [SerializeField]
    protected ParticleSystem SplashParticles;

    void Start()
    {
        StartCoroutine(WaitForStateManager(StateType.END));

        Score.text = scoreWatcher.Score.ToString();
        Rank.text = rankWatcher.Rank.rank.ToString();
        Era.text = levelHandler.CurrentSeason.LevelSeason.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameStateManager.Instance.CurrentState == StateType.END)
        {
            WipeData();
            GameStateManager.Instance.ChangeState(StateType.START);
            SplashParticles.Play();
        }
    }

    protected void WipeData()
    {
        scoreWatcher.Reset();
        rankWatcher.Reset();
        levelHandler.Reset();
    }
}
