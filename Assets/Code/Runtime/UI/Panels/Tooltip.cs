using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Panels
{
    public sealed class Tooltip : AbstractPanel
    {
        private TextMeshProUGUI tooltip;
        private bool showLeft;

        [SerializeField, Range(0, 100)] private float xOffsetToCursor = 10;
        private float OffsetX => showLeft ? +xOffsetToCursor : -xOffsetToCursor;

        protected override void OnDisable()
        {
            base.OnDisable();

            //ShowTooltipCommand.OnShowTooltip -= ShowTooltip;
            //HideTooltipCommand.OnHideTooltip -= HideTooltip;
        }

        private void OnEnable()
        {
            //ShowTooltipCommand.OnShowTooltip -= ShowTooltip;
            //ShowTooltipCommand.OnShowTooltip += ShowTooltip;

            //HideTooltipCommand.OnHideTooltip -= HideTooltip;
            //HideTooltipCommand.OnHideTooltip += HideTooltip;
        }

        private void Start() => tooltip = GetComponentInChildren<TextMeshProUGUI>();

        private void LateUpdate()
        {
            // do not move the tooltip during replaying
            if (IsActive)// && !ReplayProvider.Instance.IsReplaying)
                SetPosition(Input.mousePosition);
        }

        private void HideTooltip() => FadeOut();

        private void ShowTooltip(string text, Vector2 position)
        {
            tooltip.text = text;

            SetPivot(position);
            SetPosition(position);

            FadeIn();
        }

        private void SetPosition(Vector2 position)
        {
            var mousePos = position / RectTransform.lossyScale;
            RectTransform.anchoredPosition = new Vector2(mousePos.x + OffsetX, mousePos.y);
        }

        private void SetPivot(Vector2 position)
        {
            showLeft = position.x < (Screen.width * 0.5);

            /// pivot pointing towards center of screen
            var pivotX = showLeft ? 0 : 1;
            var pivotY = position.y.MapTo01(0, Screen.height);

            RectTransform.pivot = new(pivotX, pivotY);
        }
    }
}
