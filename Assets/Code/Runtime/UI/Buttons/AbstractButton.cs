using System;
using System.Collections;
using Code.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    public abstract class AbstractButton : Selectable, IPointerClickHandler
    {
        // TODO: disable the button for x seconds to prevent spam clicking
        
        [SerializeField] private string tooltipText = $"Set the tooltip text in the inspector";
        private Coroutine showTooltip;
        
        public static event Action OnShowTooltip;
        public static event Action OnHideTooltip;

        protected abstract void OnLeftClick();
        protected abstract void OnRightClick();

        protected void HideTooltip()
        {
            if (showTooltip != null)
                StopCoroutine(showTooltip);

            OnHideTooltip?.Invoke();
        }

        protected void ShowTooltip(float delay) => showTooltip = StartCoroutine(DelayedShowTooltip(delay));

        private IEnumerator DelayedShowTooltip(float delay)
        {
            yield return new WaitForSeconds(delay);

            OnShowTooltip?.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable || eventData.dragging)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                OnLeftClick();
            else if (eventData.button == PointerEventData.InputButton.Right)
                OnRightClick();

            HideTooltip();

            ShowTooltip(Const.TooltipDelayAfterInteraction);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            ShowTooltip(Const.TooltipDelay);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            HideTooltip();
        }
    }
}