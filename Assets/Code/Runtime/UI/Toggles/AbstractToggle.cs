using System;
using System.Collections;
using Code.Data;
using Code.Utility.AttributeRef.Attributes;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Toggles
{
    public abstract class AbstractToggle : Selectable, IPointerClickHandler
    {
        [SerializeField] protected string tooltipForNotInteractable = $"Set the tooltip text in the inspector";
        [SerializeField] protected string tooltipForOn = $"Set the tooltip text in the inspector";
        [SerializeField] protected string tooltipForOff = $"Set the tooltip text in the inspector";

        public static event Action OnShowTooltip;
        public static event Action OnHideTooltip;

        private Coroutine showTooltip;

        [field: SerializeField] public bool IsOn { get; private set; } = false;

        [SerializeField, ReadOnly] protected RadioGroup radioGroup = null;
        public RadioGroup RadioGroup => radioGroup == null ? radioGroup = GetComponentInParent<RadioGroup>() : radioGroup;

        [SerializeField, ReadOnly] protected TextMeshProUGUI displayText = null;
        public TextMeshProUGUI DisplayText => displayText == null ? displayText = GetComponentInChildren<TextMeshProUGUI>() : displayText;
        [SerializeField] private string toggledOffText;
        [SerializeField] private string toggledOnText;

        [SerializeField, ReadOnly] protected Image icon = null;
        public Image Icon => icon == null ? icon = GetComponentsInChildren<Image>()[1] : icon;
        [SerializeField, PreviewIcon] private Sprite toggledOffSprite;
        [SerializeField, PreviewIcon] private Sprite toggledOnSprite;

        public event Action<bool> OnToggle;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (RadioGroup != null && RadioGroup.transform != transform.parent)
                radioGroup = null;

            if (IsOn && RadioGroup)
                RadioGroup.Activate(this);
        }
#endif // if UNTIY_EDITOR

        protected override void OnDisable()
        {
            base.OnDisable();

            if (RadioGroup)
                RadioGroup.Unregister(this);

            if (targetGraphic && DOTween.IsTweening(targetGraphic.transform))
                _ = DOTween.Kill(targetGraphic.transform);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (RadioGroup && interactable)
                RadioGroup.Register(this);
        }

        protected override void Start()
        {
            base.Start();
            //hideTooltip = new HideTooltipCommand();

            SetToggle(IsOn);
        }

        internal void SetToggle(bool isOn)
        {
            IsOn = isOn;
            OnToggle?.Invoke(IsOn);

            if (Icon != null)
            {
                if (toggledOffSprite != null && toggledOnSprite != null)
                    Icon.sprite = IsOn ? toggledOnSprite : toggledOffSprite;
            }

            if (DisplayText != null)
            {
                if (toggledOffText != string.Empty && toggledOnText != string.Empty)
                    DisplayText.text = IsOn ? toggledOnText : toggledOffText;
            }

            if (IsOn && RadioGroup)
                RadioGroup.Activate(this);

            if (!IsOn)
                DoStateTransition(SelectionState.Normal, false);

            PlayToggleSound(IsOn);

            Toggle(isOn);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            if (interactable)
                DoStateTransition(IsOn ? SelectionState.Selected : SelectionState.Normal, false);
        }

        protected abstract void Toggle(bool isOn);

        private void HideTooltip()
        {
            if (showTooltip != null)
                StopCoroutine(showTooltip);

            OnHideTooltip?.Invoke();
        }

        private void ShowTooltip(float delay, string text) => showTooltip = StartCoroutine(DelayedShowTooltip(delay, text));

        private IEnumerator DelayedShowTooltip(float delay, string text)
        {
            yield return new WaitForSeconds(delay);

            OnShowTooltip?.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
                return;

            if (RadioGroup && !RadioGroup.AllowSwitchOff && IsOn)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                SetToggle(!IsOn);

            HideTooltip();

            if (IsOn)
                ShowTooltip(Const.TooltipDelayAfterInteraction, tooltipForOn);
            else
                ShowTooltip(Const.TooltipDelayAfterInteraction, tooltipForOff);
        }
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);

            if (!interactable)
            {
                ShowTooltip(Const.TooltipDelay, tooltipForNotInteractable);
                return;
            }

            if (IsOn)
                ShowTooltip(Const.TooltipDelay, tooltipForOn);
            else
                ShowTooltip(Const.TooltipDelay, tooltipForOff);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (interactable)
                DoStateTransition(IsOn ? SelectionState.Selected : SelectionState.Normal, false);

            HideTooltip();
        }

        public virtual void PlayToggleSound(bool isOn) { } // => AudioProvider.Instance.PlayButtonClick();
    }
}