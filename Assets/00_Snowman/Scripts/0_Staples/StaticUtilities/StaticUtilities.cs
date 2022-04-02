using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticUtilities
{
    public static void ParentTo(Transform target, Transform newParent)
    {
        target.transform.parent = newParent;
        target.transform.localPosition = Vector3.zero;
        target.transform.localRotation = Quaternion.identity;
    }

    public static int CapInt(int valueToCap, int min, int max)
    {
        var returnvalue = valueToCap;
        if (valueToCap < 0) returnvalue = 0;
        else if (valueToCap > max) returnvalue = max;
        return returnvalue;
    }
}
