using System;
using System.Collections.Generic;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Runtime.UI.Toggles
{
    public class RadioGroup : MonoBehaviour
    {
        [field: SerializeField, ReadOnly] public AbstractToggle activatedToggle { get; private set; }
        [field: SerializeField] public bool allowSwitchOff { get; private set; } = false;

        public event Action OnGroupChanged;

        private readonly List<AbstractToggle> _radioToggles = new();

        public void Activate( AbstractToggle newToggle )
        {
            if (newToggle == null || activatedToggle == newToggle)
                return;

            activatedToggle = newToggle;

            foreach (var toggle in _radioToggles)
            {
                if (toggle != activatedToggle /*&& toggle.IsOn*/)
                    toggle.SetToggle(false);
            }

            OnGroupChanged?.Invoke();
        }

        public void Register(AbstractToggle item)
        {
            if (_radioToggles.Contains(item))
                return;

            _radioToggles.Add(item);

            OnGroupChanged?.Invoke();
        }

        public void Unregister(AbstractToggle item)
        {
            if (!_radioToggles.Contains(item))
                return;

            _ = _radioToggles.Remove(item);

            OnGroupChanged?.Invoke();
        }
    }
}