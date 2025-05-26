using System.ComponentModel;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.Enums
{
    public enum SkillTagId : byte
    {
        None = 0,
        
        //FireDamage,
        //LightningDamage,
        //PhysicalDamage,
        //PlagueDamage,
        ElementalDamage,
        
        SingleTarget,
        AreaOfEffect,
        Projectile,
        
        Marksman,
        Melee,
        
        Mobility,
        Enchantment,
        Spell,
        Summon,
        
        Passive,
    }
    
    public enum DamageTypeId : byte
    {
        None = 0,
        [Description("Physical Damage")]
        Physical,
        [Description("Fire Damage")]
        Fire,
        [Description("Lightning Damage")]
        Lightning,
        [Description("Plague Damage")]
        Plague,
        //[Description("Elemental Damage")]
        //Elemental,
    }
}