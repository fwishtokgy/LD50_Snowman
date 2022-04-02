using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimplePositionLerper : MonoBehaviour
{
    public Vector3 Target { get; protected set; }

    [SerializeField]
    protected float Duration = 1f;

    protected bool IsHoningIn;

    public UnityEvent OnLerpComplete = new UnityEvent();

    public Transform dummyTarget;
    private void Start()
    {
        if (dummyTarget != null)
        {
            PassInTarget(dummyTarget.position);
        }
    }

    public void PassInTarget(Vector3 target)
    {
        Target = target;
    }
    public void StartMove()
    {
        StartMove(0);
    }
    public void StartMove(float delay)
    {
        StopAllCoroutines();
        StartCoroutine(HoneIntoTarget(delay));
    }
    IEnumerator HoneIntoTarget(float delay)
    {
        yield return new WaitForSeconds(delay);
        IsHoningIn = true;

        float moveTime = 0f;
        var oldPosition = transform.position;
        Vector3 interPos = new Vector3();
        // in duration of specified time, move sprite with lerp
        while (moveTime < Duration)
        {
            interPos = Vector3.Lerp(oldPosition, Target, moveTime / Duration);

            transform.position = interPos;
            moveTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = Target;
        IsHoningIn = false;
    }

}
