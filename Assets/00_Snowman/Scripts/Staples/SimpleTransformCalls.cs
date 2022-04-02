using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTransformCalls : MonoBehaviour
{
    public void Scale(float newScale)
    {
        this.transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
