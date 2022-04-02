using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLookat : MonoBehaviour
{
    public Transform TargetTransform;

    public Transform RestTarget;

    public bool IsLookingAtTarget;

    // Update is called once per frame
    void Update()
    {
        var positiondiff = transform.position - Camera.main.transform.position;
        if (IsLookingAtTarget && TargetTransform != null)
        {
            transform.LookAt(TargetTransform);
        }
        else if (RestTarget != null)
        {
            transform.LookAt(RestTarget);
        }
    }
}
