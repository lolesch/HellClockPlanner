using System;
using System.Collections.Generic;
using Code.Data.Enums;
using Code.Runtime.Statistics;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;
using ZLinq;

namespace Code.Data.Imports.Skills
{
    [Serializable]
    public class SkillData : IModifierSource
    {
        public string name;
        public int skillHashId;
        public SkillTypeId type;
        public LocalizedStringData[] localizedName;
        public LocalizedStringData[] descriptionKey;
        [PreviewIcon] public Sprite icon;
        public float baseDamageMod;
        public DamageTypeId damageTypeId;
        public SkillLevelStatModifier[] modifiersPerLevel;
        public GlobalStatDefinition[] statModifiersPerRankUpgrade; //TODO: replace with globalStatModifiers
        public SkillModifiableStatsDefinition[] skillValueModifierConfigs; 

        public float cooldown;
        //public float minCooldown;
        //public bool useAttackSpeed;
        //public bool ignoreCooldownSpeed;
        //public float range;
        public string[] skillTags;
        public int manaCost;
        public int projectiles;
        
        public Guid guid { get; } = Guid.NewGuid();

        public SkillData( SkillDefinition definition )
        {
            name = definition.name;
            skillHashId = definition.id;
            type = (SkillTypeId)definition.id;
            localizedName = definition.localizedName;
            descriptionKey = definition.descriptionKey;
            icon = Resources.Load<Sprite>( Const.GetIconPath( $"{definition.icon}" ) );
            baseDamageMod = definition.baseDamageMod;
            damageTypeId = Enum.TryParse<DamageTypeId>(definition.eDamageType, true, out var damageType) ? damageType : DamageTypeId.None;
            modifiersPerLevel = RetreiveModifiersPerLevel( definition.modifiersPerLevel );
            statModifiersPerRankUpgrade = definition.statModifiersPerRankUpgrade;
            skillValueModifierConfigs = definition.skillValueModifierConfigs;
            cooldown = definition.cooldown;
            //minCooldown = definition.minCooldown;
            //useAttackSpeed = definition.useAttackSpeed;
            //ignoreCooldownSpeed = definition.ignoreCooldownSpeed;
            //range = definition.range;
            skillTags = definition.skillTags;
            
            //manaCost = table.manaCost;
            //projectiles = table.projectiles;
        }

        public string GetLocaDescription( ) => descriptionKey?.AsValueEnumerable()
            .FirstOrDefault( x => x.langCode == Const.CurrentLocale.ToDescription() ).langTranslation;
        public string GetLocaName(  ) => localizedName?.AsValueEnumerable()
            .FirstOrDefault( x => x.langCode == Const.CurrentLocale.ToDescription() ).langTranslation;

        private SkillLevelStatModifier[] RetreiveModifiersPerLevel( SkillLevelDefinition[] levelDefinitions )
        {
            if( levelDefinitions == null || levelDefinitions.Length == 0 )
                return Array.Empty<SkillLevelStatModifier>();

            Array.Sort( levelDefinitions, ( x, y ) => x.key.CompareTo( y.key ) );

            var modifiers = new List<SkillLevelStatModifier>();
            
            foreach( var levelDefinition in levelDefinitions )
                modifiers.Add( new SkillLevelStatModifier( levelDefinition ) );
            
            return modifiers.ToArray();
        }
    }
    
    [Serializable]
    public struct SkillDefinitions
    {
        public SkillDefinition[] Skills;
    }

    [Serializable]
    public struct SkillDefinition
    {
        public string name;
        public int id;
        //public string type; // could be an enum, names have no spaces
        public LocalizedStringData[] localizedName;
        public LocalizedStringData[] descriptionKey;
        public string icon;
        public float baseDamageMod;
        public string eDamageType;
        public SkillLevelDefinition[] modifiersPerLevel;
        public GlobalStatDefinition[] statModifiersPerRankUpgrade;
        public SkillModifiableStatsDefinition[] skillValueModifierConfigs;
        public float cooldown;
        public float minCooldown;
        public bool useAttackSpeed;
        public bool ignoreCooldownSpeed;
        public float range;
        public string[] skillTags;
    }
    
    [Serializable]
    public struct LocalizedStringData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        public string langCode;
        public string langTranslation;
        
        public void OnBeforeSerialize() { }
        public void OnAfterDeserialize() => name = $"{langCode}: \t{langTranslation}";
    }

    [Serializable]
    public struct SkillLevelStatModifier : IModifierSource
    {
        public int level;
        public SkillStatModifier[] mods;
        public Guid guid { get; }

        public SkillLevelStatModifier( SkillLevelDefinition levelDefinition )
        {
            level = levelDefinition.key;
            guid = Guid.NewGuid();
            
            var skillStatModifiers = new List<SkillStatModifier>();

            foreach( var modDefinition in levelDefinition.value )
                //TODDO: convert skillValueModifierKey to SkillStatId enum
                if( TryGetSkillStatIdFromString( modDefinition.skillValueModifierKey, out var statId ) )
                    skillStatModifiers.Add( new SkillStatModifier( statId, new Modifier( modDefinition.value, guid ) ) );
            
            mods = skillStatModifiers.ToArray();
        }
    
        private static bool TryGetSkillStatIdFromString( string skillValueModifierKey, out SkillStatId statId )
        {
            foreach( var id in (SkillStatId[])Enum.GetValues( typeof( SkillStatId ) ) )
            {
                if( id.ToDescription() + " Modifier" != skillValueModifierKey ) 
                    continue;
                
                statId = id;
                return true;
            }

            statId = SkillStatId.None;
            Debug.LogWarning( $"Could not find skill stat id: {skillValueModifierKey}" );
            return false;
        }
    }

    [Serializable]
    public struct SkillLevelDefinition
    {
        public int key; // level
        public SkillStatModifierDefinition[] value; // modifiers
    }

    [Serializable]
    public struct SkillStatModifierDefinition
    {
        public string type; // always "SkillUpgradeModifier"
        public string skillValueModifierKey;
        public string modifierType;
        public float value;
    }
    
    [Serializable]
    public struct GlobalStatDefinition
    {
        public string type; // always "StatModifierDefinition"
        public string eStatDefinition;
        public string modifierType;
        public float value;
    }
    
    [Serializable]
    public struct SkillModifiableStatsDefinition
    {
        public string type; // always "SkillValueModifierConfig"
        public string skillValueModifierKey;
        public string[] multiplicativeStats; // enum[] of skill stats
        public string[] additiveStats; // enum[] of skill stats
    }
}