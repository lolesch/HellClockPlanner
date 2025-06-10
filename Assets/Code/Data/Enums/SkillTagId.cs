using System.ComponentModel;

namespace Code.Data.Enums
{
    public enum SkillTagId : byte
    {
        None = 0,
        
        // Skill Definitions
        Marksman,
        Melee,
        Enchantment,
        Spell,
        
        SingleTarget,
        AreaOfEffect,
        Summon,
        Prayer,
        Mobility,
        
        Projectile,
        Attack,
        
        Passive,
        ElementalDamage,
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