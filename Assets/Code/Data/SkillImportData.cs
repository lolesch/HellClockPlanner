using System;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct SkillImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillId Id;
        //[TextArea(3,5)] public string Description;
        [ReadOnly] public int ManaCost;
        [ReadOnly] public float Cooldown;
        [ReadOnly] public int BaseDamage;

        public void OnBeforeSerialize() => name = Id.ToString();
        public void OnAfterDeserialize() => name = Id.ToString();
    }
}