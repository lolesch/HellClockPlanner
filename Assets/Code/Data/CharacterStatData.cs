using System;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public sealed class CharacterStatData : ISerializationCallbackReceiver
    {
        // CONTINUE HERE
        [HideInInspector]public string name;
        
        public CharacterStatId Id;

        public void OnBeforeSerialize() => name = Id.ToDescription();
        public void OnAfterDeserialize() => name = Id.ToDescription();
    }
}