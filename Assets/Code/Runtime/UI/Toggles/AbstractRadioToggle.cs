using Code.Utility.AttributeRef.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Runtime.UI.Toggles
{
    public abstract class AbstractRadioToggle : AbstractToggle
    {
        [SerializeField, ReadOnly] private RadioGroup _radioGroup = null;
        public RadioGroup radioGroup// => _radioGroup ??= GetComponentInParent<RadioGroup>( );
        {
            get
            {
                if( _radioGroup is not null && _radioGroup.enabled ) 
                    return _radioGroup;
                
                var inParent = GetComponentInParent<RadioGroup>( );
                if( inParent is not null && inParent.enabled )
                    return _radioGroup = inParent;
        
                _radioGroup = null;
                return _radioGroup;
            }
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            
            if ( radioGroup?.transform != transform.parent )
                _radioGroup = null;

            if (isOn && radioGroup)
                radioGroup.Activate(this);
        }
#endif // UNITY_EDITOR

        protected override void OnDisable()
        {
            base.OnDisable();

            if (radioGroup)
                radioGroup.Unregister(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (radioGroup && interactable)
                radioGroup.Register(this);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (radioGroup && !radioGroup.allowSwitchOff && isOn)
                return;

            base.OnPointerClick(eventData);
        }

        internal override void SetToggle(bool on)
        {
            base.SetToggle( on );

            if ( isOn && radioGroup )
                radioGroup.Activate(this);
        }
        
        protected abstract override void Toggle(bool on);
    }
}