using System;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct GlobalBuffImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillId id;
        private string _globalBuff;
        [ReadOnly] public CharacterStatId globalBuff;
        [ReadOnly] public float amountPerRank;

        public void OnBeforeSerialize()
        {
            if( globalBuff == CharacterStatId.None)
                globalBuff = (CharacterStatId) _globalBuff.ToEnum<CharacterStatId>();
            
            name = $"{id} - {amountPerRank} {globalBuff}";
        }

        public void OnAfterDeserialize() {}
    }
}