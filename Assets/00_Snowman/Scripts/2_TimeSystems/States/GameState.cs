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

    public bool IsActive { get; protected set; }

    public void Initialize(int index, GameStateManager manager)
    {
        Index = index;
        Manager = manager;
        IsInitialized = true;

        manager.OnStateChange(OnStateChange);

    }

    [System.Serializable]
    public delegate void StateEvent();
    [SerializeField]
    protected event StateEvent OnStart;
    [SerializeField]
    protected event StateEvent OnEnded;

    public void RunOnStart(StateEvent func)
    {
        if (IsActive) func();
        OnStart += func;
    }
    public void RunOnEnded(StateEvent func)
    {
        OnEnded += func;
    }

    protected void OnStateChange(StateType newState)
    {
        if (!IsActive && newState == this.Type)
        {
            OnStart?.Invoke();
            IsActive = true;
        }
        else if (IsActive && newState != this.Type)
        {
            OnEnded?.Invoke();
            IsActive = false;
        }
    }
}
