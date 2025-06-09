using System;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct Modifier : IEquatable<Modifier>
    {
        //public readonly Vector2Int Range; // TODO: implement
        [field: SerializeField] public float Value { get; private set; }
        public readonly Guid Source;

       //public Modifier( float value, IModifierSource source )
       //{
       //    Value = value;
       //    Source = source.guid;
       //}

        public Modifier( float value, Guid source )
        {
            Value = value;
            Source = source;
        }
        
        public static implicit operator float( Modifier mod ) => mod.Value;

        public bool Equals( Modifier other ) => Mathf.Approximately( Value, other.Value ) 
                                                && Source == other.Source;
    }

    public interface IModifierSource
    {
        Guid guid { get; }
    }
}