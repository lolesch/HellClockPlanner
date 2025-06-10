using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Toggles
{
    public abstract class AbstractToggle : Selectable, IPointerClickHandler
    {
        [field: SerializeField] public bool isOn { get; private set; } = false;

        //[SerializeField] protected TextMeshProUGUI displayText = null;
        
        //[SerializeField] private string toggledOffText;
        //[SerializeField] private string toggledOnText;

        //[SerializeField] protected Image icon = null;
        
        //[SerializeField, PreviewIcon] private Sprite toggledOffSprite;
        //[SerializeField, PreviewIcon] private Sprite toggledOnSprite;

        public event Action<bool> OnToggle;

        protected override void Start()
        {
            base.Start();

            SetToggle(isOn);
        }

        internal virtual void SetToggle(bool on)
        {
            isOn = on;
            OnToggle?.Invoke(isOn);

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

            DoStateTransition();

            //PlayToggleSound(this.isOn);
            
            if( Application.isPlaying)
                Toggle(on);
        }

        protected abstract void Toggle(bool on);

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
                return;

            if (eventData.button == PointerEventData.InputButton.Left)
                SetToggle(!isOn);
        }
        
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (interactable)
                DoStateTransition();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
        
            if (interactable)
                DoStateTransition();
        }
        
        private void DoStateTransition() => DoStateTransition(isOn ? SelectionState.Selected : SelectionState.Normal, false);
    }
}