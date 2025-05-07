using Code.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    [RequireComponent(typeof(TooltipHolder))]
    public abstract class AbstractButton : Selectable, IPointerClickHandler
    {
        // TODO: disable the button for x seconds to prevent spam clicking

        [SerializeField] private TooltipHolder tooltipHolder;

        protected abstract void OnLeftClick();
        protected abstract void OnRightClick();

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable || eventData.dragging)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                OnLeftClick();
            else if (eventData.button == PointerEventData.InputButton.Right)
                OnRightClick();

            tooltipHolder.HideTooltip();

            tooltipHolder.ShowTooltip(Const.TooltipDelayAfterInteraction);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            tooltipHolder.ShowTooltip(Const.TooltipDelay);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            tooltipHolder.HideTooltip();
        }
    }
}