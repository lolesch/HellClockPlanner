using System;
using Code.Utility.Tools.Statistics;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    internal struct StatModifier : IComparable<StatModifier>, IStatModifier
    {
        [field: SerializeField] public StatType Stat { get; private set; }
        [field: SerializeField] public Modifier Modifier { get; private set; }

        public StatModifier( StatType stat, Modifier modifier )
        {
            Stat = stat;
            Modifier = modifier;
        }

        public int CompareTo( StatModifier other ) => Stat.CompareTo( other.Stat );
        // then by modifier
    }

    internal interface IStatModifier
    {
        StatType Stat { get; }
        Modifier Modifier { get; }
    }
}