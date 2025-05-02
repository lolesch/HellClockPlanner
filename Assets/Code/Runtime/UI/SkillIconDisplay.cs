using System;
using System.Linq;
using Code.Data.Enums;
using Code.Data.ScriptableObjects;
using Code.Runtime.Serialisation;
using  Code.Utility.AttributeRef.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public sealed class SkillIconDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerSave playerSave;
        [SerializeField] private SkillIcons icons;
        [SerializeField] private TMP_Dropdown dropdown;
        private Image _icon;
        [SerializeField, ReadOnly] private int slotIndex;
            
        private void OnValidate()
        {
            _icon = GetComponent<Image>();
            slotIndex = transform.parent.GetSiblingIndex();
        }

        private void OnEnable()
        {
            playerSave.OnSaveLoaded += OnSaveLoaded;
            dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(dropdown); });
        }

        private void OnDisable()
        {
            playerSave.OnSaveLoaded -= OnSaveLoaded;
            dropdown.onValueChanged.RemoveListener(delegate { DropdownValueChanged(dropdown); });
        }

        private void OnSaveLoaded( PlayerSave save )
        {
            var skill = save.GetSkillFromSlotIndex( slotIndex );
            //var color = skill == SkillHashId.None ? Color.clear : Color.white;
            var sprite = icons.GetIconFromSkillHashId( skill );

            //_icon.color = color;
            _icon.sprite = sprite;
            _icon.enabled = sprite != null;
        }

        void DropdownValueChanged( TMP_Dropdown change )
        {
            var skillHashId = ( Enum.GetValues( typeof( SkillHashId ) ) as SkillHashId[] )!
                .First( x => x.ToString() == change.options[change.value].text );
            
            playerSave.SetSkillInSlotIndex( slotIndex, skillHashId );
        }
    }
}