using System;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public struct SkillImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        [ReadOnly] public SkillId id;
        [ReadOnly] [TextArea(3,5)] public string description;
        [ReadOnly] public int manaCost;
        [ReadOnly] public float cooldown;
        [ReadOnly] public int baseDamage;
        [ReadOnly] public Sprite icon => DataProvider.Instance.GetIconFromSkillId( id );

        public void OnBeforeSerialize() => name = id.ToString();
        public void OnAfterDeserialize() => name = id.ToString();
    }
}