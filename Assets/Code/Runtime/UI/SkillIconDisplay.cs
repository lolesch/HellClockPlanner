using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.Serialisation;
using  Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public sealed class SkillIconDisplay : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int slotIndex;
        [SerializeField] private TMP_Dropdown dropdown;
        private Image _icon;
            
        private void Start()
        {
            _icon = GetComponent<Image>();
            slotIndex = transform.parent.GetSiblingIndex();
            
            SetDropdownOptions();
        }

        private void OnEnable()
        {
            DataProvider.Instance.playerSave.OnSkillSlotChanged += OnSkillSlotChanged;
            dropdown.onValueChanged.AddListener( delegate { DropdownValueChanged( dropdown ); } );
        }

        private void OnDisable()
        {
            DataProvider.Instance.playerSave.OnSkillSlotChanged -= OnSkillSlotChanged;
            dropdown.onValueChanged.RemoveListener(delegate { DropdownValueChanged(dropdown); });
        }

        private void OnSkillSlotChanged( PlayerSave save )
        {
            var skill = save.GetSkillIdAtSlotIndex( slotIndex );
            var sprite = DataProvider.Instance.skillIcons.GetIconFromSkillHashId( skill );

            _icon.sprite = sprite;
            _icon.enabled = sprite != null;
            _icon.color = skill == SkillHashId.None ? Color.clear : Color.white;

            //SetDropdownOptions();
        }
        
        void SetDropdownOptions()
        {
            //var assignedSkills = DataProvider.Instance.playerSave.GetAssignedSkillIds();
            dropdown.options = DataProvider.Instance.skillIcons.skillIcons
                .Select( x => new TMP_Dropdown.OptionData( x.name, x.icon, Color.white ) ).ToList();
        }

        void DropdownValueChanged( TMP_Dropdown change )
        {
            var skillHashId = ( Enum.GetValues( typeof( SkillHashId ) ) as SkillHashId[] )!
                .First( x => x.ToDescription() == change.options[change.value].text );
            
            DataProvider.Instance.playerSave.SetSkillIdAtSlotIndex( slotIndex, skillHashId );
        }
    }
}