using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanCollider : MonoBehaviour
{

    [SerializeField]
    MeltWatcher meltWatcher;

    [SerializeField]
    ScoreWatcher score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Item>())
        {
            var item = other.gameObject.GetComponent<Item>();
            if (item.Type == HitType.TEMPERATURE_EFFECT)
            {
                meltWatcher.ApplyHeat(item.Value);
                if (item.Value > 0)
                {
                    score.Decrement(Mathf.CeilToInt(item.Value / 4));
                }
            }
            else if (item.Type == HitType.SCOREUP)
            {
                score.Increment(item.Value);
            }
            item.Root.SetActive(false);
        }
    }

}
