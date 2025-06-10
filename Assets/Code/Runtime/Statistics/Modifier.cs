using System;
using Code.Data.Enums;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct Modifier : IEquatable<Modifier>
    {
        public readonly Guid Source;
        public readonly float Value;
        public readonly ModType ModType;

        public Modifier( float value, Guid source, ModType modType = ModType.Flat )
        {
            Value = value;
            Source = source;
            ModType = modType;
        }
        
        public static implicit operator float( Modifier mod ) => mod.Value;
        
        public readonly override string ToString() => ModType switch
        {
            ModType.Flat => $"{Value * 100:+0.###;-0.###;0.###}",  //   +123   |   -123   |    0
            ModType.Percent => $"{Value:+0.###;-0.###;0.###} %",   //   +123 % |   -123 % |    0 %

            var _ => $"?? {Value}",
        };
        
        public bool Equals( Modifier other ) => 
            Source.Equals(other.Source) && ModType == other.ModType && Mathf.Approximately( Value, other.Value );
    }

    public interface IModifierSource
    {
        Guid guid { get; }
    }
}