using System;
using Code.Data.Enums;

namespace Code.Data
{
    [Serializable]
    public sealed class SkillSlotData
    {
        public SkillHashId _skillHashId;
        public readonly int _slotIndex;
    }
}