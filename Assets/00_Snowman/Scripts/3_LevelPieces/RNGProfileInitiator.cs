using System.Collections;
using UnityEngine;

public class RNGProfileInitiator : MonoBehaviour
{
    [SerializeField]
    protected LevelHandler LevelHandler;

    public delegate void InitiationEvent();
    public InitiationEvent OnRNGsInitialized;

    private void Awake()
    {
        StartCoroutine(SetupAllRNGProfiles());
    }

    IEnumerator SetupAllRNGProfiles()
    {
        foreach (var leveldata in LevelHandler.Levels)
        {
            yield return StartCoroutine(SlowRNGSetup(leveldata));
        }
        OnRNGsInitialized?.Invoke();
    }

    IEnumerator SlowRNGSetup(LevelData data)
    {
        var counter = 0;

        data.ItemRNG = new RNGProfile();
        data.ItemRNG.SetChanceOfDrop(data.ChanceOfDrop);
        foreach (var drop in data.Drops)
        {
            counter++;
            if (counter < itemsPerFrame)
            {
                data.ItemRNG.AddItem(drop.ItemPrefab, drop.Chance);
            }
            else
            {
                counter = 0;
                yield return new WaitForEndOfFrame();
            }
        }

        data.BackPropRNG = new RNGProfile();
        data.MidBackRNG = new RNGProfile();
        data.MidFrontRNG = new RNGProfile();
        data.FrontPropRNG = new RNGProfile();

        data.BackPropRNG.SetChanceOfDrop(data.PropDensity);
        data.MidBackRNG.SetChanceOfDrop(data.PropDensity);
        data.MidFrontRNG.SetChanceOfDrop(data.PropDensity);
        data.FrontPropRNG.SetChanceOfDrop(data.PropDensity);

        foreach (var prop in data.Props)
        {
            counter++;
            if (counter < itemsPerFrame)
            {
                if ((prop.Placements & PropPlacement.BACKGROUND) == PropPlacement.BACKGROUND)
                {
                    data.BackPropRNG.AddItem(prop.ItemPrefab, prop.Chance);
                }
                if ((prop.Placements & PropPlacement.MIDBACK) == PropPlacement.MIDBACK)
                {
                    data.BackPropRNG.AddItem(prop.ItemPrefab, prop.Chance);
                }
                if ((prop.Placements & PropPlacement.MIDFRONT) == PropPlacement.MIDFRONT)
                {
                    data.BackPropRNG.AddItem(prop.ItemPrefab, prop.Chance);
                }
                if ((prop.Placements & PropPlacement.FRONT) == PropPlacement.FRONT)
                {
                    data.BackPropRNG.AddItem(prop.ItemPrefab, prop.Chance);
                }
            }
            else
            {
                counter = 0;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    const float itemsPerFrame = 5;

}
