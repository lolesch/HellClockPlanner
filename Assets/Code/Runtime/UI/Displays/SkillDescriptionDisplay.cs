using System.Text;
using Code.Data;
using Code.Data.Enums;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillDescriptionDisplay : IndexDependentDisplay
    {
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI globalBuffsText;
        [SerializeField] private TextMeshProUGUI globalBuffsValue;

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
            var skill = GameState.Player.skills[ slot.index ];
            descriptionText.text = skill != null ? skill.description : "";
            globalBuffsText.text = skill != null ? GetGlobalBuffDescription( skill ) : "";
            
            if( globalBuffsValue.text == (skill != null ? GetGlobalBuffValue( skill ) : "" ) )
                return;
            globalBuffsValue.text = skill != null ? GetGlobalBuffValue( skill ) : "";
            globalBuffsValue.DoPunch();
        }
        
        private string GetGlobalBuffDescription( Skill skill )
        {
            var sb = new StringBuilder();
            
            foreach( var globalBuff in skill.GlobalBuffs )
            {
                var modType = GameState.Player.GetStat( globalBuff.characterStatId ).Value.ModType;

                var perRankValueString = GetModTypeRelatedValueString( globalBuff.amountPerRank, modType );
                
                sb.AppendLine( $"{globalBuff.characterStatId.ToDescription()} ({perRankValueString} per rank)" );
            }

            return sb.ToString();
        }
        
        private string GetGlobalBuffValue( Skill skill )
        {
            var sb = new StringBuilder();
            
            foreach( var globalBuff in skill.GlobalBuffs )
            {
                var modType = GameState.Player.GetStat( globalBuff.characterStatId ).Value.ModType;

                var totalValueString = GetModTypeRelatedValueString( globalBuff.amountPerRank * skill.rank, modType );

                if( skill.rank > 0 )
                    totalValueString = totalValueString.Colored( Color.yellow );
                
                sb.AppendLine( $"{totalValueString}");
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