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
            foreach( var slot in skillSlots )
                if( slot._slotIndex == slotIndex )
                {
                    var sprite = DataProvider.Instance.GetIconFromSkillId( slot._skillHashId );
                    icon.sprite = sprite;
                    icon.enabled = sprite != null;
                    //icon.color = slot._skillHashId == SkillId.None ? Color.clear : Color.white;
                }
        }
    }
}