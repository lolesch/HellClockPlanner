using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.UI.Displays.SkillStatIconDisplays
{
    public sealed class SkillSpeedDisplay : SkillStatIconDisplay
    {
        protected override string CalculateValue()
        {
            var speed = $"{100 * Stat.Value * CharacterStat.Value:0.##}%";
            return Stat.Value.isModified || CharacterStat.Value.isModified 
                ? speed.Colored( Color.yellow ) 
                : speed;
        }
    }
}