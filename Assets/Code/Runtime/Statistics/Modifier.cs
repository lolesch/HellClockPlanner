using System;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    public struct Modifier : IEquatable<Modifier>
    {
        [SerializeField] private float _value;
        [SerializeField] private object _source;

        public Modifier( float value, object source )
        {
            _value = value;
            _source = source;
        }
        
        public static implicit operator float( Modifier mod ) => mod._value;

        public bool Equals( Modifier other ) => _value == other._value && _source == other._source;
    }
}