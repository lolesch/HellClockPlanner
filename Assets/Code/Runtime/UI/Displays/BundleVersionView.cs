using Code.Utility;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    [RequireComponent( typeof( TextMeshProUGUI ) )]
    internal sealed class BundleVersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI versionText;

        private void Start() => SetVersionText();
        
        [ContextMenu( "RefreshVersionText" )]
        private void SetVersionText() => versionText.text = BundleVersionSetter.GetDisplayString();
    }
}