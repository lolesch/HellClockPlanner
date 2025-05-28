using Code.Data;
using Code.Data.Enums;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillStatsDisplay : IndexDependentDisplay
    {
        [SerializeField] private TextMeshProUGUI skillDamage;
        [SerializeField] private TextMeshProUGUI onHitDamage;
        
       protected override void OnEnable()
        {
            base.OnEnable();
            GameState.Player.OnStatsChanged += RefreshDisplay;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameState.Player.OnStatsChanged -= RefreshDisplay;
        }

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots ) => RefreshDisplay();

        private void RefreshDisplay()
        {
            var skill = GameState.Player.skills[ slot.index ];
            if( skill == null || skill.skillId == SkillId.None )
                return;
            
            SetSkillDamageText( skill );
            SetHitDamageText( skill );
        }

        private void SetSkillDamageText( Skill skill )
        {
            var text = skill.GetStat( SkillStatId.Damage ).Value.ToString();
            if( skillDamage.text == text )
                return;
            
            skillDamage.text = text;
            skillDamage.DoPunch();
        }

        private void SetHitDamageText( Skill skill )
        {
            var text = $"{skill.CalculateHitDamage( ):0.##}";
            if( onHitDamage.text == text )
                return;
            
            onHitDamage.text = text;
            onHitDamage.DoPunch();
        }
    }
}