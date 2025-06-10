using System;
using System.Collections.Generic;
using ZLinq;
using System.Text;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.UI.Toggles
{
    public sealed class ShrineEffectRadioToggle : AbstractRadioToggle, IModifierSource
    {
        public Guid guid { get; } = Guid.NewGuid();
        [SerializeField] private ShrineId shrineId;
        [SerializeField] private TooltipHolder tooltipHolder;
        private IEnumerable<CharacterStatModifier> _modifiers;

        protected override void Awake()
        {
            base.Awake();

            var imports = DataProvider.Instance.GetShrineImports();
            _modifiers = imports
                .AsValueEnumerable()
                .Where( x => x.shrineId == shrineId )
                .Select( x => new CharacterStatModifier( x.statId, new Modifier( x.amount, guid )))
                .AsEnumerable();
            
            // TODO: replace with buffDisplay
            var sb = new StringBuilder();
            sb.AppendLine( $"<smallcaps>Shrine of {shrineId.ToDescription()}</smallcaps>" );
            foreach( var modifier in _modifiers )
                sb.AppendLine( modifier.stat.ToDescription() + $" {modifier.modifier}".Styled( "GreenText" ) );
            
            tooltipHolder.SetTooltipText( sb.ToString() ); 
        }
        
        protected override void Toggle( bool on )
        {
            if( on )
                foreach( var modifier in _modifiers )
                    GameState.Player.GetStat( modifier.stat ).AddModifier( modifier.modifier );
            else
                foreach( var modifier in _modifiers )
                    GameState.Player.GetStat( modifier.stat ).TryRemoveModifier( modifier.modifier );
            
            // TODO: implement buff duration => targetGraphic.DoFillAmount over duration
        }
    }
}