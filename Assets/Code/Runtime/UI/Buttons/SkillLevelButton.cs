using Code.Data;
using Code.Data.Enums;
using Code.Runtime.UI.Displays;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Buttons
{
    public sealed class SkillLevelButton : AbstractButton
    {
        private Skill _skill;
        [SerializeField] private bool isIncrement;
        [SerializeField] private TextMeshProUGUI levelText;
        
        private ISlotIndexProvider _slot;

        protected override void OnEnable()
        {
            base.OnEnable();
            GameState.Player.OnSkillSlotsChanged += OnSkillSlotsChanged;
            RefreshDisplay();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameState.Player.OnSkillSlotsChanged -= OnSkillSlotsChanged;
        }

        protected override void OnLeftClick()
        {
            _skill?.ChangeLevel( isIncrement ? 1 : -1 );
            OnDeselect( null );
        }
        
        protected override void OnRightClick() {}

        private void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            if( _skill != null )
                _skill.OnLevelChanged -= OnLevelChanged;
            
            _slot ??= GetComponentInParent<ISlotIndexProvider>( true );
            _skill = GameState.Player.skills[ _slot.index ];
            
            if( _skill != null )
                _skill.OnLevelChanged += OnLevelChanged;
            
            RefreshDisplay();
        }

        private void OnLevelChanged( int level ) => RefreshDisplay(); 
        
        private void RefreshDisplay()
        {
            interactable = isIncrement 
                ? _skill?.level < Const.MaxSkillLevel 
                : _skill?.level > 0;
            
            DoStateTransition(interactable ? SelectionState.Normal : SelectionState.Disabled, false);
            
            if( levelText.text == (_skill?.level + 1).ToString() )
                return;
            
            levelText.text = (_skill?.level + 1).ToString();
            levelText.DoPunch();
        }
    }
}