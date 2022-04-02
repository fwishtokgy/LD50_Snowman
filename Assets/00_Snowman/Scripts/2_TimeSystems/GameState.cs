using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MyMonoBehaviour
{
    public int Index { get; protected set; }

    [SerializeField]
    protected StateType statetype;
    public StateType Type { get { return statetype; } }

    protected GameStateManager Manager;

    public void Initialize(int index, GameStateManager manager)
    {
        Index = index;
        Manager = manager;
        IsInitialized = true;
    }

    [SerializeField]
    public GameStateManager.GameStateEvent OnStart = new GameStateManager.GameStateEvent();
    [SerializeField]
    public GameStateManager.GameStateEvent OnEnded = new GameStateManager.GameStateEvent();

    public void StartState()
    {
        OnStart.Invoke(Type);
    }
    public void EndState()
    {
        OnEnded.Invoke(Type);
    }
}
