using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWatcher : MainPlayMonoBehaviour
{
    [SerializeField]
    protected SnowmanState Snowman;

    protected override void OnInit()
    {
        Snowman.OnDeath += ProceedToGameOver;
    }

    protected void ProceedToGameOver()
    {
        GameStateManager.Instance.ChangeState(StateType.END);
    }

    
}
