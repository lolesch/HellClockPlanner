using System;
using System.Collections;
using Code.Data;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.UI
{
    public sealed class TooltipHolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private string tooltipText = "Set the tooltip text in the inspector";
        private Coroutine _showTooltip;
        
        public static event Action<string> OnShowTooltip;
        public static event Action OnHideTooltip;
        
        public void SetTooltipText(string text) => tooltipText = text;
        
        private void HideTooltip()
        {
            if (_showTooltip != null)
                StopCoroutine(_showTooltip);

            OnHideTooltip?.Invoke();
        }

        private void ShowTooltip(float delay)
        {
            HideTooltip();
            
            if (!string.IsNullOrEmpty(tooltipText))
                _showTooltip = StartCoroutine( DelayedShowTooltip( delay ) );
        }

        private IEnumerator DelayedShowTooltip(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            OnShowTooltip?.Invoke(tooltipText);
        }
        
        public void OnPointerClick(PointerEventData eventData) => ShowTooltip(Const.TooltipDelayAfterInteraction);
        public void OnPointerEnter(PointerEventData eventData) => ShowTooltip(Const.TooltipDelay);
        public void OnPointerExit(PointerEventData eventData) => HideTooltip();
    }
}