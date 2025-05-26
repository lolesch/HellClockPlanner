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
    public sealed class SkillStatDisplay : IndexDependentDisplay, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SkillStatId statId;
        private SkillStat _stat;
        //[SerializeField] private SkillId _currentSkillId;
        
        [SerializeField] private TextMeshProUGUI statName;
        [SerializeField] private TextMeshProUGUI statValue;
        [SerializeField] private Image hoverImage;
        [SerializeField] private Color highlightedColor = new Color( 0.1764706f, 0, 0, 1f );
        
        public void OnPointerEnter( PointerEventData eventData ) => hoverImage.color = highlightedColor;
        public void OnPointerExit( PointerEventData eventData ) => hoverImage.color = Color.clear;

        private void Start( ) => RefreshDisplay();

        private void SetValueText( float value ) => SetValueText(); 
        private void SetValueText()
        {
            var text = _stat.Value.ToString();
            
            if( _stat.Value.isModified )
                text = text.Colored( Color.green );
            statValue.text = text;
        }

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots ) => RefreshDisplay();

        private void RefreshDisplay()
        {
            if( _stat != null )
                _stat.Value.OnTotalChanged -= SetValueText;
    
            _stat = GameState.Player.skills[ slot.index ].GetStat( statId );
            _stat.Value.OnTotalChanged += SetValueText;
            
            statName.text = statId.ToDescription();
            SetValueText();
        }
    }
}