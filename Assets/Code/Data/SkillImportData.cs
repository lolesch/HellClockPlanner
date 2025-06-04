using System;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct SkillData
    {
        [HideInInspector] 
        [ReadOnly] public string name;
        
        [ReadOnly] public SkillHashId id;
        //[ReadOnly] [TextArea(3,5)] public string localizedName;
        [ReadOnly] [TextArea(3,5)] public string description;
        public Sprite icon;
        [ReadOnly] public string iconString;
        [ReadOnly] public float baseDamageMod;
        [ReadOnly] public DamageTypeId damageTypeId;
        [ReadOnly] public float cooldown;
        //[ReadOnly] public float minCooldown;
        //[ReadOnly] public bool useAttackSpeed;
        //[ReadOnly] public bool ignoreCooldownSpeed;
        //[ReadOnly] public float range;
        [ReadOnly] public string[] skillTags;
        [ReadOnly] public int manaCost;
        [ReadOnly] public int projectiles;
        //[ReadOnly] public int maxLevel;


        public SkillData( SkillDefinitionImportData definition, SkillImportData table )
        {
            #region Definition
            
            name = definition.id.ToDescription();
            id = definition.id;
            //localizedName = definition.localizedName.GetLocalizedString();
            //description = definition.descriptionKey.GetLocalizedString();
            
            iconString = $"{definition.icon}";
            // TODO: if this works, we can remove the DataProvider.Instance.GetIconFromSkillId( definition.id );
            icon = Resources.Load<Sprite>( Const.GetIconImportDirectory( $"{definition.icon}.png" ) );
            baseDamageMod = definition.baseDamageMod;
            damageTypeId = Enum.TryParse<DamageTypeId>(definition.eDamageType, true, out var damageType) ? damageType : DamageTypeId.None;
            cooldown = definition.cooldown;
            //minCooldown = definition.minCooldown;
            //useAttackSpeed = definition.useAttackSpeed;
            //ignoreCooldownSpeed = definition.ignoreCooldownSpeed;
            //range = definition.range;
            skillTags = definition.skillTags;
            
            #endregion Definition
            
            #region Table

            name = table.skillId.ToDescription();
            id = table.skillId;
            description = table.description;
            manaCost = table.manaCost;
            projectiles = table.projectiles;
            
            #endregion Table
        }
    }
    
    [Serializable]
    public struct SkillImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] 
        [ReadOnly] public string name;
        
        [ReadOnly] public SkillHashId skillId;
        [ReadOnly] [TextArea(3,5)] public string description;
        [ReadOnly] public int manaCost;
        [ReadOnly] public int projectiles;
        //[ReadOnly] public int baseDamage;
        //[ReadOnly] public float cooldown;
        //[ReadOnly] public int maxLevel;
        //[ReadOnly] public DamageTypeId damageTypeId;

        [ReadOnly] public Sprite icon => DataProvider.Instance.GetIconFromSkillId( skillId );

        public void OnBeforeSerialize() => name = skillId.ToDescription();
        public void OnAfterDeserialize() {}
    }
    
    [Serializable]
    public struct SkillDefinitionImportData
    {
        [HideInInspector] 
        public string name;
        
        public SkillHashId id;
        //public LocalizedString localizedName;
        //public LocalizedString descriptionKey;
        public string icon;
        public float baseDamageMod;
        public string eDamageType;
        public float cooldown;
        public float minCooldown;
        public bool useAttackSpeed;
        public bool ignoreCooldownSpeed;
        public float range;
        public string[] skillTags;
    }
    
    [Serializable]
    public struct SkillDefinitionsImportData
    {
        public SkillDefinitionImportData[] Skills;
    }

    [Serializable]
    public struct SkillTagImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillHashId skillHashId;
        [ReadOnly] public SkillTagId skillTagId;
        
        public void OnBeforeSerialize() => name = $"{skillHashId.ToDescription()} - {skillTagId.ToDescription()}";
        public void OnAfterDeserialize() {}
    }
    
    [Serializable]
    public struct GlobalBuffImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillHashId skillHashId;
        [ReadOnly] public CharacterStatId characterStatId;
        [ReadOnly] public float amountPerRank;

        public void OnBeforeSerialize() => name = $"{skillHashId.ToDescription()} -> {amountPerRank} {characterStatId.ToDescription()}";

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