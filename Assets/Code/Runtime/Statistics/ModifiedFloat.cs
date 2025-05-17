using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ValueType = Code.Data.Enums.ValueType;

namespace Code.Runtime.Statistics
{
    public sealed class ModifiedFloat : IFormattable
    {
        private readonly float _baseValue;
        private readonly ValueType _valueType;
        private readonly List<Modifier> _modifiers;
        
        private float _totalValue;
        
        public bool isModified => _modifiers.Any();
        
        public event Action<float> OnTotalChanged;
        
        public ModifiedFloat( float baseValue, ValueType valueType )
        {
            _baseValue = baseValue;
            _valueType = valueType;
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
            return _valueType switch
            {
                ValueType.Flat => $"{_totalValue:0.##}",
                ValueType.Percent => $"{_totalValue:P#}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public string ToString(string format, IFormatProvider provider) => _totalValue.ToString( format, provider );
    }
}