using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyStateMonoBehaviour : MyMonoBehaviour
{
    protected GameState state;

    protected virtual void OnStateStart() { }
    protected virtual void OnStateEnd() { }

    protected IEnumerator WaitForStateManager(StateType targetState)
    {
        yield return new WaitUntil(() => GameStateManager.Instance.IsInitialized);
        state = GameStateManager.Instance.GetState(targetState);
        state.RunOnStart(OnStateStart);
        state.RunOnEnded(OnStateEnd);
    }
}
