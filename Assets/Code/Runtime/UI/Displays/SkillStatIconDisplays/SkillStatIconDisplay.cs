using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays.SkillStatIconDisplays
{
    public abstract class SkillStatIconDisplay : IndexDependentDisplay
    {
        protected Skill Skill;
        protected SkillStat Stat;
        protected CharacterStat CharacterStat;
        
        [SerializeField] private SkillStatId statId;
        [SerializeField] private CharacterStatId characterStatId;
        [SerializeField] private Image statIcon;
        [SerializeField] private TextMeshProUGUI statValue;
        [SerializeField] private TooltipHolder tooltipHolder;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            GameState.Player.OnStatsChanged += RefreshDisplay;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            GameState.Player.OnStatsChanged -= RefreshDisplay;
        }
        
        private void Start( )
        {
            statIcon.sprite = DataProvider.Instance.GetIconFromSkillStatId( statId );
            CharacterStat = GameState.Player.GetStat( characterStatId );
            RefreshDisplay();
        }

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            if( Stat != null )
                Stat.Value.OnTotalChanged -= RefreshDisplay;
            Skill = GameState.Player.skills[slot.index];
            if( Skill == null || Skill.skillId == SkillHashId.None )
            {
                statValue.text = "";
                return;
            }
            Stat = Skill?.GetStat( statId );
            if( Stat != null )
                Stat.Value.OnTotalChanged += RefreshDisplay;
            
            RefreshDisplay();
        }

        protected abstract string CalculateValue();
            
        private void RefreshDisplay( float value ) => RefreshDisplay(); 
        private void RefreshDisplay()
        {
            var value = "#";
            if( Skill != null && Stat != null && CharacterStat != null )
                value = CalculateValue();
            
            if( statValue.text == value )
                return;
                
            statValue.text = value;
            statValue.DoPunch();
            tooltipHolder.SetTooltipText( $"{statId.ToDescription()} {value}" );
        }
    }
}