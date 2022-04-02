using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : Singleton<GameStateManager>
{
    protected Dictionary<StateType, GameState> gameStates;

    public class GameStateEvent : UnityEvent<StateType> { }
    public GameStateEvent ChangedState = new GameStateEvent();

    protected StateType currentState;

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
        currentState = StateType.START;
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
            gameStates[currentState].EndState();
            gameStates[newState].StartState();
            ChangedState.Invoke(newState);
            currentState = newState;
        }
    }
}
