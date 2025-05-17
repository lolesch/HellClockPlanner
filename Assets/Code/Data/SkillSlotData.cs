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

        public SkillId _skillHashId; // do not rename "_skillHashId" for json importer to work
        public int _slotIndex; // do not rename "_slotIndex" for json importer to work

        public SkillSlotData( int slotIndex, SkillId skillHashId )
        {
            name = $"[{slotIndex}] {skillHashId.ToDescription()}";
            
            _slotIndex = slotIndex;
            _skillHashId = skillHashId;
        }
    }
}