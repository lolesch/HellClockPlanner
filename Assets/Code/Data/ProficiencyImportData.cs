using System;
using Code.Data.Enums;
using Code.Data.ScriptableObjects;
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
        
        private string _skillAffix; // replace with enum once we know the pool
        [ReadOnly] public SkillId id;
        [ReadOnly] public ProficiencyId proficiency;
        [ReadOnly] public string title;
        [ReadOnly] public float common;
        [ReadOnly] public float magic;
        [ReadOnly] public float rare;
        [ReadOnly] public float epic;
        
        public void OnBeforeSerialize()
        {
            name = id.ToString();
            
            if( proficiency == ProficiencyId.None && _skillAffix != Empty )
                proficiency = (ProficiencyId) _skillAffix.ToEnum<ProficiencyId>();
        }

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
    public struct SkillProficiency
    {
        [HideInInspector] public string name;
        [ReadOnly] public SkillId id;
        [ReadOnly] public ProficiencyId proficiency;
        [ReadOnly] public float value;
        [ReadOnly] public RarityId rarity;
        [ReadOnly, PreviewIcon(32)] public Sprite icon;
    }
}