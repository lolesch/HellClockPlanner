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

        [SerializeField, ReadOnly] private RadioGroup _radioGroup = null;
        public RadioGroup radioGroup => _radioGroup ??= GetComponentInParent<RadioGroup>();

        //[SerializeField] protected TextMeshProUGUI displayText = null;
        
        //[SerializeField] private string toggledOffText;
        //[SerializeField] private string toggledOnText;

        //[SerializeField] protected Image icon = null;
        
        //[SerializeField, PreviewIcon] private Sprite toggledOffSprite;
        //[SerializeField, PreviewIcon] private Sprite toggledOnSprite;

        public event Action<bool> OnToggle;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (radioGroup != null && radioGroup.transform != transform.parent)
                _radioGroup = null;

            if (isOn && radioGroup)
                radioGroup.Activate(this);
        }
#endif // if UNTIY_EDITOR

        protected override void OnDisable()
        {
            base.OnDisable();

            if (radioGroup)
                radioGroup.Unregister(this);

            if (targetGraphic && DOTween.IsTweening(targetGraphic.transform))
                _ = DOTween.Kill(targetGraphic.transform);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (radioGroup && interactable)
                radioGroup.Register(this);
        }

        protected override void Start()
        {
            base.Start();
            //hideTooltip = new HideTooltipCommand();

            SetToggle(isOn);
        }

        internal void SetToggle(bool on)
        {
            this.isOn = on;
            OnToggle?.Invoke(this.isOn);

            //if (icon != null)
            //{
            //    if (toggledOffSprite != null && toggledOnSprite != null)
            //        icon.sprite = this.isOn ? toggledOnSprite : toggledOffSprite;
            //}

            //if (displayText != null)
            //{
            //    if (toggledOffText != string.Empty && toggledOnText != string.Empty)
            //        displayText.text = this.isOn ? toggledOnText : toggledOffText;
            //}

            if (this.isOn && radioGroup)
                radioGroup.Activate(this);

            if (!this.isOn)
                DoStateTransition(SelectionState.Normal, false);

            //PlayToggleSound(this.isOn);

            Toggle(on);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            if (interactable)
                DoStateTransition(isOn ? SelectionState.Selected : SelectionState.Normal, false);
        }

        protected abstract void Toggle(bool on);

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
                return;

            if (radioGroup && !radioGroup.allowSwitchOff && isOn)
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

        //public virtual void PlayToggleSound(bool on) { } // => AudioProvider.Instance.PlayButtonClick();
    }
}