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
            descriptionText.text = _skill != null ? _skill.description : "";
            globalBuffsText.text = _skill != null ? GetGlobalBuffDescription( _skill ) : "";
            
            if( globalBuffsValue.text == (_skill != null ? GetGlobalBuffValue( _skill ) : "" ) )
                return;
            globalBuffsValue.text = _skill != null ? GetGlobalBuffValue( _skill ) : "";
            globalBuffsValue.DoPunch();
        }
        
        private string GetGlobalBuffDescription( Skill skill )
        {
            var sb = new StringBuilder();
            
            foreach( var globalBuff in skill.GlobalBuffs )
            {
                var modType = GameState.Player.GetStat( globalBuff.statId ).Value.ModType;

                var perRankValueString = GetModTypeRelatedValueString( globalBuff.amountPerRank, modType );
                
                sb.AppendLine( $"{globalBuff.statId.ToDescription()} ({perRankValueString} per rank)" );
            }

            return sb.ToString();
        }
        
        private string GetGlobalBuffValue( Skill skill )
        {
            var sb = new StringBuilder();
            
            foreach( var globalBuff in skill.GlobalBuffs )
            {
                var modType = GameState.Player.GetStat( globalBuff.statId ).Value.ModType;

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