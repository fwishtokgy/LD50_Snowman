using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SplashUI : MyStateMonoBehaviour
{
    [SerializeField]
    protected RNGProfileInitiator RNGInitiator;

    [SerializeField]
    protected TMP_Text StatusMessage;

    protected bool IsInitialized;
    private void Awake()
    {
        RNGInitiator.OnRNGsInitialized += OnInit;
    }

    protected void OnInit()
    {
        IsInitialized = true;
        StatusMessage.text = "[PRESS SPACEBAR TO START]";
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInitialized && Input.GetKeyDown(KeyCode.Space))
        {
            GameStateManager.Instance.ChangeState(StateType.MAIN);
        }
    }
}
