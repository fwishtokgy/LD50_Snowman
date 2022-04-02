using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesUI : MyMonoBehaviour
{
    [SerializeField]
    protected List<StateUIPanel> StatePanels;

    protected Dictionary<StateType, StateUIPanel> panels;

    protected StateType currentState;

    private void Start()
    {
        foreach (var panel in StatePanels)
        {
            panel.Panel.SetActive(false);
        }
        StartCoroutine(WaitForStateManager());
    }
    protected IEnumerator WaitForStateManager()
    {
        yield return new WaitUntil(() => GameStateManager.Instance.IsInitialized);
        panels = new Dictionary<StateType, StateUIPanel>();
        foreach (var panel in StatePanels)
        {
            panels.Add(panel.State, panel);
        }
        GameStateManager.Instance.OnStateChange(ChangePanel);
    }

    protected void ChangePanel(StateType newState)
    {
        panels[currentState].Panel.SetActive(false);
        panels[newState].Panel.SetActive(true);
        currentState = newState;
    }

    [System.Serializable]
    public class StateUIPanel
    {
        public StateType State;
        public GameObject Panel;
    }
}
