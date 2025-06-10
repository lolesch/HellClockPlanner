using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Statistics;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class CharacterStatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private StatId statId;
        private CharacterStat _stat;
        
        [SerializeField] private TextMeshProUGUI statName;
        [SerializeField] private TextMeshProUGUI statValue;
        [SerializeField] private Image hoverImage;
        [SerializeField] private Color highlightedColor = new Color( 0.1764706f, 0, 0, 1f );
        
        public void OnPointerEnter( PointerEventData eventData ) => hoverImage.color = highlightedColor;
        public void OnPointerExit( PointerEventData eventData ) => hoverImage.color = Color.clear;

        private void Start()
        {
            _stat = GameState.Player.GetStat( statId );
            
            statName.text = statId.ToDescription();
            SetValueText();

            _stat.Value.OnTotalChanged += _ => SetValueText();
        }
        
        private void SetValueText()
        {
            var text = _stat.Value.ToString();
            if( statValue.text == text )
                return;
            statValue.text = text;
            statValue.DoPunch();
        }
    }
}