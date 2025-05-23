using System.Text;
using Code.Data.Enums;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillDescriptionDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI globalBuffsText;

        public void SetSkill( Skill skill )
        {
            descriptionText.text = skill.description;
            globalBuffsText.text = GetGlobalBuffString( skill );
        }
        
        private string GetGlobalBuffString( Skill skill )
        {
            var sb = new StringBuilder();
            
            foreach( var globalBuff in skill.GlobalBuffs )
            {
                var modType = GameState.Player.GetStat( globalBuff.characterStatId ).Value.ModType;

                var perRankValueString = GetModTypeRelatedValueString( globalBuff.amountPerRank, modType );
                var totalValueString = GetModTypeRelatedValueString( globalBuff.amountPerRank * skill.rank, modType );

                if( skill.rank > 0 )
                    totalValueString = totalValueString.Colored( Color.yellow );
                
                sb.AppendLine( $"{totalValueString} {globalBuff.characterStatId.ToDescription()} ({perRankValueString} per rank)" );
            }

            return sb.ToString();
        }

        private string GetModTypeRelatedValueString( float value, ModType modType ) => modType switch
        {
            ModType.Flat => $"{value:+0.##;-0.##}",
            ModType.Percent => $"{value:+0.##;-0.##}%",
            _ => $"{value}",
        };
    }
}