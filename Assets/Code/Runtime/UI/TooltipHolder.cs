using System;
using System.Collections;
using UnityEngine;

namespace Code.Runtime.UI
{
    public sealed class TooltipHolder : MonoBehaviour
    {
        [SerializeField] private string tooltipText = "Set the tooltip text in the inspector";
        private Coroutine _showTooltip;
        
        public static event Action<string> OnShowTooltip;
        public static event Action OnHideTooltip;
        
        public void HideTooltip()
        {
            if (_showTooltip != null)
                StopCoroutine(_showTooltip);

            OnHideTooltip?.Invoke();
        }

        public void ShowTooltip(float delay) => _showTooltip = StartCoroutine(DelayedShowTooltip(delay));

        private IEnumerator DelayedShowTooltip(float delay)
        {
            yield return new WaitForSeconds(delay);

            OnShowTooltip?.Invoke(tooltipText);
        }
    }
}