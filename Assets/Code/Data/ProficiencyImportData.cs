using System;
using Code.Data.Enums;
using Code.Data.ScriptableObjects;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEditor;
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
        
        public float GetValue( RarityId rarityId ) => rarityId switch
        {
            RarityId.Common => common,
            RarityId.Magic => magic,
            RarityId.Rare => rare,
            RarityId.Epic => epic,
            _ => throw new ArgumentOutOfRangeException( nameof( rarityId ), rarityId, null )
        };
    }
    
    [Serializable]
    public struct Proficiency : IModifierSource
    {
        [HideInInspector] public string name;
        [ReadOnly] public SkillId skillId;
        [ReadOnly] public string modDescription;
        [ReadOnly] public SkillStatId skillStatId;
        [ReadOnly] public float value;
        [ReadOnly] public ModType modType;
        [ReadOnly] public RarityId rarityId;
        [ReadOnly, PreviewIcon(32)] public Sprite icon;
        [ReadOnly] public Guid guid { get; private set; }

        public Proficiency( SkillId skillId, SkillStatId skillStatId, string modDescription, float value, RarityId rarityId, string name, Sprite icon, ModType modType )
        {
            this.skillId = skillId;
            this.skillStatId = skillStatId;
            this.modDescription = modDescription;
            this.value = value;
            this.rarityId = rarityId;
            this.name = name;
            this.icon = icon;
            this.modType = modType;
            guid = Guid.NewGuid();
        }

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
    }
}