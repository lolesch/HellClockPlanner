using System;
using Code.Data;
using Code.Utility.AttributeRef.Attributes;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Toggles
{
    [RequireComponent(typeof(TooltipHolder))]
    public abstract class AbstractToggle : Selectable, IPointerClickHandler
    {
        [SerializeField] private TooltipHolder tooltipHolder;

        [field: SerializeField] public bool isOn { get; private set; } = false;

        [SerializeField, ReadOnly] protected RadioGroup radioGroup = null;
        public RadioGroup RadioGroup => radioGroup == null ? radioGroup = GetComponentInParent<RadioGroup>() : radioGroup;

        [SerializeField] protected TextMeshProUGUI displayText = null;
        
        [SerializeField] private string toggledOffText;
        [SerializeField] private string toggledOnText;

        [SerializeField] protected Image icon = null;
        
        [SerializeField, PreviewIcon] private Sprite toggledOffSprite;
        [SerializeField, PreviewIcon] private Sprite toggledOnSprite;

        public event Action<bool> OnToggle;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (RadioGroup != null && RadioGroup.transform != transform.parent)
                radioGroup = null;

            if (isOn && RadioGroup)
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

            SetToggle(isOn);
        }

        internal void SetToggle(bool isOn)
        {
            this.isOn = isOn;
            OnToggle?.Invoke(this.isOn);

            if (icon != null)
            {
                if (toggledOffSprite != null && toggledOnSprite != null)
                    icon.sprite = this.isOn ? toggledOnSprite : toggledOffSprite;
            }

            if (displayText != null)
            {
                if (toggledOffText != string.Empty && toggledOnText != string.Empty)
                    displayText.text = this.isOn ? toggledOnText : toggledOffText;
            }

            if (this.isOn && RadioGroup)
                RadioGroup.Activate(this);

            if (!this.isOn)
                DoStateTransition(SelectionState.Normal, false);

            PlayToggleSound(this.isOn);

            Toggle(isOn);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            if (interactable)
                DoStateTransition(isOn ? SelectionState.Selected : SelectionState.Normal, false);
        }

        protected abstract void Toggle(bool isOn);

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
                return;

            if (RadioGroup && !RadioGroup.allowSwitchOff && isOn)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                SetToggle(!isOn);

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

            if (interactable)
                DoStateTransition(isOn ? SelectionState.Selected : SelectionState.Normal, false);

            tooltipHolder.HideTooltip();
        }

        public virtual void PlayToggleSound(bool isOn) { } // => AudioProvider.Instance.PlayButtonClick();
    }
}