using System;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public sealed class SkillData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        public SkillHashId Id;
        //[TextArea(3,5)] public string Description;
        //public int ManaCost;
        //public float Cooldown;
        //public int BaseDamage;
        //public CharacterStatId GlobalBuff;
        public Sprite Icon;

        //[SerializeField] private SkillTag[] Tags;
        //[SerializeField] private int[] AffectingRelics;
        //TODO: implement skill effects
        //[SerializeField] private string SkillEffect;
        public void OnBeforeSerialize() => name = Id.ToDescription();
        public void OnAfterDeserialize() => name = Id.ToDescription();
    }

    public sealed class Skill
    {
        public int Rank;
        //public bool assigned;
    }
}