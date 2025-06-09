using System;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.Imports
{
    [Serializable]
    public struct CharacterStatImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;

        [ReadOnly] public CharacterStatId characterStatId;
        [ReadOnly] public float baseValue;
        [ReadOnly] public ModType modType;

        public void OnBeforeSerialize() => name = $"{characterStatId.ToDescription()} - {GetValueString()}";
        public void OnAfterDeserialize() {}

        private string GetValueString()
        {
            return modType switch
            {
                ModType.Flat => $"{baseValue:0.##}",
                ModType.Percent => $"{baseValue:0.##}%",
                _ => baseValue.ToString(),
            };
        }
    }
}