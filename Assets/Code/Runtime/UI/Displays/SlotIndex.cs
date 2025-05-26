using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SlotIndex : MonoBehaviour, ISlotIndexProvider
    {
        [field: SerializeField] public int index { get; private set; }

        private void OnValidate() => index = transform.GetSiblingIndex();

        private void Awake() => index = transform.GetSiblingIndex();
    }
}