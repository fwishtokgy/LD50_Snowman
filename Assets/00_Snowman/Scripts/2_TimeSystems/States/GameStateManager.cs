using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : Singleton<GameStateManager>
{
    protected Dictionary<StateType, GameState> gameStates;

    [System.Serializable]
    public delegate void GameStateChangeEvent(StateType state);
    protected event GameStateChangeEvent ChangedState;

    public void OnStateChange(GameStateChangeEvent func)
    {
        if (IsInitialized)
        {
            func(StartingState);
        }
        ChangedState += func;
    }

    protected StateType currentState;

    [SerializeField]
    protected StateType StartingState;

    public bool IsInitialized { get; protected set; }

    private void Start()
    {
        gameStates = new Dictionary<StateType, GameState>();
        var index = 0;
        foreach (var gamestate in GetComponentsInChildren<GameState>())
        {
            gamestate.Initialize(index, this);
            gameStates.Add(gamestate.Type, gamestate);
            index++;
        }
        IsInitialized = true;
        currentState = StartingState;
        ChangedState.Invoke(StartingState);
    }
    public GameState GetState(StateType name)
    {
        if (gameStates != null && gameStates.ContainsKey(name))
        {
            return gameStates[name];
        }
        return null;
    }

    public void ChangeState(StateType newState)
    {
        if (gameStates != null && 
            gameStates.ContainsKey(newState) && 
            currentState != newState)
        {
            ChangedState.Invoke(newState);
            currentState = newState;
        }
    }
}
