using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Statistics;
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

            statName.text = _stat.Config.GetLocaName();
            SetValueText();

            _stat.Value.OnTotalChanged += _ => SetValueText();
        }
        
        private void SetValueText()
        {
            var text = _stat.ToString();
            if( statValue.text == text )
                return;
            statValue.text = text;
            statValue.DoPunch();
        }
        
        private void SetValueDetailText()
        {
            var total = _stat.ToString();
            if( statValue.text == total )
                return;
            statValue.text = total;
            statValue.DoPunch();
        }
    }
}