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
        private readonly Skill[] _skills = new Skill[5];
        
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

        public CharacterStat GetStat( CharacterStatId statId ) => GetStats().First( x => x.Stat == statId );
        public CharacterStat[] GetStats()
        {
            if( _stats != null ) 
                return _stats;
            
            var baseStats = DataProvider.Instance.GetBaseStatImports();
            _stats = new CharacterStat[baseStats.Count];
            for( var i = 0; i < baseStats.Count; i++ )
                _stats[i] = new CharacterStat( baseStats[i] );
            
            return _stats;
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
            _skills[slotIndex]?.RevertGlobalBuffs();
            
            var config = DataProvider.Instance.GetSkillImports().FirstOrDefault( x => x.skillId == id );
            var globalBuffs = DataProvider.Instance.GetGlobalBuffImports().Where( x => x.skillId == id ).ToList();
            _skills[slotIndex] = new Skill( config, globalBuffs );
            
            SkillSlots[slotIndex] = new SkillSlotData( slotIndex, id );
            OnSkillSlotsChanged?.Invoke( SkillSlots );
        }

        public void SetProficiencyAtSlotIndex( int slotIndex, Proficiency proficiency, int proficiencySlotIndex ) =>
            _skills[slotIndex].AddProficiency( proficiency, proficiencySlotIndex );

        public Skill GetSkillFromSkillId( SkillId currentSkillId ) => _skills.FirstOrDefault( x => x.skillId == currentSkillId );
        public Skill GetSkillAtSlotIndex( int slotIndex ) => _skills[slotIndex];
    }
}