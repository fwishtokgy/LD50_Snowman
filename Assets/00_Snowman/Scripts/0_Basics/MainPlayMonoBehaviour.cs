using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainPlayMonoBehaviour : MyStateMonoBehaviour
{
    protected GameState mainGame;

    protected bool IsRunning;
    protected override void OnStateStart()
    {
        IsRunning = true;
    }
    protected override void OnStateEnd()
    {
        IsRunning = false;
    }
    void Start()
    {
        StartCoroutine(WaitForStateManager(StateType.MAIN));
        OnInit();
    }

    protected virtual void OnInit() { }
}
