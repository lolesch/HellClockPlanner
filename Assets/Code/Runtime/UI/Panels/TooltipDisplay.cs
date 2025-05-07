using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Panels
{
    public sealed class TooltipDisplay : AbstractPanel
    {
        [SerializeField] private TextMeshProUGUI tooltip;
        private bool _showLeft;

        [SerializeField, Range(0, 100)] private float xOffsetToCursor = 10;
        private float offsetX => _showLeft ? +xOffsetToCursor : -xOffsetToCursor;

        private void OnValidate()
        {
            if( transform is RectTransform rt )
            {
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.zero;
            }
        }

        protected override void OnDisable()
        {
            TooltipHolder.OnShowTooltip -= ShowTooltip;
            TooltipHolder.OnHideTooltip -= HideTooltip;
            
            base.OnDisable();
        }

        private void OnEnable()
        {
            TooltipHolder.OnShowTooltip += ShowTooltip;
            TooltipHolder.OnHideTooltip += HideTooltip;
        }

        private void LateUpdate()
        {
            if (isActive)
                SetPosition();
        }

        private void HideTooltip() => FadeOut();

        private void ShowTooltip( string text )
        {
            tooltip.text = text;

            FadeIn();
        }

        protected override void BeforeAppear()
        {
            base.BeforeAppear();
            
            SetPivot();
            SetPosition();
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        private void SetPosition()
        {
            var mousePos = (Vector2)Input.mousePosition / rectTransform.lossyScale;
            rectTransform.anchoredPosition = new Vector2(mousePos.x + offsetX, mousePos.y);
        }

        private void SetPivot()
        {
            var position = Input.mousePosition;
            _showLeft = position.x < (Screen.width * 0.5);

            // pivot pointing towards center of screen
            var pivotX = _showLeft ? 0 : 1;
            // TODO: clamp within screen
            var pivotY = position.y.MapTo01(0, Screen.height);

            rectTransform.pivot = new(pivotX, pivotY);
        }
    }
}
