using Code.Data.Enums;
using Code.Runtime.UI.Buttons;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillLevelDisplay : MonoBehaviour
    {
        [SerializeField] private SkillLevelButton levelDownButton;
        [SerializeField] private SkillLevelButton levelUpButton;

        [SerializeField] private GameObject statParent;
        [SerializeField] private TextMeshProUGUI skillBaseDamage;
        [SerializeField] private TextMeshProUGUI skillManaCost;
        [SerializeField] private TextMeshProUGUI skillCooldown;
        
        public void SetSkill( Skill skill )
        {
            levelDownButton.SetSkill( skill );
            levelUpButton.SetSkill( skill );
            
            skillBaseDamage.text = $"BaseDamage: {skill.baseDamage}%";
            skillManaCost.text = GetManaCostString( skill );
            skillCooldown.text = GetCooldownString( skill ) + "s";
        }
        
        public void ActivateStatParent( bool isActive ) => statParent.SetActive( isActive );
        
        private string GetManaCostString( Skill skill )
        {
            var stat = skill.GetStat( SkillStatId.ManaCostReduction );
            var manaCost = $"{skill.manaCost * stat.Value:0.##}";
            return stat.Value.isModified ? manaCost.Colored( Color.yellow ) : manaCost;
        }

        private string GetCooldownString( Skill skill )
        {
            var stat = skill.GetStat( SkillStatId.CooldownReduction );
            var cooldown = $"{skill.cooldown * stat.Value:0.##}";
            return stat.Value.isModified ? cooldown.Colored( Color.yellow ) : cooldown;
        }
    }
}