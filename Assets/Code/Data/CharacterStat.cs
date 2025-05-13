using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.Enums;
using Code.Runtime.Statistics;

namespace Code.Data
{
    [Serializable]
    public sealed class CharacterStat
    {
        public readonly CharacterStatId Stat;
        public readonly float BaseValue;
        private readonly List<float> _modifiers;
        public bool isModified => 0 < _modifiers.Count;
        public float addedValue => _modifiers.Sum();
        public float totalValue => BaseValue + addedValue;
            
        public CharacterStat( CharacterStatImportData data )
        {
            Stat = data.Id;
            BaseValue = data.BaseValue;
            _modifiers = new List<float>();
        }
        
        public void AddModifier( float modifier ) => _modifiers.Add( modifier );
        public bool TryRemoveModifier( float modifier ) => _modifiers.Remove( modifier );
    }
    
    [Serializable]
    public sealed class DerivedCharacterStat
    {
        private readonly CharacterStat _addedStat;
        private readonly CharacterStat _percentStat;
        public bool isModified => _addedStat.isModified || _percentStat.isModified;
        public float totalValue => _addedStat.totalValue * _percentStat.totalValue;
            
        public DerivedCharacterStat( CharacterStat add, CharacterStat percent )
        {
            _addedStat = add;
            _percentStat = percent;
        }
    }
}