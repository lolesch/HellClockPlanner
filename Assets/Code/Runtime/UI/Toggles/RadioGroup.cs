using System;
using System.Collections.Generic;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Runtime.UI.Toggles
{
    public class RadioGroup : MonoBehaviour
    {
        [field: SerializeField, ReadOnly] public AbstractRadioToggle activatedRadioToggle { get; private set; }
        [field: SerializeField] public bool allowSwitchOff { get; private set; } = false;

        //public event Action OnGroupChanged;

        private readonly List<AbstractRadioToggle> _radioToggles = new();
        
        void Start()
        {
            if( _radioToggles?.Count == 0 || allowSwitchOff ) 
                return;
            
            if ( !activatedRadioToggle.gameObject.activeInHierarchy )
                _radioToggles[0]?.SetToggle(true);
        }

        public void Activate( AbstractRadioToggle newRadioToggle )
        {
            if (newRadioToggle == null || activatedRadioToggle == newRadioToggle)
                return;

            activatedRadioToggle = newRadioToggle;

            foreach (var toggle in _radioToggles)
            {
                if (toggle != activatedRadioToggle /*&& toggle.IsOn*/)
                    toggle.SetToggle(false);
            }

            //OnGroupChanged?.Invoke();
        }

        public void Register(AbstractRadioToggle item)
        {
            if (_radioToggles.Contains(item))
                return;

            _radioToggles.Add(item);

            //OnGroupChanged?.Invoke();
        }

        public void Unregister(AbstractRadioToggle item)
        {
            if (!_radioToggles.Contains(item))
                return;

            _ = _radioToggles.Remove(item);

            //OnGroupChanged?.Invoke();
        }
    }
}