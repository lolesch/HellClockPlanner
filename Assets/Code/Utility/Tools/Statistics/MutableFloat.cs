using System;
using System.Collections.Generic;
using ZLinq;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Utility.Tools.Statistics
{
    public sealed class MutableFloat : IMutable<float>, IFormattable
    {
        [SerializeField] private readonly float _baseValue;
        // consider making _baseValue a Modifiable -> And growthPerLevel is applied on level up
        [SerializeField] private readonly List<Modifier> _modifiers;
        [SerializeField, ReadOnly] private float _totalValue;

        public event Action<float> OnTotalChanged;
        public MutableFloatData GetTerms() => ApplyModifiers( out _ );

        public MutableFloat( float baseValue )
        {
            _baseValue = baseValue;
            _totalValue = baseValue;
            _modifiers = new List<Modifier>();
            OnTotalChanged = null;
        }

        public static implicit operator float( MutableFloat mutableFloat ) => mutableFloat._totalValue;
        
        private void CalculateTotalValue()
        {
            ApplyModifiers( out var newTotal );

            //newTotal = Mathf.Clamp(newTotal, range.min, range.max);

            if( Mathf.Approximately( _totalValue, newTotal ) )
                return;

            _totalValue = newTotal;
            OnTotalChanged?.Invoke( _totalValue );
        }

        private MutableFloatData ApplyModifiers( out float newTotal )
        {
            var baseValue = _baseValue;
            var flatAddModValue = 0f;
            var percentAddModValue = 0f;
            
            newTotal = _baseValue;
            if( _modifiers?.Count == 0 )
                return new MutableFloatData( newTotal, baseValue, flatAddModValue, 1 + percentAddModValue );

            flatAddModValue = _modifiers.AsValueEnumerable().Where( x => x.Type == ModifierType.FlatAdd ).Sum( x => x );
            newTotal += flatAddModValue;

            percentAddModValue = _modifiers.AsValueEnumerable().Where( x => x.Type == ModifierType.PercentAdd ).Sum( x => x / 100f );
            newTotal *= 1 + percentAddModValue;
            
            return new MutableFloatData( newTotal, baseValue, flatAddModValue, 1 + percentAddModValue );
        }

        public void AddModifier( Modifier modifier )
        {
            _modifiers.Add( modifier );
            CalculateTotalValue();
        }

        public bool TryRemoveModifier( Modifier modifier )
        {
            for( var i = _modifiers.Count; i-- > 0; )
                if( _modifiers[i].Equals( modifier ) )
                {
                    _modifiers.RemoveAt( i );

                    CalculateTotalValue();
                    return true;
                }

            Debug.LogWarning( $"Modifier {modifier} not found" );
            return false;
        }

        public string ToString(string format) => _totalValue.ToString( format );
        public string ToString(string format, IFormatProvider provider) => _totalValue.ToString( format, provider );
        
        public struct MutableFloatData
        {
            public readonly float totalValue { get; }
            public readonly float baseValue { get; }
            public readonly float flatAdd { get; }
            public readonly float percentAdd { get; }
            
            public bool wasModified => !Mathf.Approximately( totalValue, baseValue );

            public MutableFloatData( float totalValue, float baseValue, float flatAdd, float percentAdd )
            {
                this.baseValue = baseValue;
                this.flatAdd = flatAdd;
                this.percentAdd = percentAdd;
                
                this.totalValue = totalValue;
            }
        }
    }

    internal interface IMutable<out T>
    {
        //float BaseValue { get; }
        //float TotalValue { get; }
        //List<Modifier> Modifiers { get; }
        void AddModifier( Modifier modifier );
        bool TryRemoveModifier( Modifier modifier );
        event Action<T> OnTotalChanged;
        
        MutableFloat.MutableFloatData GetTerms();
    }
}