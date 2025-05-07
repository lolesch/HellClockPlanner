using System;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct SkillSlotData
    {
        [HideInInspector] public string name;

        public SkillId _skillHashId;// { get; private set; }
        public int _slotIndex;// { get; private set; }

        public SkillSlotData( int slotIndex, SkillId skillHashId )
        {
            this._slotIndex = slotIndex;
            this._skillHashId = skillHashId;
            name = $"[{slotIndex}] {skillHashId.ToDescription()}";
        }
    }
}