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
        private ISlotIndexProvider _slot;

        private void Start()
        {
            _slot ??= GetComponentInParent<ISlotIndexProvider>( true );
            SetDropdownOptions();
        }


        private void OnEnable() => dropdown.onValueChanged.AddListener( delegate { OnSkillChanged( dropdown ); } );

        private void OnDisable() => dropdown.onValueChanged.RemoveListener( delegate { OnSkillChanged( dropdown ); } );

        private void OnSkillChanged( TMP_Dropdown change )
        {
            var skillId = ( Enum.GetValues( typeof( SkillId ) ) as SkillId[] )!
                .First( x => x.ToDescription() == change.options[change.value].text );
            
            GameState.Player.SetSkillIdAtSlotIndex( _slot.Index, skillId );
        }
        
        private void SetDropdownOptions()
        {
            dropdown.ClearOptions();
            dropdown.options.Add( DataProvider.Instance.defaultOption );
            dropdown.AddOptions( DataProvider.Instance.GetSkillImports()
                .Select( x => new TMP_Dropdown.OptionData( x.skillId.ToDescription(), x.icon, Color.white ) ).ToList() );
        }
    }
}