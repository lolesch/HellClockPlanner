using System;
using System.Collections.Generic;
using System.Linq;
using ZLinq;
using Code.Data.Enums;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    public sealed class ModifiedFloat : IFormattable
    {
        private readonly float _baseValue;
        private readonly List<Modifier> _modifiers;
        
        private float _totalValue;
        
        public bool isModified => 0 < _modifiers?.Count;
        
        public event Action<float> OnTotalChanged;
        
        public ModifiedFloat( float baseValue )
        {
            _baseValue = baseValue;
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
                var flatAddModValue = _modifiers.Where( x => x.ModType == ModType.Flat ).Sum( x => x );
                var percentAddModValue = _modifiers.Where( x => x.ModType == ModType.Percent ).Sum( x => x );
                
                newTotal += flatAddModValue;
                newTotal *= 1 + percentAddModValue;
            }
            
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

        public bool TryRemoveAllModifiersBySource( Guid source )
        {
            var removed = _modifiers.RemoveAll( x => x.Source == source );
            CalculateTotalValue();
            return removed > 0;
        }

        public string ToString( string format, IFormatProvider formatProvider ) => _totalValue.ToString( format, formatProvider );
    }
}