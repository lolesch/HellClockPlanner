using System;
using System.Linq;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.Imports.Skills
{
    [Serializable]
    public struct SkillImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] 
        [ReadOnly] public string name;
        
        [ReadOnly] public SkillTypeId skillTypeId;
        [ReadOnly] public int manaCost;
        [ReadOnly] public int projectiles;

        public void OnBeforeSerialize() => name = skillTypeId.ToDescription();
        public void OnAfterDeserialize() {}
    }

    [Serializable]
    public struct SkillTagImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillTypeId skillTypeId;
        [ReadOnly] public SkillTagId skillTagId;
        
        public void OnBeforeSerialize() => name = $"{skillTypeId.ToDescription()} - {skillTagId.ToDescription()}";
        public void OnAfterDeserialize() {}
    }
    
    [Serializable]
    public struct GlobalBuffImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillTypeId skillTypeId;
        [ReadOnly] public CharacterStatId characterStatId;
        [ReadOnly] public float amountPerRank;

        public void OnBeforeSerialize() => name = $"{skillTypeId.ToDescription()} -> {amountPerRank} {characterStatId.ToDescription()}";

        public void OnAfterDeserialize() {}
    }
    
    [Serializable]
    public struct StatusEffectImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public string statusEffectId;
        [ReadOnly] public string description;
        public void OnBeforeSerialize() => name = $"{statusEffectId.ToDescription()}";

        public void OnAfterDeserialize() {}
    }
    
    [Serializable]
    public struct ShrineImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public ShrineId shrineId;
        [ReadOnly] public CharacterStatId characterStatId;
        [ReadOnly] public int amount;
        //[ReadOnly] public int duration;
        public void OnBeforeSerialize() => name = $"{shrineId.ToDescription()} -> {amount} {characterStatId.ToDescription()}";

        public void OnAfterDeserialize() {}
    }
}