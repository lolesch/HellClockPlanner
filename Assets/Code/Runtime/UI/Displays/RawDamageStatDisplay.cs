using Code.Data.Enums;
using Code.Utility.Extensions;

namespace Code.Runtime.UI.Displays
{
    public sealed class RawDamageStatDisplay : BaseCharacterStatDisplay
    {
        private const CharacterStatId AddedStatId = CharacterStatId.BaseDamage;
        private const CharacterStatId PercentStatId = CharacterStatId.Damage;
        
        protected override void Start()
        {
            addedStat = GameState.Player.GetStat( AddedStatId );
            percentStat = GameState.Player.GetStat( PercentStatId );
            
            base.Start();
        }
        
        protected override string GetTotalString() => $"{addedStat.totalValue * percentStat.totalValue:0.###}"
            .Colored( addedStat.isModified || percentStat.isModified ? statModifiedColor : statBaseColor );
        
        protected override string GetDetailedString()
        {
            var flatString = $"{addedStat.totalValue:0.##}"
                                 .Colored( addedStat.isModified ? statModifiedColor : statBaseColor ) + 
                             $" {AddedStatId.ToDescription()}";
            var percentString = $"{percentStat.totalValue:P0}"
                                    .Colored( percentStat.isModified ? statModifiedColor : statBaseColor ) +
                                $" {PercentStatId.ToDescription()}";
            var totalString = GetTotalString();
            
            return $"{flatString} * {percentString} = {totalString}";
        }
    }
}