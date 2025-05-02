using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.UI
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class Draggable : MonoBehaviour, IDragHandler
    {
        private RectTransform rectTransform;
        public RectTransform RectTransform => rectTransform == null ? rectTransform = GetComponent<RectTransform>() : rectTransform;

        public void OnDrag(PointerEventData eventData)
        {
            //if (ReplayProvider.Instance.IsReplaying)
            //    return;

            RectTransform.anchoredPosition += eventData.delta / RectTransform.lossyScale;
            RectTransform.anchoredPosition = ClampToParentRect();
        }

        private Vector2 ClampToParentRect()
        {
            var anchoredPosition = RectTransform.anchoredPosition;

            anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, 0f, ((RectTransform) RectTransform.parent).rect.width - RectTransform.rect.width);
            anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, 0f, ((RectTransform) RectTransform.parent).rect.height - RectTransform.rect.height);

            return anchoredPosition;
        }
    }
}
