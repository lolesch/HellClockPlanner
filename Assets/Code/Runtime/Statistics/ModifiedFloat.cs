using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    public sealed class ModifiedFloat : IFormattable
    {
        private readonly float _baseValue;
        public readonly ModType ModType;
        private readonly List<Modifier> _modifiers;
        
        private float _totalValue;
        private float totalPercent => _totalValue * 0.01f;
        
        public bool isModified => _modifiers.Any();
        
        public event Action<float> OnTotalChanged;
        
        public ModifiedFloat( float baseValue, ModType modType )
        {
            _baseValue = baseValue;
            ModType = modType;
            _modifiers = new List<Modifier>();
            OnTotalChanged = null;
            
            _totalValue = _baseValue;
        } 
        
        public static implicit operator float( ModifiedFloat value )
        {
            return value.ModType switch
            {
                ModType.Flat => value._totalValue,
                ModType.Percent => value.totalPercent,
                _ => value._totalValue,
            };
        }

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
            
            OnTotalChanged?.Invoke( this );
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

        public override string ToString() => isModified ? $"{_totalValue:0.##}".Colored( Color.green ) : $"{_totalValue:0.##}";

        public string ToString(string format, IFormatProvider provider) => _totalValue.ToString( format, provider );

        public bool TryRemoveAllModifiersBySource( object source )
        {
            var removed = _modifiers.RemoveAll( x => x.Source == source );
            CalculateTotalValue();
            return removed > 0;
        }
    }
}