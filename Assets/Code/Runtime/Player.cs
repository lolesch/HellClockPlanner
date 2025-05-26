using System;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Code.Runtime.UI.Displays;

namespace Code.Runtime
{
    public sealed class Player
    {
        //private PlayerSaveData _config;

        private CharacterStat[] _stats;
        public Skill[] skills { get; } = new Skill[5];
        
        // GEAR
        //private readonly Trinket[,] _trinkets = new Trinket[3,4];
        //private readonly Equipment[] _equipment = new Equipment[8];
        
        public readonly SkillSlotData[] SkillSlots = 
        {
            new ( 0, SkillId.None ),
            new ( 1, SkillId.None ),
            new ( 2, SkillId.None ),
            new ( 3, SkillId.None ),
            new ( 4, SkillId.None ),
        };

        public event Action<SkillSlotData[]> OnSkillSlotsChanged;
        public event Action OnStatsChanged;

        public CharacterStat GetStat( CharacterStatId statId ) => GetStats().First( x => x.Stat == statId );
        public CharacterStat[] GetStats()
        {
            if( _stats != null ) 
                return _stats;

            return InitializeCharacterStats();
        }

        private CharacterStat[] InitializeCharacterStats()
        {
            var baseStats = DataProvider.Instance.GetBaseStatImports();
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
            var baseDamage = GetStat( CharacterStatId.BaseDamage ).Value;
            var damage = GetStat( CharacterStatId.Damage ).Value;
            //var elementalDamage = GetStat( CharacterStatId.ElementalDamage ).Value;
            var addedTypeDamage = skill.damageType switch
            {
                DamageTypeId.Physical => GetStat( CharacterStatId.AddedPhysicalDamage ).Value,
                DamageTypeId.Fire => GetStat( CharacterStatId.AddedFireDamage ).Value,
                DamageTypeId.Lightning => GetStat( CharacterStatId.AddedLightningDamage ).Value,
                DamageTypeId.Plague => GetStat( CharacterStatId.AddedPlagueDamage ).Value,
                _ => 0f,
            };
            var typeDamage = skill.damageType switch
            {
                DamageTypeId.Physical => GetStat( CharacterStatId.PhysicalDamage ).Value,
                DamageTypeId.Fire => GetStat( CharacterStatId.FireDamage ).Value,
                DamageTypeId.Lightning => GetStat( CharacterStatId.LightningDamage ).Value,
                DamageTypeId.Plague => GetStat( CharacterStatId.PlagueDamage ).Value,
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

        public void SetSkillIdAtSlotIndex( int slotIndex, SkillId id )
        {
            skills[slotIndex]?.RevertGlobalBuffs();
            
            var config = DataProvider.Instance.GetSkillImports().FirstOrDefault( x => x.skillId == id );
            var globalBuffs = DataProvider.Instance.GetGlobalBuffImports().Where( x => x.skillId == id ).ToList();
            skills[slotIndex] = new Skill( config, globalBuffs );
            
            SkillSlots[slotIndex] = new SkillSlotData( slotIndex, id );
            OnSkillSlotsChanged?.Invoke( SkillSlots );
        }
    }
}