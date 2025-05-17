using System;
using Code.Data.Enums;
using Code.Utility.Tools.Statistics;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct StatModifier : IStatModifier
    {
        [field: SerializeField] public CharacterStatId stat { get; private set; }
        [field: SerializeField] public Modifier modifier { get; private set; }

        public StatModifier( CharacterStatId stat, Modifier modifier )
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