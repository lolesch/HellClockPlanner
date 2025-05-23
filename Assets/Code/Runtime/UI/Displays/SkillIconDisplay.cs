using Code.Data;
using Code.Runtime.Provider;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillIconDisplay : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private int slotIndex;

        private void OnEnable() => GameState.Player.OnSkillSlotsChanged += OnSkillSlotsChanged;
        private void OnDisable() => GameState.Player.OnSkillSlotsChanged -= OnSkillSlotsChanged;

        private void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            var sprite = DataProvider.Instance.GetIconFromSkillId( skillSlots[slotIndex]._skillHashId );
            
            icon.sprite = sprite;
            icon.enabled = sprite != null;
        }
    }
}