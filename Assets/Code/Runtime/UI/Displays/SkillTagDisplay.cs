using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Pools;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    [RequireComponent( typeof(GraphicRaycaster), typeof(RectTransform))]
    public sealed class SkillTagDisplay : PoolObject
    {
        [SerializeField] private TextMeshProUGUI tagText;
        [SerializeField] private Image frame;
        [SerializeField] private Color defaultColor;

        public void SetTag( SkillTagId tagId) => tagText.text = tagId.ToDescription();
        public void SetTag( DamageTypeId tagId)
        {
            tagText.text = tagId.ToDescription();
            frame.color = Const.GetDamageTypeColor( tagId ); // add damage type icon instead/on top ?
        }

        public override void Dispose()
        {
            base.Dispose();
            tagText.text = string.Empty;
            frame.color = defaultColor;
        }
    }
}