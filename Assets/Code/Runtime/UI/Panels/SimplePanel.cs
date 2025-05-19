using UnityEngine;

namespace Code.Runtime.UI.Panels
{
    public sealed class SimplePanel : AbstractPanel
    {
        [SerializeField] private bool startVisible = false;
        private void OnEnable()
        {
            if( startVisible )
                FadeIn();
        }
    }
}