using System;
using Code.Data;
using Code.Data.Enums;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public sealed class CharacterStat
    {
        public readonly CharacterStatId Stat;
        public readonly ModifiedFloat Value;
            
        public CharacterStat( CharacterStatImportData config )
        {
            Stat = config.characterStatId;
            Value = new ModifiedFloat( config.baseValue, config.modType );
        }

        public void AddModifier( Modifier modifier ) => Value.AddModifier( modifier );

        public bool TryRemoveModifier( Modifier modifier ) => Value.TryRemoveModifier( modifier );
        
        public bool TryRemoveAllModifiersBySource( IModifierSource source ) => Value.TryRemoveAllModifiersBySource( source );
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