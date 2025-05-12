using System;
using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.UI.Displays;

namespace Code.Runtime
{
    public sealed class Player
    {
        //private PlayerSaveData _config;

        public readonly CharacterStat[] Stats;
        public readonly SkillSlotData[] SkillSlots;

        public event Action<SkillSlotData[]> OnSkillSlotsChanged;

        public Player()
        {
            var baseStats = DataProvider.Instance.GetBaseStats();
            Stats = new CharacterStat[baseStats.Count];
            for( var i = 0; i < baseStats.Count; i++ )
                Stats[i] = new CharacterStat( baseStats[i] );
            
            SkillSlots = new SkillSlotData[] {
                new ( 0, SkillId.None ),
                new ( 1, SkillId.None ),
                new ( 2, SkillId.None ),
                new ( 3, SkillId.None ),
                new ( 4, SkillId.None ),
            };
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