namespace Code.Data.Enums
{
    public enum StatId : byte
    {   //                                      IsPlayerStat     willClamp
        // ? = 55, summon related
        // ? = 56, summon related
        
        // Player Stats
        ExperienceGain = 9,                     // true             false
        ItemFind = 11,                          // true             false
        SoulStoneGain = 22,                     // true             false
        GoldGain = 40,                          // true             false
        BlessedGearDropChanceBonus = 23,        // true             false
        TrinketDropChanceBonus = 51,            // true             false
        RelicDropChanceBonus = 52,              // true             false
        BonusRelicDropsFromBoss = 24,           // true             false
        
        SummonLifePercentage = 53,              // true             false
        SummonDamagePercentage = 54,            // true             false
        
        // Avoidance
        MovementSpeed = 6,                      // false            false
        Evasion = 74,                           // false            true
        Endurance = 78, // physical evasion     // false            true
        AntiMagic = 79, // magic evasion        // false            true

        ImmuneToPhysicalDamage = 88,            // false            true
        ImmuneToFireDamage = 89,                // false            true
        ImmuneToPlagueDamage = 90,              // false            true
        ImmuneToLightningDamage = 91,           // false            true

        // Mitigation
        Life = 3,                               // false            false
        AdditionalChanceToReceiveCritical = 34, // false            false

        ManaSpentBarrierRegen = 41,             // true             false
        BarrierDecayThreshold = 43,             // true             false
        BarrierDecayResilience = 45,            // true             false
        BarrierGeneration = 42,                 // false            false
        BarrierGain = 44,                       // false            false
        
        #region Resistances
        PhysicalResistance = 4,                 // false            true
        MagicResistance = 5,                    // false            true
        FireResistance = 25,                    // false            true
        LightningResistance = 26,               // false            true
        PlagueResistance = 27,                  // false            true
        
        DamageOverTimeResistance = 35,          // false            true
        StunResistance = 36,                    // false            true
        CrowdControlResistance = 37,            // false            true
        AilmentResistance = 71,                 // false            false
        WitherResistance = 73,                  // false            true
        DamageResistance = 77,                  // false            true
        #endregion Resistances
        
        // Recovery
        DamageLifeRegen = 38,                   // false            false
        LifeRegen = 28,                         // false            false
        LifeGain = 86,                          // false            false

        #region Potions
        ChanceToFindPotion = 46,                // true             false
        PotionCapacity = 47,                    // true             false
        PotionLifePercentageRegen = 48,         // true             false
        PotionLifeFlatRegen = 49,               // true             false
        PotionEfficiency = 50,                  // true             false
        #endregion Potions
        
        // Offensive Stats
        BaseDamage = 0,                         // false            false
        Damage = 12,                            // false            false
        MagicDamage = 13,                       // false            false
        
        AdditionalPhysicalDamage = 14,          // false            false
        PhysicalDamage = 15,                    // false            false
        AdditionalFireDamage = 16,              // false            false
        FireDamage = 17,                        // false            false
        AdditionalPlagueDamage = 18,            // false            false
        PlagueDamage = 19,                      // false            false
        AdditionalLightningDamage = 20,         // false            false
        LightningDamage = 21,                   // false            false
        
        CriticalChance = 1,                     // false            false
        CriticalDamage = 2,                     // false            false
        AttackSpeed = 7,                        // false            false
        ChanceToMiss = 80,                      // false            true
        CooldownSpeed = 8,                      // false            false
        
        Mana = 10,                              // false            false
        ManaRegen = 29,                         // false            false
        DamageManaRegen = 39,                   // false            false
        ManaGain = 87,                          // false            false
        SkillManaCost = 57,                     // true             false
        ManaShield = 76,                        // true             true

        BuffDuration = 92,                      // false            false

        #region Ailments
        AilmentApplication = 81,                // false            false
        ElementalAilmentDuration = 84,          // false            false
        AilmentDuration = 58,                   // false            false
        AilmentDamage = 65,                     // false            false
        
        ChanceToApplyBleed = 30,                // false            false
        BleedDuration = 59,                     // false            true
        BleedDamage = 72,                       // false            false
        
        ChanceToApplyIgnite = 31,               // false            false
        IgniteDuration = 60,                    // false            true
        IgniteDamage = 66,                      // false            false
        
        ChanceToApplyShock = 32,                // false            false
        ShockDuration = 61,                     // false            true
        ShockIntensity = 67,                    // false            false
        
        ChanceToApplyBlight = 33,               // false            false
        BlightDuration = 62,                    // false            true
        BlightIntensity = 68,                   // false            false
        
        WitherDuration = 63,                    // false            true
        WitherBuildupRate = 69,                 // false            true
        
        StunDuration = 64,                      // false            true
        StunBuildupRate = 70,                   // false            true
        #endregion Ailments
        
        SkillAreaOfEffect = 75,                 // false            false
        SkillProjectileAmount = 82,             // false            false
        SkillMeleeDamage = 83,                  // false            false
        SkillSpellDamage = 85,                  // false            false
    }
    
    public enum ShrineId : byte
    {
        None = 0,
        Learning,
        Aptitude,
        Speed,
        Wealth,
        Protection,
        Power,
    }
}