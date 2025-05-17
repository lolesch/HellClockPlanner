using Code.Utility;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    [RequireComponent( typeof( TextMeshProUGUI ) )]
    internal sealed class BundleVersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI versionText;
#if UNITY_EDITOR
        private void OnValidate() => SetVersionText();
        
        [ContextMenu( "RefreshVersionText" )]
        private void SetVersionText() => versionText.text = BundleVersionSetter.GetDisplayString();
#endif
    }
}