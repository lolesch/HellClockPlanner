using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.UI.Displays.SkillStatIconDisplays
{
    public sealed class SkillManaCostDisplay : SkillStatIconDisplay
    {
        protected override string CalculateValue()
        {
            var manaCost = $"{Skill.manaCost * Stat.Value * CharacterStat.Value:0.##}";
            return Stat.Value.isModified || CharacterStat.Value.isModified 
                ? manaCost.Colored( Color.yellow ) 
                : manaCost;
        }
    }
}