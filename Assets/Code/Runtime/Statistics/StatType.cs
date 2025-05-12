using System.ComponentModel;

namespace Code.Runtime.Statistics
{
    public enum StatType : byte
    {
        #region Damage
        [Description( "Base Damage" )] BaseDamage,
        [Description( "Damage" )] Damage,
        [Description( "Elemental Damage" )] ElementalDamage,
        
        // Fire
        [Description( "Fire Damage" )] FireDamage,
        [Description( "Additional Fire Damage" )] AddedFireDamage,
        [Description( "Chance to apply Ignite on Fire Damage" )] ChanceToIgnite,
        [Description( "Ignited Status Intensity" )] IgnitedStatusIntensity,
        
        // Lightning
        [Description( "Lightning Damage" )] LightningDamage,
        [Description( "Additional Lightning Damage" )] AddedLightningDamage,
        [Description( "Chance to apply Shock on Lightning Damage" )] ChanceToShock,
        [Description( "Shocked Status Intensity" )] ShockedStatusIntensity,
        
        // Physical
        [Description( "Physical Damage" )] PhysicalDamage,
        [Description( "Additional Physical Damage" )] AddedPhysicalDamage,
        [Description( "Chance to apply Bleed on Physical Damage" )] ChanceToBleed,
        [Description( "Bleeding Status Intensity" )] BleedingStatusIntensity,
        
        // Plague
        [Description( "Plague Damage" )] PlagueDamage,
        [Description( "Additional Plague Damage" )] AddedPlagueDamage,
        [Description( "Chance to apply Blight on Plague Damage" )] ChanceToBlight,
        [Description( "Blighted Status Intensity" )] BlightedStatusIntensity,
        
        [Description( "Critical Hit Chance" )] CriticalHitChance,
        [Description( "Critical Hit Chance Multiplier" )] CriticalHitChanceMultiplier,
        [Description( "Critical Hit Damage" )] CriticalHitDamage,
        [Description( "Critical Hit Damage Multiplier" )] CriticalHitDamageMultiplier,
        #endregion Damage
        
        #region Utility
        [Description( "Movement Speed" )] MovementSpeed,
        [Description( "Attack Speed" )] AttackSpeed,
        [Description( "Cooldown Speed" )] CooldownSpeed,
        [Description( "Skill Mana Cost" )] SkillManaCost,
        
        [Description( "Experience Gain" )] ExperienceGain,
        [Description( "Gold Gain" )] GoldGain,
        [Description( "Soul Stone Gain" )] SoulStoneGain,
        
        [Description( "Item Find" )] ItemFind,
        [Description( "Bonus Gear Drop Chance" )] BonusGearDropChance,
        [Description( "Bonus Relic Drop Chance" )] BonusRelicDropChance,
        [Description( "Bonus Trinket Drop Chance" )] BonusTrinketDropChance,
        #endregion Utility
        
        #region Conviction
        [Description( "Conviction Decay Resistance" )] ConvictionDecayResistance,
        [Description( "Conviction Decay Threshold" )] ConvictionDecayThreshold,
        [Description( "Conviction From Mana Spent" )] ConvictionFromManaSpent,
        [Description( "Conviction Gain" )] ConvictionGain,
        [Description( "Conviction Generation" )] ConvictionGeneration,
        #endregion Conviction
        
        #region Resristances
        [Description( "Elemental Resistance" )] ElementalResistance,
        [Description( "Fire Resistance" )] FireResistance,
        [Description( "Lightning Resistance" )] LightningResistance,
        [Description( "Physical Resistance" )] PhysicalResistance,
        [Description( "Plague Resistance" )] PlagueResistance,
        #endregion Resristances
        
        #region Life
        [Description( "Maximum Life" )] MaxLife,
        [Description( "Life Percent" )] LifePercent,
        [Description( "Life On Kill" )] LifeOnKill,
        [Description( "Life Regeneration" )] LifeRegeneration,
        #endregion Life
        
        #region Mana
        [Description( "Maximum Mana" )] MaxMana,
        [Description( "Mana Percent" )] ManaPercent,
        [Description( "Mana On Kill" )] ManaOnKill,
        [Description( "Mana Regeneration" )] ManaRegeneration,
        #endregion Mana
        
        #region Potions
        [Description( "Potion Capacity" )] PotionCapacity,
        [Description( "Potion Efficiency" )] PotionEfficiency,
        #endregion Potions
        
        #region Other
        [Description( "Evasion" )] Evasion,
        [Description( "Ailmant Duration" )] AilmantDuration,
        [Description( "Skill Area of Effect" )] SkillAreaofEffect,
        [Description( "Witherbuilduprate" )] Witherbuilduprate,
        [Description( "Stunbuilduprate" )] Stunbuilduprate,
        [Description( "Crowd Control Resistance" )] CrowdControlResistance,
        #endregion Other
    }

    public enum ResourceType : byte
    {
        [Description( "Current Health" )] Health,
        [Description( "Current Mana" )] Mana,
    }

    public enum CurrencyType : byte
    {
        [Description( "Gold" )] Gold,
        [Description( "Gold" )] SoulStones,
    }

    public enum ScalableAbilityModifierType : byte
    {
        [Description( "ResourceCost" )] ResourceConst,

        //[Description( "CooldownReduction" )] CooldownReduction,
        [Description( "Maximum Charges" )] MaxCharges,
        [Description( "Area of Effect" )] Aoe,
        [Description( "Range" )] Range,
    }
}