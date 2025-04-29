using Code.Data.Enums;
using Code.Utility.AttributeRefs;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu(fileName = "SkillData", menuName = "ScriptableObjects/SkillData")]
    public sealed class SkillData : ScriptableObject
    {
        public SkillHashId _skillHashId;
        [PreviewIcon] public Sprite Icon;
        public int manaCost;
        public float cooldown;
        public SkillTag[] tags;
        public int BaseDamage;
        [TextArea(3,5)]public string Description;
        public int globalBuff;
        public int[] affectingRelics;
        //TODO: implement skill effects
        public string skillEffect;
        
        public string Name => _skillHashId.ToDescription();
    }

    public sealed class Skill
    {
        public int rank;
        //public bool assigned;
    }

    public enum SkillTag
    {
        FireDamage,
        LightningDamage,
        PhysicalDamage,
        PlagueDamage,
        
        SingleTarget,
        AreaOfEffect,
        Projectile,
        
        Marksman,
        Melee,
        
        Mobility,
        Spell,
        Enhancement,
        Passive,
    }
}