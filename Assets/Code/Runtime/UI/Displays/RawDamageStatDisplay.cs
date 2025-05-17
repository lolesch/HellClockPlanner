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
        
        protected override string GetTotalString() => $"{addedStat.Value * percentStat.Value:0.###}"
            .Colored( addedStat.Value.isModified || percentStat.Value.isModified ? statModifiedColor : statBaseColor );
        
        protected override string GetDetailedString()
        {
            var flatString = $"{addedStat.Value:0.##}"
                                 .Colored( addedStat.Value.isModified ? statModifiedColor : statBaseColor ) + 
                             $" {AddedStatId.ToDescription()}";
            var percentString = $"{percentStat.Value:P0}"
                                    .Colored( percentStat.Value.isModified ? statModifiedColor : statBaseColor ) +
                                $" {PercentStatId.ToDescription()}";
            var totalString = GetTotalString();
            
            return $"{flatString} * {percentString} = {totalString}";
        }
    }
}