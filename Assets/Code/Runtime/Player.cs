using System;
using ZLinq;
using Code.Data;
using Code.Data.Enums;
using Code.Data.Imports;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;

namespace Code.Runtime
{
    public sealed class Player
    {
        //private PlayerSaveData _config;

        private CharacterStat[] _stats;
        
        // GEAR
        //private readonly Trinket[,] _trinkets = new Trinket[3,4];
        //private readonly Equipment[] _equipment = new Equipment[8];
        
        public Skill[] skills { get; } = new Skill[5];
        public readonly SkillSlotData[] SkillSlots = 
        {
            new ( 0, SkillTypeId.None ),
            new ( 1, SkillTypeId.None ),
            new ( 2, SkillTypeId.None ),
            new ( 3, SkillTypeId.None ),
            new ( 4, SkillTypeId.None ),
        };

        public event Action<SkillSlotData[]> OnSkillSlotsChanged;
        public event Action OnStatsChanged;

        public CharacterStat GetStat( StatId statId ) => GetStats().AsValueEnumerable().First( x => x.stat == statId );
        private CharacterStat[] GetStats() => _stats ?? InitializeCharacterStats();

        private CharacterStat[] InitializeCharacterStats()
        {
            var baseStats = DataProvider.Instance.statData;
            _stats = new CharacterStat[baseStats.Count];

            for( var i = 0; i < baseStats.Count; i++ )
            {
                _stats[i] = new CharacterStat( baseStats[i] );
                _stats[i].Value.OnTotalChanged += _ => OnStatsChanged?.Invoke();
            }
            
            return _stats;
        }

        public float CalculateHitDamage( Skill skill )
        {
            var baseDamage = GetStat( StatId.BaseDamage ).Value;
            var damage = GetStat( StatId.Damage ).Value;
            var magicDamage = GetStat( StatId.MagicDamage ).Value;
            var addedTypeDamage = skill.damageType switch
            {
                DamageTypeId.Physical => GetStat( StatId.AdditionalPhysicalDamage ).Value,
                DamageTypeId.Fire => GetStat( StatId.AdditionalFireDamage ).Value,
                DamageTypeId.Lightning => GetStat( StatId.AdditionalLightningDamage ).Value,
                DamageTypeId.Plague => GetStat( StatId.AdditionalPlagueDamage ).Value,
                _ => 0f,
            };
            var typeDamage = skill.damageType switch
            {
                DamageTypeId.Physical => GetStat( StatId.PhysicalDamage ).Value,
                DamageTypeId.Fire => GetStat( StatId.FireDamage ).Value,
                DamageTypeId.Lightning => GetStat( StatId.LightningDamage ).Value,
                DamageTypeId.Plague => GetStat( StatId.PlagueDamage ).Value,
                _ => 0f,
            };

            return ( baseDamage + addedTypeDamage) * damage * typeDamage; // * elementalDamage?
        }

        public void UpdateData( PlayerSaveData _config )
        {
            //_config = playerSaveData;

            foreach( var slot in _config.skillSlots )
                SkillSlots[slot._slotIndex] = slot;
            OnSkillSlotsChanged?.Invoke( SkillSlots );
        }

        public void SetSkillIdAtSlotIndex( int slotIndex, SkillTypeId typeId )
        {
            skills[slotIndex]?.RevertGlobalBuffs();
            
            skills[slotIndex] = null;
            
            if( typeId != SkillTypeId.None )
            {
                // TODO: select the skill from a database instead of creating garbage every time
                var skillData = DataProvider.Instance.GetSkillData( typeId );
                var globalBuffs = DataProvider.Instance.GetGlobalBuffImports().AsValueEnumerable().Where( x => x.skillTypeId == typeId ).ToList();
                skills[slotIndex] = new Skill( skillData, globalBuffs );
            }
            
            SkillSlots[slotIndex].skillTypeId =  typeId;
            OnSkillSlotsChanged?.Invoke( SkillSlots );
        }
    }
}