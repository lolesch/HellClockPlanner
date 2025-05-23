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
    public sealed class ProficiencySelectorDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private TooltipHolder tooltipHolder;
        [SerializeField] private int proficiencySlotIndex;
        [SerializeField] private RarityId rarity;
        private ISlotIndexProvider _slot;

        private void Start() => _slot ??= GetComponentInParent<ISlotIndexProvider>( true );

        private void OnEnable()
        {
            GameState.Player.OnSkillSlotsChanged += SetDropdownOptions;
            dropdown.onValueChanged.AddListener( delegate { OnProficiencyChanged( dropdown ); });
        }

        private void OnDisable()
        {
            GameState.Player.OnSkillSlotsChanged -= SetDropdownOptions;
            dropdown.onValueChanged.RemoveListener(delegate { OnProficiencyChanged(dropdown); });
        }

        private void SetDropdownOptions( SkillSlotData[] skillSlots )
        {
            foreach( var slot in skillSlots )
                if( slot._slotIndex == _slot.Index )
                {
                    var skillDependentProficiencies =
                        DataProvider.Instance.GetSkillProficiencies( slot._skillHashId, rarity );
                    
                    dropdown.ClearOptions();
                    dropdown.options.Add(DataProvider.Instance.defaultOption);
                    dropdown.AddOptions(  skillDependentProficiencies
                        .Select( x => new TMP_Dropdown.OptionData( x.modDescription, x.icon, Color.white ) )
                        .ToList() );
                }
            dropdown.value = 0;
        }

        private void OnProficiencyChanged( TMP_Dropdown change )
        {
            var skillId = GameState.Player.SkillSlots[_slot.Index]._skillHashId;
            var optionIndex = change.value - 1;
                
            var proficiency = new Proficiency();
            if( 0 < optionIndex ) 
                proficiency = DataProvider.Instance.GetSkillProficiencies( skillId, rarity ).ToArray()[optionIndex];
            
            GameState.Player.SetProficiencyAtSlotIndex( _slot.Index, proficiency, proficiencySlotIndex );
        
            SetTooltip( proficiency );
        }
        
        public void SetTooltip( Proficiency proficiency )
        {
            string valueString = proficiency.modType switch
            {
                ModType.Flat => $"{proficiency.value:+0.##;-0.##}",
                ModType.Percent => $"{proficiency.value:+0.##;-0.##}%",
                _ => proficiency.value.ToString()
            };

            tooltipHolder.SetTooltipText( $"{proficiency.modDescription} {valueString.Colored( Color.green)}" );
        } 
    }
}
