using System.ComponentModel;
using JetBrains.Annotations;

namespace Code.Data.Enums
{
    public enum CharacterStatId
    {
        None = 0,
        
        BaseDamage = 1,
        Damage = 2,
        
        FireDamage = 3,
        AddedFireDamage = 4,
        [Description("Chance to apply Ignite on Fire Damage")]
        ChanceToIgnite = 5,
        IgnitedStatusIntensity = 6,
        
        LightningDamage = 7,
        AddedLightningDamage = 8,
        [Description("Chance to apply Shock on Lightning Damage")]
        ChanceToShock = 9,
        ShockedStatusIntensity = 10,
        
        PhysicalDamage = 11,
        AddedPhysicalDamage = 12,
        [Description("Chance to apply Bleed on Physical Damage")]
        ChanceToBleed = 13,
        BleedingStatusIntensity = 14,
        
        PlagueDamage = 15,
        AddedPlagueDamage = 16,
        [Description("Chance to apply Blight on Plague Damage")]
        ChanceToBlight = 17,
        BlightedStatusIntensity = 18,
        
        CriticalHitChance = 19,
        CriticalHitChanceMultiplier = 20,
        CriticalHitDamage = 21,
        CriticalHitDamageMultiplier = 22,
        
        [Description("Movement Speed")]
        MoveSpeed = 23,
        AttackSpeed = 24,
        CooldownSpeed = 25,
        SkillManaCost = 26,
        
        ExperienceGain = 27,
        GoldGain = 28,
        SoulstoneGain = 29,
        
        ItemFind = 30,
        BonusGearDropChance = 31,
        BonusRelicDropChance = 32,
        BonusTrinketDropChance = 33,
        
        ConvictionDecayResistance = 34,
        ConvictionDecayThreshold = 35,
        ConvictionFromManaSpent = 36,
        ConvictionGain = 37,
        ConvictionGeneration = 38,
        
        ElementalResistance = 39,
        FireResistance = 40,
        LightningResistance = 41,
        PhysicalResistance = 42,
        PlagueResistance = 43,
        
        Life = 44,
        LifePercent = 45,
        LifeOnKill = 46,
        LifeRegeneration = 47,
        
        Mana = 48,
        ManaPercent = 49,
        ManaOnKill = 50,
        ManaRegeneration = 51,
        
        PotionCapacity = 52,
        PotionEfficiency = 53,
    }
}