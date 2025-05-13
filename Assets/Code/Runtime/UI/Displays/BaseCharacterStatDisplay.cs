using Code.Data;
using Code.Runtime.Statistics;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public abstract class BaseCharacterStatDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected StatType stat;
        protected CharacterStat addedStat;
        protected CharacterStat percentStat;
        
        [SerializeField] private TextMeshProUGUI statName;
        [SerializeField] private TextMeshProUGUI statValue;
        [SerializeField] private Image hoverImage;
        [SerializeField] private Color highlightedColor;
        
        [SerializeField] protected Color statBaseColor;
        [SerializeField] protected Color statModifiedColor;

        public void OnPointerEnter( PointerEventData eventData )
        {
            hoverImage.color = highlightedColor;
            //percentStat.AddModifier( new Modifier( .3f ) );
            statValue.text = GetDetailedString();
        }

        public void OnPointerExit( PointerEventData eventData )
        {
            hoverImage.color = Color.clear;
            //addedStat.AddModifier( 30 );
            statValue.text = GetTotalString();
        }

        protected virtual void Start()
        {
            statName.text = stat.ToDescription();
            statValue.text = GetTotalString();
        }

        protected abstract string GetTotalString();
        protected abstract string GetDetailedString();
    }
}