using System;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct Modifier : IEquatable<Modifier>
    {
        [field: SerializeField] public float value { get; private set; }
        public readonly IModifierSource Source;

        public Modifier( float value, IModifierSource source )
        {
            this.value = value;
            this.Source = source;
        }
        
        public static implicit operator float( Modifier mod ) => mod.value;

        public bool Equals( Modifier other ) => value == other.value && Source == other.Source;
    }
    
    public interface IModifierSource { }
}