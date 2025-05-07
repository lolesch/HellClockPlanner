using System;
using Code.Data;
using Code.Data.Enums;

namespace Code.Runtime
{
    public sealed class Player
    {
        private PlayerSaveData _config;

        //public CharacterStats Stats { get; private set; } = new();
        public readonly SkillSlotData[] SkillSlots = new SkillSlotData[5];

        public event Action<SkillSlotData[]> OnSkillSlotsChanged;

        public void UpdateData( PlayerSaveData playerSaveData )
        {
            _config = playerSaveData;

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