using System;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using Code.Utility.Tools.Statistics;
using UnityEngine;
using ValueType = Code.Data.Enums.ValueType;

namespace Code.Data
{
    [Serializable]
    public struct CharacterStatImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;

        [ReadOnly] public CharacterStatId id;
        [ReadOnly] public float baseValue;
        [ReadOnly] public ValueType valueType;

        public void OnBeforeSerialize()
        {
            name = $"{id} - {GetValueString()}";
        }

        public void OnAfterDeserialize()
        {
        }

        private string GetValueString()
        {
            return valueType switch
            {
                ValueType.Flat => $"{baseValue:0.##}",
                ValueType.Percent => $"{baseValue:P0}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}