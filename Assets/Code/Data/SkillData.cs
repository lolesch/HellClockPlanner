using System;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public sealed class SkillData : ISerializationCallbackReceiver
    {
        [HideInInspector]public string name;
        
        public SkillHashId Id;
        //public Sprite Icon;
        public string Name;
        [TextArea(3,5)]public string Description;
        public int ManaCost;
        public float Cooldown;
        public int BaseDamage;
        public int GlobalBuff;
        //public SkillTag[] Tags;
        //public int[] AffectingRelics;
        //TODO: implement skill effects
        //public string SkillEffect;

        public void OnBeforeSerialize() => name = Id.ToDescription();
        public void OnAfterDeserialize() => name = Id.ToDescription();
        //[ContextMenu("RetrieveIcon")]private void RetrieveIcon() => Icon = Resources.Load<Sprite>( Id.ToString() );
    }

    public sealed class Skill
    {
        public int Rank;
        //public bool assigned;
    }
}