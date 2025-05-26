using Code.Data;
using Code.Data.Enums;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillStatsDisplay : IndexDependentDisplay
    {
        [SerializeField] private TextMeshProUGUI skillBaseDamage;
        [SerializeField] private TextMeshProUGUI skillManaCost;
        [SerializeField] private TextMeshProUGUI skillCooldown;
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
            skillBaseDamage.text = $"Skill Damage: {skill.GetStat( SkillStatId.Damage ).Value.ToString()}%";
            skillManaCost.text = GetManaCostString( skill );
            skillCooldown.text = GetCooldownString( skill ) + " s";
            
            onHitDamage.text = $"Hit Damage: {skill.CalculateHitDamage( ):0.##}";
        }
        
        private string GetManaCostString( Skill skill )
        {
            var smc = GameState.Player.GetStat( CharacterStatId.SkillManaCost );
            var mc = skill.GetStat( SkillStatId.ManaCost );
            var manaCost = $"{skill.manaCost * mc.Value * smc.Value:0.##}";
            return mc.Value.isModified || smc.Value.isModified ? manaCost.Colored( Color.yellow ) : manaCost;
        }

        private string GetCooldownString( Skill skill )
        {
            var cds = GameState.Player.GetStat( CharacterStatId.CooldownSpeed );
            var cdr = skill.GetStat( SkillStatId.Cooldown );
            var cooldown = $"{skill.cooldown * cdr.Value / cds.Value:0.##}";
            return cdr.Value.isModified || cds.Value.isModified ? cooldown.Colored( Color.yellow ) : cooldown;
        }
    }
}