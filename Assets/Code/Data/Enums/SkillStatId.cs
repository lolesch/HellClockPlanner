using System.ComponentModel;

namespace Code.Data.Enums
{
    public enum SkillStatId : byte
    {
        None = 0,
        
        Damage,
        CriticalHitChance,
        CriticalHitDamage,
        
        ManaCost,
        Cooldown,
        SkillSpeed,
        AreaOfEffect,
        Duration,
        
        ProjectileAmount,
        ProjectileBounces,
        ProjectileSpeed,
        
        DashDistance,
        FireLightningResistShred,
        PhysicalPlagueResistShred,
        SlowDebuffIntensity,
        
    }
}