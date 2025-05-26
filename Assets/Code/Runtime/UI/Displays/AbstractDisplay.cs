using Code.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    [RequireComponent( typeof(GraphicRaycaster), typeof(RectTransform))]
    public abstract class AbstractDisplay : MonoBehaviour {}

    public abstract class IndexDependentDisplay : AbstractDisplay
    {
        protected ISlotIndexProvider slot { get; private set; }

        protected virtual void Awake() => slot ??= GetComponentInParent<ISlotIndexProvider>( true );
        
        protected virtual void OnEnable() => GameState.Player.OnSkillSlotsChanged += OnSkillSlotsChanged;
        protected virtual void OnDisable() => GameState.Player.OnSkillSlotsChanged -= OnSkillSlotsChanged;

        protected abstract void OnSkillSlotsChanged( SkillSlotData[] skillSlots );
    }
}