using Code.Data.Enums;
using Code.Runtime.Statistics;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class DamageTypeStatDisplay : BaseCharacterStatDisplay
    {
        [SerializeField] private CharacterStatId addedStatId;
        [SerializeField] private CharacterStatId percentStatId;
        private DerivedCharacterStat _rawDamage;

        
        protected override void Start()
        {
            addedStat = GameState.Player.GetStat( addedStatId );
            percentStat = GameState.Player.GetStat( percentStatId );
            _rawDamage = new DerivedCharacterStat( GameState.Player.GetStat( CharacterStatId.BaseDamage ), 
                GameState.Player.GetStat( CharacterStatId.Damage ) );
            
            base.Start();
        }
        
        protected override string GetTotalString() => $"{(_rawDamage.totalValue + addedStat.Value) * percentStat.Value:0.###}"
            .Colored( _rawDamage.isModified ? statModifiedColor : statBaseColor );
        
        protected override string GetDetailedString()
        {
            var flatString = $"{addedStat.Value:0.##}"
                                 .Colored( addedStat.Value.isModified ? statModifiedColor : statBaseColor );
            var percentString = $"{percentStat.Value:P0}"
                                    .Colored( percentStat.Value.isModified ? statModifiedColor : statBaseColor );
            var totalString = GetTotalString();
            
            return $"( {_rawDamage.totalValue} + {flatString} ) * {percentString} = {totalString}";
        }
    }
}