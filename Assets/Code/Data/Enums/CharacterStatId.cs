using System.ComponentModel;
using JetBrains.Annotations;

namespace Code.Data.Enums
{
    public enum CharacterStatId : byte
    {
        None = 0,
        
        BaseDamage = 1,
        Damage = 2,
        ElementalDamage = 3,
        
        FireDamage = 4,
        AddedFireDamage = 5,
        [Description("Chance to apply Ignite on Fire Damage")]
        ChanceToIgnite = 6,
        IgnitedStatusIntensity = 7,
        
        LightningDamage = 8,
        AddedLightningDamage = 9,
        [Description("Chance to apply Shock on Lightning Damage")]
        ChanceToShock = 10,
        ShockedStatusIntensity = 11,
        
        PhysicalDamage = 12,
        [Description("Added Phys. Damage")]
        AddedPhysicalDamage = 13,
        [Description("Chance to apply Bleed on Physical Damage")]
        ChanceToBleed = 14,
        BleedingStatusIntensity = 15,
        
        PlagueDamage = 16,
        AddedPlagueDamage = 17,
        [Description("Chance to apply Blight on Plague Damage")]
        ChanceToBlight = 18,
        BlightedStatusIntensity = 19,
        
        CriticalHitChance = 20,
        CriticalHitChanceMultiplier = 21,
        CriticalHitDamage = 22,
        CriticalHitDamageMultiplier = 23,
        
        [Description("Move Speed")]
        MovementSpeed = 24,
        AttackSpeed = 25,
        CooldownSpeed = 26,
        SkillManaCost = 27,
        
        ExperienceGain = 28,
        GoldGain = 29,
        SoulStoneGain = 30,
        
        ItemFind = 31,
        BonusGearDropChance = 32,
        BonusRelicDropChance = 33,
        BonusTrinketDropChance = 34,
        
        [Description("Conv. Decay Resistance")]
        ConvictionDecayResistance = 35,
        [Description("Conv. Decay Threshold")]
        ConvictionDecayThreshold = 36,
        [Description("Conv. From Mana Spent")]
        ConvictionFromManaSpent = 37,
        ConvictionGain = 38,
        ConvictionGeneration = 39,
        
        ElementalResistance = 40,
        FireResistance = 41,
        LightningResistance = 42,
        PhysicalResistance = 43,
        PlagueResistance = 44,
        
        Life = 45,
        LifePercent = 46,
        LifeOnKill = 47,
        LifeRegeneration = 48,
        
        Mana = 49,
        ManaPercent = 50,
        ManaOnKill = 51,
        ManaRegeneration = 52,
        
        PotionCapacity = 53,
        PotionEfficiency = 54,
        
        Evasion = 55,
        AilmentDuration = 56,
        SkillAreaOfEffect = 57,
        WitherBuildupRate = 58,
        StunBuildupRate = 59,
        CrowdControlResistance = 60,
    }
}