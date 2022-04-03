using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField]
    protected LevelHandler levelHandler;

    [SerializeField]
    protected AmbientTemp Temperature;

    private void Start()
    {
        levelHandler.OnNewSeason += OnNewSeason;
    }

    protected void OnNewSeason(LevelData data)
    {
        Temperature.RiseRate = data.RiseRate;
    }

}
