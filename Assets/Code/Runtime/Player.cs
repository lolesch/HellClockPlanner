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
            
            var baseStats = DataProvider.Instance.GetBaseStats();
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

        public void SetSkillIdAtSlotIndex( int slotIndex, SkillId skillId )
        {
            SkillSlots[slotIndex] = new SkillSlotData( slotIndex, skillId );
            OnSkillSlotsChanged?.Invoke( SkillSlots );
        }

        public void SetProficiencyAtSlotIndex( int slotIndex, ProficiencyId proficiencyId )
        {
            //Skills[slotIndex].AddProficiency( proficiencyId );
            // apply global buff <- this should happen in the skill
        }
    }
}