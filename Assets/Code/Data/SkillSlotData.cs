using System;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public sealed class SkillSlotData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;

        public SkillHashId _skillHashId;
        //public readonly int _slotIndex;
        public void OnBeforeSerialize() => name = $"{_skillHashId.ToDescription()}";
        public void OnAfterDeserialize() => name = $"{_skillHashId.ToDescription()}";
    }
}