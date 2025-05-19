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

        [ReadOnly] public CharacterStatId id;
        [ReadOnly] public float baseValue;
        [ReadOnly] public ModType modType;

        public void OnBeforeSerialize()
        {
            name = $"{id} - {GetValueString()}";
        }

        public void OnAfterDeserialize()
        {
        }

        private string GetValueString()
        {
            return modType switch
            {
                ModType.Flat => $"{baseValue:0.##}",
                ModType.Percent => $"{baseValue * 100:0.##}%",
                _ => baseValue.ToString(),
            };
        }
    }
}