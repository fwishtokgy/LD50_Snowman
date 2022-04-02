using System.Collections;
using UnityEngine;

public class MovementInput : MainPlayMonoBehaviour
{
    [SerializeField]
    protected Transform Root;

    [SerializeField]
    protected Transform YRoot;

    [SerializeField]
    protected MovementNodes movementNodes;

    [SerializeField]
    protected MovementNode oldNode;
    protected MovementNode currentNode;

    [SerializeField]
    protected float LerpSpeed;

    [SerializeField]
    protected float JumpDuration;

    [SerializeField]
    protected float JumpHeight;

    [SerializeField]
    protected float InputCooldown;

    protected bool isOpenToInput;

    protected bool isJumping;

    
    protected Coroutine MoveRoutine;
    protected Coroutine JumpRoutine;


    protected override void OnStateStart()
    {
        base.OnStateStart();
        if (oldNode != null)
        {
            StaticUtilities.ParentTo(Root, oldNode.transform);
            Root.parent = null;
        }
        StartCoroutine(WaitForSceneReady());
    }
    protected override void OnStateEnd()
    {
        base.OnStateEnd();
        isOpenToInput = false;
    }

    IEnumerator WaitForSceneReady()
    {
        yield return new WaitUntil(() => movementNodes.IsInitialized);
        currentNode = movementNodes.GetCurrentNode();
        yield return StartCoroutine(LerpToNewNode());
        if (!IsInitialized) IsInitialized = true;
        isOpenToInput = true;
    }

    IEnumerator LerpToNewNode()
    {
        var oldPosition = this.transform.position;
        var newPosition = currentNode.transform.position;

        var distance = Vector3.Distance(oldPosition, newPosition);
        var duration = distance / LerpSpeed;
        var curtime = 0f;
        while (curtime < duration)
        {
            curtime += Time.deltaTime;
            Root.position = Vector3.Lerp(oldPosition, newPosition, curtime / duration);
            yield return new WaitForEndOfFrame();
        }
        Root.position = newPosition;
    }
    IEnumerator CoolDown()
    {
        isOpenToInput = false;
        var curtime = 0f;
        while (curtime < InputCooldown)
        {
            curtime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        isOpenToInput = true;
    }
    IEnumerator Jump()
    {
        isJumping = true;
        var curtime = 0f;
        var factor = 0f;
        var curheight = 0f;
        var curposition = Vector3.zero;
        var midpoint = .4f * JumpDuration;
        var descent = JumpDuration - midpoint;
        while (curtime < JumpDuration)
        {
            curtime += Time.deltaTime;

            if (curtime < midpoint)
            {
                factor = Easings.QuadraticEaseInOut(curtime / midpoint);
                curheight = Mathf.Lerp(0, JumpHeight, factor);
            }
            else
            {
                factor = Easings.ExponentialEaseIn((curtime - midpoint) / descent);
                curheight = Mathf.Lerp(JumpHeight, 0, factor);
            }
            
            curposition = new Vector3(0, curheight, 0);
            YRoot.localPosition = curposition;

            yield return new WaitForEndOfFrame();
        }
        YRoot.localPosition = Vector3.zero;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            if (isOpenToInput)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ||
                Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    oldNode = currentNode;
                    currentNode = movementNodes.MoveToPrevNode();
                    if (MoveRoutine != null) StopCoroutine(MoveRoutine);
                    MoveRoutine = StartCoroutine(LerpToNewNode());
                    StartCoroutine(CoolDown());
                }
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) ||
                    Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    oldNode = currentNode;
                    currentNode = movementNodes.MoveToNextNode();
                    if (MoveRoutine != null) StopCoroutine(MoveRoutine);
                    MoveRoutine = StartCoroutine(LerpToNewNode());
                    StartCoroutine(CoolDown());
                }
            }
            
            if (!isJumping)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    JumpRoutine = StartCoroutine(Jump());
                }
            }
        }
    }
}
