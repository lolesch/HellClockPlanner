using System;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Data.Imports;
using Code.Runtime.Provider;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class ProficiencySelectorDisplay : IndexDependentDisplay
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private TooltipHolder tooltipHolder;
        [SerializeField] private int proficiencySlotIndex;
        [SerializeField] private RarityId rarityId;

        private Skill _skill;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            dropdown.onValueChanged.AddListener( delegate { OnProficiencyChanged( dropdown ); });
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            dropdown.onValueChanged.RemoveListener(delegate { OnProficiencyChanged(dropdown); });
        }

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            if( _skill == GameState.Player.skills[slot.index] ) 
                return;
                
            _skill = GameState.Player.skills[slot.index];

            var skillDependentProficiencies = _skill != null
                ? DataProvider.Instance.GetSkillProficiencies( _skill.skillTypeId, rarityId )
                : Enumerable.Empty<Proficiency>();
            var hasProficiencies = skillDependentProficiencies.Any();
            
            dropdown.interactable = hasProficiencies;
            dropdown.captionImage.color = hasProficiencies ? Color.white : Color.clear;
                
            dropdown.ClearOptions();
            dropdown.options.Add(DataProvider.Instance.defaultOption);
            
            if( hasProficiencies )
                dropdown.AddOptions(  skillDependentProficiencies
                    .Select( x => new TMP_Dropdown.OptionData( x.modDescription, x.icon, Color.white ) )
                    .ToList() );
            
            dropdown.value = 0;
        }

        private void OnProficiencyChanged( TMP_Dropdown change )
        {
            var proficiency = 0 < change.value 
                ? DataProvider.Instance.GetSkillProficiencies( _skill.skillTypeId, rarityId ).ToArray()[change.value - 1]
                : new Proficiency();
            
            GameState.Player.skills[slot.index].AddProficiency( proficiency, proficiencySlotIndex );
        
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
