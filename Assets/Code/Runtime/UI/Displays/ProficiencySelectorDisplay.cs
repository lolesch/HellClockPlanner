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
        [SerializeField] private int slotIndex;
        [SerializeField] private RarityId rarity;

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
            // remove previous global buff mods
            foreach( var slot in skillSlots )
                if( slot._slotIndex == slotIndex )
                {
                    var skillDependentProficiencies =
                        DataProvider.Instance.GetDropdownProficiencies( slot._skillHashId, rarity );
                    
                    dropdown.options = DataProvider.Instance.proficiencyIconList
                        .Where( x => x.proficiencyId == ProficiencyId.None || skillDependentProficiencies.Select( y => y.proficiency ).Contains( x.proficiencyId ) )
                        .Select( x => new TMP_Dropdown.OptionData( x.proficiencyId.ToDescription(), x.icon, Color.white ) )
                        .ToList();
                }
        }

        private void OnProficiencyChanged( TMP_Dropdown change )
        {
            var proficiencyId = ( Enum.GetValues( typeof( ProficiencyId ) ) as ProficiencyId[] )!
                .First( x => x.ToDescription() == change.options[change.value].text );
            
            GameState.Player.SetProficiencyAtSlotIndex( slotIndex, proficiencyId );
        } 
        
    }
}