using Code.Data.Enums;
using Code.Runtime.Pools;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillTagDisplay : PoolObject
    {
        [SerializeField] private TextMeshProUGUI tagText;

        public void SetTag( SkillTagId tagId) => tagText.text = tagId.ToDescription();

        public override void Dispose()
        {
            base.Dispose();
            tagText.text = string.Empty;
        }
    }
}