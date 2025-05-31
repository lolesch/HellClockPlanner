using System;
using Code.Data.Enums;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct CharacterStatModifier : IStatModifier
    {
        /// where to put this?
        //[field: SerializeField] protected float randomRoll { get; } = Random.value;
        //[SerializeField] protected Vector2 _range;
        [field: SerializeField] public CharacterStatId stat { get; private set; }
        public Modifier modifier { get; private set; }

        public CharacterStatModifier( CharacterStatId stat, Modifier modifier )
        {
            this.stat = stat;
            this.modifier = modifier;
        }
    }

    internal interface IStatModifier
    {
        CharacterStatId stat { get; }
        Modifier modifier { get; }
    }
}