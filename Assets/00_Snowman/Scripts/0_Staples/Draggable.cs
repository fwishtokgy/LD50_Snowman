using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu("UI/Effects/Draggable", 5)]
    public class Draggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// The difference between the pivot of the draggable object and the current mouse/touch position
        /// </summary>
        Vector3 clickDifference;

        /// <summary>
        /// Whether the object this script is attached to is currently being dragged
        /// </summary>
        bool isBeingDragged;

        /// <summary>
        /// The camera that is drawing the canvas the object is located on
        /// </summary>
        protected Camera cam;


        [Serializable]
        public class TouchDownEvent : UnityEvent { }
        
        [FormerlySerializedAs("onTouchDown")]
        [SerializeField]
        private TouchDownEvent onTouchDown = new TouchDownEvent();

        public TouchDownEvent OnTouchDown
        {
            get
            {
                return onTouchDown;
            }
            set
            {
                onTouchDown = value;
            }
        }

        [Serializable]
        public class TouchReleaseEvent : UnityEvent { }
        
        [FormerlySerializedAs("onTouchRelease")]
        [SerializeField]
        private TouchReleaseEvent onTouchRelease = new TouchReleaseEvent();

        public TouchReleaseEvent OnTouchRelease
        {
            get
            {
                return onTouchRelease;
            }
            set
            {
                onTouchRelease = value;
            }
        }

        void Awake()
        {
            if (cam == null)
            {
                var raycasterCheck = this.GetComponentInParent<GraphicRaycaster>();
                if (raycasterCheck == null)
                {
                    Debug.LogError("No GraphicRaycaster found on object or its parent(s): " + this.gameObject.name, transform);
                }
                cam = this.GetComponentInParent<Canvas>().worldCamera;
            }
        }

        /// <summary>
        /// Called when an object is first pressed down upon.
        /// </summary>
        /// <param name="eventData">Data regarding the initiating touch</param>
        public void OnPointerDown(PointerEventData eventData)
        {
            var screenPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            clickDifference = transform.position - screenPoint;
            isBeingDragged = true;
            StartCoroutine(Drag());

            onTouchDown.Invoke();
        }

        /// <summary>
        /// Called when the object is released from touch.
        /// </summary>
        /// <param name="eventData">Data regarding the releasing touch</param>
        public void OnPointerUp(PointerEventData eventData)
        {
            isBeingDragged = false;

            onTouchRelease.Invoke();
        }

        /// <summary>
        /// Performs the actual update of the object's position based on the touch input.
        /// </summary>
        /// <returns>Returns null while the drag is still occuring</returns>
        private IEnumerator Drag()
        {
            while (isBeingDragged)
            {
                var screenPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                var newPosition = new Vector2(screenPoint.x + clickDifference.x, screenPoint.y + clickDifference.y);
                transform.position = newPosition;
                yield return null;
            }
        }
    }
}