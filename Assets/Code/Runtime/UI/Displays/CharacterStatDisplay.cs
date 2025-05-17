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
        [SerializeField] private CharacterStatId statId;
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
            statValue.text = GetTotalString();
        
            _stat.Value.OnTotalChanged += _ => statValue.text = GetTotalString();
        }
        
        private string GetTotalString()
        {
            var text = _stat.Value.ToString();
            
            if( _stat.Value.isModified )
                text = text.Colored( Color.green );
            return text;
        }
    }
}