using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Buttons
{
    public sealed class SkillLevelButton : AbstractButton
    {
        private Skill _skill;
        [SerializeField] private bool isIncrement;
        [SerializeField] private TextMeshProUGUI levelText;
        
        protected override void OnLeftClick()
        {
            _skill.ChangeLevel( isIncrement ? 1 : -1 );
            RefreshInteractability( _skill.level );
            OnDeselect( null );
        }

        protected override void OnRightClick() {}
        
        private void RefreshInteractability( int level = 1 )
        {
            var canInteract = isIncrement 
                ? _skill.level < _skill.MaxLevel 
                : _skill.level > 1;

            interactable = canInteract;
            levelText.text = level.ToString();
            
            DoStateTransition(canInteract ? SelectionState.Normal : SelectionState.Disabled, false);
        }

        public void SetSkill( Skill skill )
        {
            if( _skill != null )
                _skill.OnLevelChanged -= RefreshInteractability;
            
            _skill = skill;
            _skill.OnLevelChanged += RefreshInteractability;
            
            RefreshInteractability( _skill.level );
        }
    }
}