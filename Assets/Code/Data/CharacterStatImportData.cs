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

        public void OnBeforeSerialize() => name = $"{Id} - {BaseValue}";

        public void OnAfterDeserialize() {}
    }
}