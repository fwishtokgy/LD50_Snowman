using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        /// <summary>
        /// The UnityEvent that will be sent when this UI item is pressed down upon.
        /// </summary>
        [Serializable]
        public class HoverEnterEvent : UnityEvent { }
        /// <summary>
        /// An instance of the custom UnityEvent for handling initial touch downs.
        /// </summary>
        [FormerlySerializedAs("onHoverEnter")]
        [SerializeField]
        protected HoverEnterEvent onHoverEnter = new HoverEnterEvent();
        /// <summary>
        ///  Public accessor to the event that fires on an initial touch down.
        /// </summary>
        public virtual HoverEnterEvent OnHoverEnter
        {
            get
            {
                return onHoverEnter;
            }
            set
            {
                onHoverEnter = value;
            }
        }

        /// <summary>
        /// The UnityEvent that will be sent when this UI item is pressed down upon.
        /// </summary>
        [Serializable]
        public class HoverExitEvent : UnityEvent { }
        /// <summary>
        /// An instance of the custom UnityEvent for handling initial touch downs.
        /// </summary>
        [FormerlySerializedAs("onHoverEnter")]
        [SerializeField]
        protected HoverExitEvent onHoverExit = new HoverExitEvent();
        /// <summary>
        ///  Public accessor to the event that fires on an initial touch down.
        /// </summary>
        public virtual HoverExitEvent OnHoverExit
        {
            get
            {
                return onHoverExit;
            }
            set
            {
                onHoverExit = value;
            }
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            onHoverEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onHoverExit.Invoke();
        }

    }
}