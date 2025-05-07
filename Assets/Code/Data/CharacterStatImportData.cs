using System;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct CharacterStatImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public CharacterStatId Id;
        [ReadOnly] public float BaseValue;
        
        private string ModType;
        // TODO: replace with statistics.modType once implemented
        [SerializeField][ReadOnly] private StatModType modType;

        public void OnBeforeSerialize()
        {
            name = Id.ToString();
            if( modType == StatModType.None)
                modType = (StatModType) ModType.ToEnum<StatModType>();
        }

        public void OnAfterDeserialize()
        {
            name = Id.ToString();
            if( modType == StatModType.None)
                modType = (StatModType) ModType.ToEnum<StatModType>();
        }
        
        // TODO: replace with statistics.modType once implemented
        private enum StatModType
        {
            None,
            FlatAdd,
            PercentAdd
        }
    }
}