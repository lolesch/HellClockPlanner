using System.Collections;
using Code.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public abstract class AbstractButton : Selectable, IPointerClickHandler
    {
        // TODO: disable the button for x seconds to prevent spam clicking

        //private HideTooltipCommand hideTooltipCommand;

        [SerializeField] private string tooltipText = $"Set the tooltip text in the inspector";
        private Coroutine showTooltip;

        protected override void Start()
        {
            base.Start();
            //hideTooltipCommand = new HideTooltipCommand();
        }

        protected abstract void OnLeftClick();
        protected abstract void OnRightClick();

        protected void HideTooltip()
        {
            if (showTooltip != null)
                StopCoroutine(showTooltip);

            //hideTooltipCommand.Execute();
        }

        protected void ShowTooltip(float delay) => showTooltip = StartCoroutine(DelayedShowTooltip(delay));

        private IEnumerator DelayedShowTooltip(float delay)
        {
            yield return new WaitForSeconds(delay);

            //new ShowTooltipCommand(tooltipText, delay).Execute();
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

            ShowTooltip(Constants.TooltipDelayAfterInteraction);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            ShowTooltip(Constants.TooltipDelay);
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            HideTooltip();
        }
    }
}