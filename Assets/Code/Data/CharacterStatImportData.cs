using System;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using Code.Utility.Tools.Statistics;
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
        [SerializeField][ReadOnly] public ModifierType modType;

        public void OnBeforeSerialize()
        {
            name = Id.ToString();
            if( modType == 0)
                modType = (ModifierType) ModType.ToEnum<ModifierType>();
        }

        public void OnAfterDeserialize()
        {
            name = Id.ToString();
            if( modType == 0)
                modType = (ModifierType) ModType.ToEnum<ModifierType>();
        }
    }
}