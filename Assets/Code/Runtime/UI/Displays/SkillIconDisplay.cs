using Code.Data;
using Code.Runtime.Provider;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillIconDisplay : IndexDependentDisplay
    {
        [SerializeField] private Image icon;

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            var sprite = DataProvider.Instance.GetIconFromSkillId( skillSlots[slot.index]._skillHashId );
            
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