using System;
using Code.Data;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillRankDisplay : IndexDependentDisplay
    {
        [SerializeField] private TextMeshProUGUI rankValue;

        private Skill _skill;

        private void Start() => RefreshDisplay();

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            if( _skill != null )
                _skill.OnProficienciesChanged -= RefreshDisplay;
            
            _skill = GameState.Player.skills[ slot.index ];

            if( _skill != null )
                _skill.OnProficienciesChanged += RefreshDisplay;
            
            RefreshDisplay();
        }
        
        private void RefreshDisplay()
        {
            var text = _skill != null ? $"{_skill.rank}".Colored( Color.yellow ) : "0";
            if( rankValue.text == text )
                return;
            rankValue.text = text;
            
            rankValue.DoPunch();
        }
    }
}