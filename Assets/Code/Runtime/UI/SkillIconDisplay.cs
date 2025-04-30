using System;
using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Serialisation;
using Code.Utility.AttributeRefs;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI
{
    public sealed class SkillIconDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerSave playerSave;
        [SerializeField] private SkillIcons icons;
        private Image _icon;
        [SerializeField, ReadOnly] private int slotIndex;

        private void OnValidate()
        {
            _icon = GetComponent<Image>();
            slotIndex = transform.parent.GetSiblingIndex();
        }

        private void OnEnable() => playerSave.OnSaveLoaded += OnSaveLoaded;
        private void OnDisable() => playerSave.OnSaveLoaded -= OnSaveLoaded;

        private void OnSaveLoaded( PlayerSave save )
        {
            var skill = save.GetSkillFromSlotIndex( slotIndex );
            var color = skill == SkillHashId.None ? Color.clear : Color.white;
            var sprite = icons.GetIconFromSkillHashId( skill );
            
            _icon.color = color;
            _icon.sprite = sprite;
            _icon.enabled = sprite != null;
        }
    }
}