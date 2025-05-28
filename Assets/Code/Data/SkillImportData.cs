using System;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct SkillImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillId skillId;
        [ReadOnly] [TextArea(3,5)] public string description;
        [ReadOnly] public int baseDamage;
        [ReadOnly] public int manaCost;
        [ReadOnly] public float cooldown;
        [ReadOnly] public int projectiles;
        //[ReadOnly] public int maxLevel;
        [ReadOnly] public DamageTypeId damageTypeId;

        [ReadOnly] public Sprite icon => DataProvider.Instance.GetIconFromSkillId( skillId );

        public void OnBeforeSerialize() => name = skillId.ToDescription();
        public void OnAfterDeserialize() {}
    }

    [Serializable]
    public struct SkillTagImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillId skillId;
        [ReadOnly] public SkillTagId skillTagId;
        
        public void OnBeforeSerialize() => name = $"{skillId.ToDescription()} - {skillTagId.ToDescription()}";
        public void OnAfterDeserialize() {}
    }
    
    [Serializable]
    public struct GlobalBuffImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillId skillId;
        [ReadOnly] public CharacterStatId characterStatId;
        [ReadOnly] public float amountPerRank;

        public void OnBeforeSerialize() => name = $"{skillId.ToDescription()} -> {amountPerRank} {characterStatId.ToDescription()}";

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