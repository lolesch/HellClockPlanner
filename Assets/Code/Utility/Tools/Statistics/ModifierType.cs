using System.ComponentModel;
using UnityEngine;

namespace Code.Utility.Tools.Statistics
{
    public enum ModifierType : short
    {
        // WORDING
        // how to consistently translate the modifier types
        // - Overwrite   => absolute, explicit, fix
        // - FlatAdd     => additional, additive, bonus, 
        // - PercentAdd  => more/less
        // - PercentMult => multiplicative

        /// Values are the order the modifiers are applied
        [Tooltip( "Sets the stat to a fixed value" )]
        Overwrite = -1,

        [Tooltip( "Adds a flat value to the stat" )]
        [Description( "Flat Add" )]
        FlatAdd = 100,

        [Tooltip( "Adds a percentage to the stat" )]
        [Description( "Percent Add" )]
        PercentAdd = 200,

        [Tooltip( "Multiplies the total by a percentage" )]
        PercentMult = 300,
    }
}