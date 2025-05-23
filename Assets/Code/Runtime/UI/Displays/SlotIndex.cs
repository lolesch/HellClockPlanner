using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SlotIndex : MonoBehaviour, ISlotIndexProvider
    {
        [field: SerializeField] public int Index { get; private set; }

        private void OnValidate() => Index = transform.GetSiblingIndex();

        private void Start() => Index = transform.GetSiblingIndex();
    }
}