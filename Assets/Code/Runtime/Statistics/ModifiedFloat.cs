using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.Enums;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    public sealed class ModifiedFloat : IFormattable
    {
        private readonly float _baseValue;
        private readonly ModType _modType;
        private readonly List<Modifier> _modifiers;
        
        private float _totalValue;
        
        public bool isModified => _modifiers.Any();
        
        public event Action<float> OnTotalChanged;
        
        public ModifiedFloat( float baseValue, ModType modType )
        {
            _baseValue = baseValue;
            _modType = modType;
            _modifiers = new List<Modifier>();
            OnTotalChanged = null;
            
            _totalValue = _baseValue;
        } 
        
        public static implicit operator float( ModifiedFloat value ) => value._totalValue;
        
        private void CalculateTotalValue()
        {
            var newTotal = _baseValue;

            if( isModified )
            {
                var flatAddModValue = _modifiers.Sum( x => x );
                newTotal += flatAddModValue;
            }

            //newTotal = Mathf.Clamp(newTotal, range.min, range.max);
            
            if( Mathf.Approximately( _totalValue, newTotal ) )
                return;

            _totalValue = newTotal;
            OnTotalChanged?.Invoke( _totalValue );
        }

        public void AddModifier( Modifier modifier )
        {
            _modifiers.Add( modifier );
            CalculateTotalValue();
        }

        public bool TryRemoveModifier( Modifier modifier )
        {
            if( !_modifiers.Remove( modifier ) )
            {
                Debug.LogWarning( $"Modifier {modifier} not found" );
                return false;
            }
            
            CalculateTotalValue();
            return true;
        }

        public override string ToString()
        {
            return _modType switch
            {
                ModType.Flat => $"{_totalValue:0.##}",
                ModType.Percent => $"{_totalValue * 100:0.##}%",
                _ => _totalValue.ToString(),
            };
        }

        public string ToString(string format, IFormatProvider provider) => _totalValue.ToString( format, provider );

        public bool TryRemoveAllModifiersBySource( object source )
        {
            var removed = _modifiers.RemoveAll( x => x.Source == source );
            CalculateTotalValue();
            return removed > 0;
        }
    }
}