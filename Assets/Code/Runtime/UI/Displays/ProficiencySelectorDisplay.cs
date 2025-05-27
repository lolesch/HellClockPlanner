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
    public sealed class ProficiencySelectorDisplay : IndexDependentDisplay
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private TooltipHolder tooltipHolder;
        [SerializeField] private int proficiencySlotIndex;
        [SerializeField] private RarityId rarity;

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
            
            var skillDependentProficiencies =
                DataProvider.Instance.GetSkillProficiencies( _skill.skillId, rarity );
                
            dropdown.ClearOptions();
            dropdown.options.Add(DataProvider.Instance.defaultOption);
            dropdown.AddOptions(  skillDependentProficiencies
                .Select( x => new TMP_Dropdown.OptionData( x.modDescription, x.icon, Color.white ) )
                .ToList() );
            dropdown.value = 0;
        }

        private void OnProficiencyChanged( TMP_Dropdown change )
        {
            var proficiency = 0 < change.value 
                ? DataProvider.Instance.GetSkillProficiencies( _skill.skillId, rarity ).ToArray()[change.value - 1]
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
