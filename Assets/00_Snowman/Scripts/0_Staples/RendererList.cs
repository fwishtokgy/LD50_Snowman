using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererList : MonoBehaviour
{
    [SerializeField]
    protected Transform root;
    public Transform Root { get { return root; } }

    Dictionary<Renderer, bool> visibilityValues;

    protected bool isVisible;
    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
        set
        {
            isVisible = value;
            Refresh();
        }
    }

    public void Refresh()
    {
        SetVisibility(root);
    }

    public void SelectiveRefresh(Transform target)
    {
        SetVisibility(target);
    }

    protected void SetVisibility(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(AsyncSetVisibility(target));
    }
    IEnumerator AsyncSetVisibility(Transform target)
    {
        if (visibilityValues == null)
        {
            visibilityValues = new Dictionary<Renderer, bool>();
        }

        // Check for unregistered renderers
        var renderers = target.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            if (renderer != null)
            {
                if (!visibilityValues.ContainsKey(renderer))
                {
                    visibilityValues.Add(renderer, renderer.enabled);
                }
                else
                {
                    visibilityValues[renderer] = renderer.enabled;
                }
                //print("Storing " + renderer.name + " as " + renderer.enabled);
            }
            yield return new WaitForEndOfFrame();
        }

        // Set renderers to the current visibility value.
        // Also cache dead keys
        var keysToRemove = new Queue<Renderer>();
        foreach (var pair in visibilityValues)
        {
            if (pair.Key != null)
            {
                pair.Key.enabled = isVisible;
                //print("Setting " + pair.Key.name + " as " + (isVisible ? pair.Value : false));
            }
            else
            {
                keysToRemove.Enqueue(pair.Key);
            }
            yield return new WaitForEndOfFrame();
        }

        // do cleanup of dead keys
        while (keysToRemove.Count > 0)
        {
            var key = keysToRemove.Dequeue();
            visibilityValues.Remove(key);
        }
    }
}
