using System;
using System.Linq;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillSelectorDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private int slotIndex;

        private void Start() => SetDropdownOptions();

        private void OnEnable() => dropdown.onValueChanged.AddListener( delegate { OnSkillChanged( dropdown ); });
        private void OnDisable() => dropdown.onValueChanged.RemoveListener(delegate { OnSkillChanged(dropdown); });

        private void OnSkillChanged( TMP_Dropdown change )
        {
            var skillId = ( Enum.GetValues( typeof( SkillId ) ) as SkillId[] )!
                .First( x => x.ToDescription() == change.options[change.value].text );
            
            GameState.Player.SetSkillIdAtSlotIndex( slotIndex, skillId );
            //SetDropdownOptions();
        }
        
        private void SetDropdownOptions()
        {
            dropdown.ClearOptions();
            dropdown.options.Add(DataProvider.Instance.GetDefaultDropdownOption());
            dropdown.AddOptions( DataProvider.Instance.GetSkills()
                .Select( x => new TMP_Dropdown.OptionData( x.id.ToDescription(), x.icon, Color.white ) ).ToList() );

            dropdown.value = dropdown.options.FindIndex( x => x.text == GameState.Player.SkillSlots[slotIndex]._skillHashId.ToDescription() );
        }
    }
}