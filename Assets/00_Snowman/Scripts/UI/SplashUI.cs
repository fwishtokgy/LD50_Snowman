using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashUI : MyStateMonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameStateManager.Instance.ChangeState(StateType.MAIN);
        }
    }
}
