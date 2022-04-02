using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTime : MyMonoBehaviour
{
    protected bool IsRunning;
    protected void OnGameStart()
    {
        IsRunning = true;
    }
    protected void OnGameEnd()
    {
        IsRunning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStateManager.Instance.GetState(StateType.MAIN);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {

        }
    }
}
