using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Pools;
using Code.Runtime.Provider;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    [RequireComponent( typeof(GraphicRaycaster), typeof(RectTransform))]
    public sealed class SkillTagDisplay : PoolObject
    {
        [SerializeField] private TooltipHolder tooltipHolder;
        [SerializeField] private Image icon;
        [SerializeField] private Image frame;
        //[SerializeField] private Color frameColor;
        [SerializeField] private Color iconColor;
        //[SerializeField] private TextMeshProUGUI tagText;

        public void SetTag( SkillTagId tagId)
        {
            tooltipHolder.SetTooltipText( tagId.ToDescription() );
            icon.sprite = DataProvider.Instance.GetIconFromTagId( tagId );

            //tagText.text = tagId.ToDescription();
        }

        public void SetTag( DamageTypeId tagId)
        {
            tooltipHolder.SetTooltipText( tagId.ToDescription() );
            icon.sprite = DataProvider.Instance.GetIconFromTagId( tagId );
            icon.color = tagId.GetStatusEffectColor();
            //frame.color = tagId.GetStatusEffectColor() * Color.gray;
            //tagText.text = tagId.ToDescription();
        }

        public override void Dispose()
        {
            base.Dispose();
            icon.sprite = null;
            icon.color = iconColor;
            //frame.color = frameColor;
            //tagText.text = string.Empty;
        }
    }
}