using System;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct Modifier : IEquatable<Modifier>
    {
        public readonly float Value;
        public readonly IModifierSource Source;

        public Modifier( float value, IModifierSource source )
        {
            Value = value;
            Source = source;
        }
        
        public static implicit operator float( Modifier mod ) => mod.Value;

        public bool Equals( Modifier other ) => Mathf.Approximately( Value, other.Value ) 
                                                && Source.guid == other.Source.guid;
    }

    public interface IModifierSource
    {
        Guid guid { get; }
    }
}