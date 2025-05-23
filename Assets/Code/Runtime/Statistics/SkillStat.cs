using System;
using Code.Data;
using Code.Data.Enums;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public sealed class SkillStat
    {
        public readonly SkillStatId Stat;
        public readonly ModifiedFloat Value;

        public SkillStat( SkillStatId id, float baseValue, ModType modType )
        {
            Stat = id;
            Value = new ModifiedFloat( baseValue, modType );
        }

        public void AddModifier( Modifier modifier ) => Value.AddModifier( modifier );

        public bool TryRemoveModifier( Modifier modifier ) => Value.TryRemoveModifier( modifier );
        
        public bool TryRemoveAllModifiersBySource( IModifierSource source ) => Value.TryRemoveAllModifiersBySource( source );
    }
}