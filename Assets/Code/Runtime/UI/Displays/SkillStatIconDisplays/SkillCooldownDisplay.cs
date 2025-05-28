using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.UI.Displays.SkillStatIconDisplays
{
    public sealed class SkillCooldownDisplay : SkillStatIconDisplay
    {
        protected override string CalculateValue()
        {
            var cooldown = $"{Skill.cooldown * Stat.Value / CharacterStat.Value:0.##}s";
            return Stat.Value.isModified || CharacterStat.Value.isModified 
                ? cooldown.Colored( Color.yellow ) 
                : cooldown;
        }
    }
}