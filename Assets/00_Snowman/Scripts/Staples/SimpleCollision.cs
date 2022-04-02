using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCollision : MonoBehaviour
{
    [SerializeField]
    protected UnityEvent OnCollision = new UnityEvent();

    protected bool Invoked;

    private void OnCollisionEnter(Collision collision)
    {
        Invoked = true;
        OnCollision.Invoke();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!Invoked)
        {
            Invoked = true;
            OnCollision.Invoke();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Invoked = false;
    }
}
