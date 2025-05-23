using Code.Data;
using Code.Runtime.Provider;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillIconDisplay : MonoBehaviour
    {
        [SerializeField] private Image icon;

        private ISlotIndexProvider _slot;

        private void Start() => _slot ??= GetComponentInParent<ISlotIndexProvider>( true );

        private void OnEnable() => GameState.Player.OnSkillSlotsChanged += OnSkillSlotsChanged;
        private void OnDisable() => GameState.Player.OnSkillSlotsChanged -= OnSkillSlotsChanged;

        private void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            var sprite = DataProvider.Instance.GetIconFromSkillId( skillSlots[_slot.Index]._skillHashId );
            
            icon.sprite = sprite;
            icon.enabled = sprite != null;
        }
    }
    
    public interface ISlotIndexProvider
    {
        int Index { get; }
    }
}