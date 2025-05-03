using System;
using Code.Data.Enums;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public sealed class SkillImportData : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        
        public SkillHashId Id;
        //public Sprite Icon;
        //[TextArea(3,5)] public string Description;
        //public int ManaCost;
        //public float Cooldown;
        //public int BaseDamage;
        //public CharacterStatId GlobalBuff;

        //[SerializeField] private SkillTag[] Tags;
        //[SerializeField] private int[] AffectingRelics;
        //TODO: implement skill effects
        //[SerializeField] private string SkillEffect;
        public void OnBeforeSerialize() => name = Id.ToString();
        public void OnAfterDeserialize() => name = Id.ToString();
    }

    public sealed class Skill
    {
        public int Rank;
        //public bool assigned;
    }
}