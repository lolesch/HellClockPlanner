using System.ComponentModel;

namespace Code.Utility.Tools.Statistics
{
    public enum StatType : byte
    {
        None = 0,
        
        RawDamage,
        
        PhysicalDamage,
        FireDamage,
        LightningDamage,
        PlagueDamage,
        
        MaxLife,
    }

    public enum ResourceType : byte
    {
        [Description( "Current Health" )] Health,
        [Description( "Current Mana" )] Mana,
    }

    public enum CurrencyType : byte
    {
        [Description( "Gold" )] Gold,
        [Description( "SoulStones" )] SoulStones,
    }
}