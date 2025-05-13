using Code.Data;
using Code.Data.Enums;
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
            _rawDamage = new DerivedCharacterStat( GameState.Player.GetStat( CharacterStatId.BaseDamage ), GameState.Player.GetStat( CharacterStatId.Damage ) );
            
            base.Start();
        }
        
        protected override string GetTotalString() => $"{(_rawDamage.totalValue + addedStat.totalValue) * percentStat.totalValue:0.###}"
            .Colored( addedStat.isModified || percentStat.isModified ? statModifiedColor : statBaseColor );
        
        protected override string GetDetailedString()
        {
            var flatString = $"{addedStat.totalValue:0.##}"
                                 .Colored( addedStat.isModified ? statModifiedColor : statBaseColor );
            var percentString = $"{percentStat.totalValue:P0}"
                                    .Colored( percentStat.isModified ? statModifiedColor : statBaseColor );
            var totalString = GetTotalString();
            
            return $"( {_rawDamage.totalValue} + {flatString} ) * {percentString} = {totalString}";
        }
    }
}