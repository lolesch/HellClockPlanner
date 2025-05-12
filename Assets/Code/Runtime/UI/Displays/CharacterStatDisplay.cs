using System;
using Code.Data;
using Code.Utility.Extensions;
using Code.Utility.Tools.Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class CharacterStatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TextMeshProUGUI statName;
        [SerializeField] private TextMeshProUGUI statValue;
        [SerializeField] private Image hoverImage;
        [SerializeField] private Color highlightedColor;

        public void OnPointerEnter( PointerEventData eventData ) => hoverImage.color = highlightedColor;
        public void OnPointerExit( PointerEventData eventData ) => hoverImage.color = Color.clear;
        
        public void RefreshDisplay( CharacterStat cs )
        {
            statName.text = cs.Stat.ToDescription();
            statValue.text = GetBaseText( cs );
        }
        
        private string GetBaseText( CharacterStat cs )
        {
            return cs.ModType switch
            {
                ModifierType.FlatAdd => $"{cs.TotalValue:0.###}",       //  0
                ModifierType.PercentAdd => $"{cs.TotalValue:0.###} %",  //  0 %

                var _ => $"?? {cs.TotalValue:+ 0.###;- 0.###;0.###}",
            };
        }
    }
}