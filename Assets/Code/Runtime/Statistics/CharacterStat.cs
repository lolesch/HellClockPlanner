using System;
using Code.Data;
using Code.Data.Enums;
using ValueType = Code.Data.Enums.ValueType;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public sealed class CharacterStat
    {
        public readonly CharacterStatId Stat;
        public readonly ModifiedFloat Value;
            
        public CharacterStat( CharacterStatImportData config )
        {
            Stat = config.id;
            Value = new ModifiedFloat( config.baseValue, config.valueType );
        }

        public void AddModifier( Modifier modifier ) => Value.AddModifier( modifier );

        public bool TryRemoveModifier( Modifier modifier ) => Value.TryRemoveModifier( modifier );
    }
    
    [Serializable]
    public sealed class DerivedCharacterStat
    {
        private readonly CharacterStat _addedStat;
        private readonly CharacterStat _percentStat;
        public bool isModified => _addedStat.Value.isModified || _percentStat.Value.isModified;
        public float totalValue => _addedStat.Value * _percentStat.Value;
            
        public DerivedCharacterStat( CharacterStat add, CharacterStat percent )
        {
            _addedStat = add;
            _percentStat = percent;
        }
    }
}