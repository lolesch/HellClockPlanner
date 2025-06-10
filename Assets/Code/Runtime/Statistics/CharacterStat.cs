using System;
using Code.Data.Enums;
using Code.Data.Imports.Skills;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public sealed class CharacterStat
    {
        public readonly StatData Config;
        public readonly ModifiedFloat Value;
        
        public StatId stat => Config.id;
        public bool isModified => Value.isModified;
        public bool isClamped => Config.willClamp && (Value < Config.minimumValue || Value >= Config.maximumValue);
        public float displayValue => Config.willClamp 
            ? Mathf.Clamp( Value, Config.minimumValue, Config.maximumValue ) 
            : Value;
            
        public CharacterStat( StatData config )
        {
            Config = config;
            Value = new ModifiedFloat( config.baseValue );
        }

        public override string ToString()
        {
            var valueString = Config.valueType switch
            {
                StatValueType.Number => $"{displayValue:0.##}",
                StatValueType.Percent => $"{displayValue * 100:0.##}%",
                _ => $"{displayValue}",
            };
            
            if( isClamped )
                return valueString.Styled( "MaxValueText" );
            
            return isModified ? valueString.Styled( "GreenText" ) : valueString;
        }

        public void AddModifier( Modifier modifier ) => Value.AddModifier( modifier );

        public bool TryRemoveModifier( Modifier modifier ) => Value.TryRemoveModifier( modifier );
        
        public bool TryRemoveAllModifiersBySource( IModifierSource source ) => Value.TryRemoveAllModifiersBySource( source.guid );
    }
    
    [Serializable]
    public sealed class DerivedCharacterStat
    {
        private readonly CharacterStat _addedStat;
        private readonly CharacterStat _percentStat;
        public bool isModified => _addedStat.isModified || _percentStat.isModified;
        public float totalValue => _addedStat.Value * _percentStat.Value;
            
        public DerivedCharacterStat( CharacterStat add, CharacterStat percent )
        {
            _addedStat = add;
            _percentStat = percent;
        }
    }
}