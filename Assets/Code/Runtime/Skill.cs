using System;
using System.Collections.Generic;
using ZLinq;
using Code.Data;
using Code.Data.Enums;
using Code.Data.Imports;
using Code.Data.Imports.Skills;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Unity.Mathematics;
using UnityEngine;
using Modifier = Code.Runtime.Statistics.Modifier;

namespace Code.Runtime
{
    public sealed class Skill : IModifierSource
    {
        private SkillStat[] _stats;
        private readonly SkillData _config;
        private readonly Dictionary<int, Proficiency> _proficiencies;
        public readonly List<GlobalBuffImportData> GlobalBuffs; // TODO: replace with _config.statModifiersPerRankUpgrade
        
        public readonly List<SkillTagId> Tags;
        public int level { get; private set; } = 0;
        public SkillTypeId skillTypeId => _config.type;
        // TODO: actually damageType is a list where damage is split with all entries in _damageTypeOverwrites
        public DamageTypeId damageType => _damageTypeOverwrite != DamageTypeId.None ? _damageTypeOverwrite : _config.damageTypeId;
        private DamageTypeId _damageTypeOverwrite;
        public float baseDamage => _config.baseDamageMod;
        public Sprite icon => _config.icon;
        public string name => _config.GetLocaName();
        public string description => _config.GetLocaDescription();
        public int manaCost => _config.manaCost; // TODO: get level dependent value
        public float cooldown => _config.cooldown; // TODO: get level dependent value
        //public int baseDamage => _config.baseDamage; // TODO: get level dependent value
        public int rank => _proficiencies.AsValueEnumerable().Where( x => x.Value.skillStatId != SkillStatId.None ).Sum( x => (int)x.Value.rarityId );
        public Guid guid { get; } = Guid.NewGuid();

        public bool hasDamageType => damageType == DamageTypeId.None ||
                                     ( damageType == DamageTypeId.Physical && 0 <= baseDamage );


        //public bool isAssigned => GameState.Player.SkillSlots.Select( x => x._skillHashId ).Contains( _config.id );
        
        public event Action OnProficienciesChanged;
        public event Action<int> OnLevelChanged;
        
        public Skill( SkillData config, List<GlobalBuffImportData> globalBuffs )
        {
            _config = config;
            GlobalBuffs = globalBuffs;
            _proficiencies = new Dictionary<int, Proficiency>();
            Tags = DataProvider.Instance.GetSkillTagsForSkill( _config.type );
        }
        
        public SkillStat GetStat( SkillStatId statId ) => GetStats().AsValueEnumerable().First( x => x.Stat == statId );

        public SkillStat[] GetStats()
        {
            if( _stats != null )
                return _stats;

            // TODO: replace with import data
            _stats = new SkillStat[]
            {
                new ( SkillStatId.SkillProjectileAmount, _config.projectiles, StatValueType.Number ),
                new ( SkillStatId.ProjectileBounces, 0,StatValueType.Number ),
                
                new ( SkillStatId.Damage, _config.baseDamageMod, StatValueType.Percent ),
                new ( SkillStatId.CriticalHitChance, 100, StatValueType.Percent ),
                new ( SkillStatId.CriticalHitDamage, 100, StatValueType.Percent ),
                new ( SkillStatId.ManaCost, 100, StatValueType.Percent ),
                new ( SkillStatId.Cooldown, 100, StatValueType.Percent ),
                new ( SkillStatId.SkillSpeed, 100, StatValueType.Percent ),
                new ( SkillStatId.SkillAreaOfEffect, 100, StatValueType.Percent ),
                new ( SkillStatId.Duration, 100, StatValueType.Percent ),
                new ( SkillStatId.ProjectileSpeed, 100, StatValueType.Percent ),
                new ( SkillStatId.DashDistance, 100, StatValueType.Percent ),
                new ( SkillStatId.FireLightningResistShred, 100, StatValueType.Percent ),
                new ( SkillStatId.PhysicalPlagueResistShred, 100, StatValueType.Percent ),
                new ( SkillStatId.SlowDebuffIntensity, 100, StatValueType.Percent ),
            };

            return _stats;
        }

        public void AddProficiency( Proficiency proficiency, int proficiencySlotIndex )
        {
            if( _proficiencies.TryGetValue( proficiencySlotIndex, out var existing ) )
                RemoveProficiency( existing, proficiencySlotIndex );

            if( proficiency.skillStatId == SkillStatId.None )
            {
                OnProficienciesChanged?.Invoke();
                return;
            }
            
            _proficiencies.Add( proficiencySlotIndex, proficiency );
            GetStat( proficiency.skillStatId ).AddModifier( new Modifier( proficiency.value, proficiency.guid ) );
                
            foreach( var buff in GlobalBuffs )
                GameState.Player.GetStat( buff.statId ).AddModifier( new Modifier( buff.amountPerRank * (int)proficiency.rarityId, guid ) );
            
            OnProficienciesChanged?.Invoke();
        }

        private void RemoveProficiency( Proficiency proficiency, int proficiencySlotIndex )
        {
            _proficiencies.Remove( proficiencySlotIndex );
            GetStat( proficiency.skillStatId ).TryRemoveModifier( new Modifier( proficiency.value, proficiency.guid ) );

            foreach( var buff in GlobalBuffs )
                GameState.Player.GetStat( buff.statId ).TryRemoveModifier( new Modifier( buff.amountPerRank * (int)proficiency.rarityId, guid ) );
        }

        private void AddLevelMods( SkillLevelStatModifier levelMods )
        {
            if( levelMods.mods == null || levelMods.mods.Length == 0 )
                return;
            
            foreach( var mod in levelMods.mods )
                GetStat( mod.stat ).AddModifier( new Modifier( mod.modifier, levelMods.guid ) );
        }

        private void RemoveLevelMods( SkillLevelStatModifier levelMods )
        {
            if( levelMods.mods == null || levelMods.mods.Length == 0 )
                return;
            
            foreach( var mod in levelMods.mods )
                GetStat( mod.stat ).TryRemoveAllModifiersBySource( levelMods.guid );
        }

        public void RevertGlobalBuffs() => GlobalBuffs.ForEach( x =>
            GameState.Player.GetStat( x.statId ).TryRemoveAllModifiersBySource( this ) );

        public void ChangeLevel( int increment )
        {
            RemoveLevelMods( _config.modifiersPerLevel.AsValueEnumerable().First(x => x.level == level ) );
            
            level = math.clamp( level + increment, 1, Const.MaxSkillLevel );
            
            AddLevelMods( _config.modifiersPerLevel.AsValueEnumerable().First(x => x.level == level ) );
            
            OnLevelChanged?.Invoke(level);
        }

        public float CalculateHitDamage() => GameState.Player.CalculateHitDamage( this ) * GetStat( SkillStatId.Damage ).Value;
    }
}