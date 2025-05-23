using System;
using Code.Utility.Tools.Statistics;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct Modifier : IEquatable<Modifier>
    {
        private readonly float _value;
        public readonly IModifierSource Source;

        public Modifier( float value, IModifierSource source )
        {
            _value = value;
            Source = source;
        }
        
        public static implicit operator float( Modifier mod ) => mod._value;

        public bool Equals( Modifier other ) => Mathf.Approximately( _value, other._value ) && Source == other.Source;
    }
    
    public interface IModifierSource { }
}