using Code.Runtime.UI.Panels;
using UnityEngine;

namespace Code.Runtime.UI.Toggles
{
    public sealed class ContentToggle : AbstractToggle
    {
        [SerializeField] private AbstractPanel _content;
        protected override void Toggle( bool on )
        {
            if( !_content ) 
                return;
            if( on )
                _content.FadeIn();
            else
                _content.FadeOut();
        }
    }
}