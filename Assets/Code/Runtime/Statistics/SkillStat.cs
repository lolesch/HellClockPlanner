using System;
using Code.Data.Enums;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public sealed class SkillStat
    {
        //TODO: public readonly SkillStatData Config;
        public readonly SkillStatId Stat;
        public readonly ModifiedFloat Value;

        public SkillStat( SkillStatId id, float baseValue, StatValueType valueType )
        {
            Stat = id;
            Value = new ModifiedFloat( baseValue );
        }

        public void AddModifier( Modifier modifier ) => Value.AddModifier( modifier );

        public bool TryRemoveModifier( Modifier modifier ) => Value.TryRemoveModifier( modifier );
        
        public bool TryRemoveAllModifiersBySource( Guid source ) => Value.TryRemoveAllModifiersBySource( source );
    }
}