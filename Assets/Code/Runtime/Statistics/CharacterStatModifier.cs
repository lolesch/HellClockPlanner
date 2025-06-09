using System;
using Code.Data.Enums;
using UnityEngine;

namespace Code.Runtime.Statistics
{
    [Serializable]
    public struct CharacterStatModifier : ICharacterStatModifier
    {
        [field: SerializeField] public CharacterStatId stat { get; private set; }
        public Modifier modifier { get; private set; }

        public CharacterStatModifier( CharacterStatId stat, Modifier modifier )
        {
            this.stat = stat;
            this.modifier = modifier;
        }
    }
    
    [Serializable]
    public struct SkillStatModifier : ISkillStatModifier
    {
        [field: SerializeField] public SkillStatId stat { get; private set; }
        [field: SerializeField] public Modifier modifier { get; private set; }

        public SkillStatModifier( SkillStatId stat, Modifier modifier )
        {
            this.stat = stat;
            this.modifier = modifier;
        }
    }

    internal interface IStatModifier<out T> where T : Enum
    {
        T stat { get; }
        Modifier modifier { get; }
    }
    internal interface ICharacterStatModifier : IStatModifier<CharacterStatId> {}
    internal interface ISkillStatModifier : IStatModifier<SkillStatId> {}
}