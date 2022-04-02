using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBob : MonoBehaviour
{
    protected Vector3 StartPosition;
    public Vector3 BobVector;
    public float BobSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        var sinfloat = Mathf.Sin(Time.time * BobSpeed);
        this.transform.localPosition = StartPosition + (sinfloat * BobVector);
    }
}
