namespace Code.Data.Enums
{
    public enum ProficiencyId : byte
    {
        None = 0,
        
        //All
        Damage,
        
        //SplitShot
        CriticalHitChance,
        ManaCost,
        ProjectileAmount,
        SkillSpeed,

        //Reflexes
        AreaOfEffect,
        IncreasedDuration,
        KnifeGenerationTime,
        MaxKnives,
        KnifeBounceAmount,
    }
}