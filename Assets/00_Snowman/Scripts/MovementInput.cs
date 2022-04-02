using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInput : MyMonoBehaviour
{
    [SerializeField]
    protected Transform Root;

    [SerializeField]
    protected MovementNodes movementNodes;

    [SerializeField]
    protected MovementNode oldNode;
    protected MovementNode currentNode;

    [SerializeField]
    protected float LerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSceneReady());
    }

    IEnumerator WaitForSceneReady()
    {
        yield return new WaitUntil(() => movementNodes.IsInitialized);
        currentNode = movementNodes.GetCurrentNode();

        yield return StartCoroutine(LerpToNewNode());
        IsInitialized = true;
        print("Nice");
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
