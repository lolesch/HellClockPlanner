using System;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillSelectorDisplay : IndexDependentDisplay
    {
        [SerializeField] private TMP_Dropdown dropdown;
        private void Start() => SetDropdownOptions();

        protected override void OnEnable()
        {
            base.OnEnable();
            dropdown.onValueChanged.AddListener( delegate { OnSkillChanged( dropdown ); } );
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            dropdown.onValueChanged.RemoveListener( delegate { OnSkillChanged( dropdown ); } );
        }

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots ) {}

        private void OnSkillChanged( TMP_Dropdown change )
        {
            var skillId = ( Enum.GetValues( typeof( SkillTypeId ) ) as SkillTypeId[] )!
                .First( x => x.ToDescription() == change.options[change.value].text );
            
            GameState.Player.SetSkillIdAtSlotIndex( slot.index, skillId );
            
            dropdown.captionImage.DoPunch();
        }
        
        private void SetDropdownOptions()
        {
            dropdown.ClearOptions();
            dropdown.options.Add( DataProvider.Instance.defaultOption );
            dropdown.AddOptions( DataProvider.Instance.GetSkillDefinitions().Select( x =>
                    new TMP_Dropdown.OptionData( x.type.ToDescription(), x.icon, Color.white ) ).ToList() );
        }
    }
}