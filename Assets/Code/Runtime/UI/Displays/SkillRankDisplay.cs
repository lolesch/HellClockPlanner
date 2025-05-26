using System;
using Code.Data;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillRankDisplay : IndexDependentDisplay
    {
        [SerializeField] private TextMeshProUGUI skillRank;

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
            skillRank.text = "Rank: " + ( _skill is { rank: > 0 } 
                ? $"{_skill.rank}".Colored( Color.yellow ) 
                : "0" );
        }
    }
}