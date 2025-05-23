using System.ComponentModel;

namespace Code.Data.Enums
{
    public enum SkillStatId : byte
    {
        None = 0,
        
        Damage,
        CriticalHitChance,
        CriticalHitDamage,
        
        [Description("Mana Cost")]
        ManaCostReduction,
        [Description("Cooldown")]
        CooldownReduction,
        SkillSpeed,
        [Description("Area of Effect")]
        EffectArea,
        [Description("Increased Duration")]
        EffectDuration,
        
        ProjectileAmount,
        ProjectileBounces,
    }
}