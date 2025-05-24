using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Code.Utility.Tools.Statistics;
using Unity.Mathematics;
using UnityEngine;
using Modifier = Code.Runtime.Statistics.Modifier;

namespace Code.Runtime
{
    public sealed class Skill : IModifierSource
    {
        private SkillStat[] _stats;
        private readonly SkillImportData _config;
        private readonly Dictionary<int, Proficiency> _proficiencies;
        public readonly List<GlobalBuffImportData> GlobalBuffs;
        
        public readonly List<SkillTagId> Tags;
        public readonly int MaxLevel;
        public int level { get; private set; } = 1;
        public SkillId skillId => _config.skillId;
        public Sprite icon => _config.icon;
        public string description => _config.description;
        public int manaCost => _config.manaCost; // TODO: get level dependent value
        public float cooldown => _config.cooldown; // TODO: get level dependent value
        public int baseDamage => _config.baseDamage; // TODO: get level dependent value
        public int rank => _proficiencies.Where( x => x.Value.skillStatId != SkillStatId.None ).Sum( x => (int)x.Value.rarity ); // (+1?)

        //public bool isAssigned => GameState.Player.SkillSlots.Select( x => x._skillHashId ).Contains( _config.id );
        

        public event Action OnProficienciesChanged;
        public event Action<int> OnLevelChanged;
        
        public Skill( SkillImportData config, List<GlobalBuffImportData> globalBuffs )
        {
            _config = config;
            GlobalBuffs = globalBuffs;
            MaxLevel = 4; // TODO: get from config.maxLevel;
            _proficiencies = new Dictionary<int, Proficiency>();
            Tags = DataProvider.Instance.GetSkillTagsForSkill( _config.skillId );
        }
        
        public SkillStat GetStat( SkillStatId statId ) => GetStats().First( x => x.Stat == statId );

        public SkillStat[] GetStats()
        {
            if( _stats != null )
                return _stats;

            _stats = new SkillStat[]
            {
                new ( SkillStatId.Damage, _config.baseDamage, ModType.Percent ),
                new ( SkillStatId.CriticalHitChance, 0, ModType.Percent ),
                new ( SkillStatId.ManaCostReduction, 100, ModType.Percent ),
                new ( SkillStatId.CooldownReduction, 100, ModType.Percent ),
                new ( SkillStatId.SkillSpeed, 100, ModType.Percent ),
                new ( SkillStatId.EffectArea, 100, ModType.Percent ),
                new ( SkillStatId.EffectDuration, 0, ModType.Percent ),
                new ( SkillStatId.ProjectileAmount, _config.projectiles, ModType.Flat ),
                new ( SkillStatId.ProjectileBounces, 0, ModType.Flat ),
            };

            return _stats;
        }

        public void AddProficiency( Proficiency proficiency, int proficiencySlotIndex )
        {
            if( _proficiencies.TryGetValue( proficiencySlotIndex , out var existing ) )
            {
                GetStat( existing.skillStatId ).TryRemoveModifier( new Modifier( existing.value, this ) );
                _proficiencies[proficiencySlotIndex] = proficiency;
            }
            else
            {
                _proficiencies.Add( proficiencySlotIndex, proficiency );
            
                foreach( var buff in GlobalBuffs )
                   GameState.Player.GetStat( buff.characterStatId ).AddModifier( new Modifier( buff.amountPerRank * (int)proficiency.rarity, this ) );
            }
            
            if( proficiency.skillStatId != SkillStatId.None )
                GetStat( proficiency.skillStatId ).AddModifier( new Modifier( proficiency.value, this ) );
            
            OnProficienciesChanged?.Invoke();
        }

        public void RemoveProficiency( Proficiency proficiency, int proficiencySlotIndex )
        {
            _proficiencies.Remove( proficiencySlotIndex );
            GetStat( proficiency.skillStatId ).TryRemoveModifier( new Modifier( proficiency.value, this ) );

            // TODO: remove only once -> not all! maybe lookup by proficiencySlotIndex
            GlobalBuffs.ForEach( x => GameState.Player.GetStat( x.characterStatId )
                .TryRemoveAllModifiersBySource( this ) );
            
            OnProficienciesChanged?.Invoke();
        }

        public void RevertGlobalBuffs() => GlobalBuffs.ForEach( x =>
            GameState.Player.GetStat( x.characterStatId ).TryRemoveAllModifiersBySource( this ) );

        public void ChangeLevel( int increment )
        {
            level = math.clamp( level + increment, 1, MaxLevel );
            OnLevelChanged?.Invoke(level);
        }
    }
}