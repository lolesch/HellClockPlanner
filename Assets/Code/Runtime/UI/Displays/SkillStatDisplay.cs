using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Code.Utility.Extensions;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillStatDisplay : IndexDependentDisplay, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SkillStatId statId;
        private SkillStat _stat;
        
        [SerializeField] private TextMeshProUGUI statName;
        [SerializeField] private TextMeshProUGUI statValue;
        [SerializeField] private Image icon;
        [SerializeField] private Image hoverImage;
        [SerializeField] private Color highlightedColor = new Color( 0.1764706f, 0, 0, 1f );
        
        public void OnPointerEnter( PointerEventData eventData ) => hoverImage.color = highlightedColor;
        public void OnPointerExit( PointerEventData eventData ) => hoverImage.color = Color.clear;

        private void Start( )
        {
            statName.text = statId.ToDescription();
            icon.sprite = DataProvider.Instance.GetIconFromSkillStatId( statId );
            RefreshDisplay();
        }

        private void SetValueText( float value ) => SetValueText(); 
        private void SetValueText()
        {
            var text = _stat != null ? _stat.Value.ToString() : "";
            if( statValue.text == text )
                return;
            
            statValue.text = text;
            statValue.DoPunch();
        }

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots ) => RefreshDisplay();

        private void RefreshDisplay()
        {
            if( _stat != null )
                _stat.Value.OnTotalChanged -= SetValueText;
    
            var skill = GameState.Player.skills[ slot.index ];
            
            _stat = skill?.GetStat( statId );
            if( _stat != null )
                _stat.Value.OnTotalChanged += SetValueText;
            SetValueText();
        }
    }
}