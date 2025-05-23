using System;
using Code.Data.Enums;
using Code.Data.ScriptableObjects;
using Code.Runtime.Provider;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;
using static System.String;

namespace Code.Data
{
    [Serializable]
    public struct ProficiencyImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillId skillId;
        [ReadOnly] public SkillStatId skillStatId;
        [ReadOnly] public string modDescription;
        [ReadOnly] public ModType modType;
        [ReadOnly] public string proficiencyName;
        [ReadOnly] public float common;
        [ReadOnly] public float magic;
        [ReadOnly] public float rare;
        [ReadOnly] public float epic;
        
        public void OnBeforeSerialize() => name = $"{skillId.ToDescription()} -> {skillStatId.ToDescription()}";

        public void OnAfterDeserialize() {}
        
        public float GetValue( RarityId rarity ) => rarity switch
        {
            RarityId.Common => common,
            RarityId.Magic => magic,
            RarityId.Rare => rare,
            RarityId.Epic => epic,
            _ => throw new ArgumentOutOfRangeException( nameof( rarity ), rarity, null )
        };
    }
    
    [Serializable]
    public struct Proficiency : IEquatable<Proficiency>
    {
        [HideInInspector] public string name;
        [ReadOnly] public SkillId skillId;
        [ReadOnly] public string modDescription;
        [ReadOnly] public SkillStatId skillStatId;
        [ReadOnly] public float value;
        [ReadOnly] public ModType modType;
        [ReadOnly] public RarityId rarity;
        [ReadOnly, PreviewIcon(32)] public Sprite icon;
        
        public string ToTooltipString()
        {
            string valueString = modType switch
            {
                ModType.Flat => $"{value:+0.##;-0.##}",
                ModType.Percent => $"{value:+0.##;-0.##}%",
                _ => value.ToString()
            };

            return $"{modDescription.ToDescription()} {valueString.Colored( Color.green)}";
        }

        public bool Equals( Proficiency other ) => skillId == other.skillId && modDescription == other.modDescription && value.Equals( other.value ) && rarity == other.rarity;

        public override bool Equals( object obj ) => obj is Proficiency other && Equals( other );

        public override int GetHashCode() => HashCode.Combine( (int) skillId, (int) skillStatId, value, (int) rarity );
    }
}