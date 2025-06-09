using Code.Data;
using Code.Runtime.Provider;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillIconDisplay : IndexDependentDisplay
    {
        [SerializeField] private Image icon;

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            var skill = GameState.Player.skills[slot.index];
            
            var sprite = skill != null ? skill.icon : DataProvider.Instance.defaultOption.image;
            
            if( icon.sprite == sprite ) 
                return;
            
            icon.sprite = sprite;
            icon.enabled = sprite != null;
            icon.DoPunch();
        }
    }
    
    public interface ISlotIndexProvider
    {
        int index { get; }
    }
}