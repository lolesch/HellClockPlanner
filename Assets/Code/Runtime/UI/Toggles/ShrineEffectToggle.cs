using Code.Runtime.Statistics;
using UnityEngine;

namespace Code.Runtime.UI.Toggles
{
    public sealed class ShrineEffectToggle : AbstractToggle
    {
        [SerializeField] private StatModifier[] modifiers = new StatModifier[1];
        // TODO: populate the modifiers with importData
        
        protected override void Toggle( bool on )
        {
            if( on )
                foreach( var modifier in modifiers )
                    GameState.Player.GetStat( modifier.stat ).AddModifier( modifier.modifier );
            else
                foreach( var modifier in modifiers )
                    GameState.Player.GetStat( modifier.stat ).TryRemoveModifier( modifier.modifier );
        }
    }
}