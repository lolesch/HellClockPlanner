using System;
using Code.Data.Enums;
using Code.Data.ScriptableObjects;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct ProficiencyImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        private string SkillAffix;
        [ReadOnly] public SkillId Id;
        [ReadOnly] public ProficiencyId proficiency;
        [ReadOnly] public string Name;
        [ReadOnly] public float Common;
        [ReadOnly] public float Magic;
        [ReadOnly] public float Rare;
        [ReadOnly] public float Epic;
        
        public void OnBeforeSerialize()
        {
            name = Id.ToString();
            
            if( proficiency == ProficiencyId.None)
                proficiency = (ProficiencyId) SkillAffix.ToEnum<ProficiencyId>();
        }

        public void OnAfterDeserialize()
        {
            name = Id.ToString();
            
            if( proficiency == ProficiencyId.None)
                proficiency = (ProficiencyId) SkillAffix.ToEnum<ProficiencyId>();
        }
        
        public float GetValue( RarityId rarity ) => rarity switch
        {
            RarityId.Common => Common,
            RarityId.Magic => Magic,
            RarityId.Rare => Rare,
            RarityId.Epic => Epic,
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