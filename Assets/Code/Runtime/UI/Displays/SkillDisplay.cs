using System;
using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillDisplay : MonoBehaviour
    {
        [SerializeField] private int slotIndex;
        [SerializeField] private Image icon;
        [SerializeField] private SkillLevelDisplay skillLevelDisplay;
        [SerializeField] private SkillDescriptionDisplay skillDescriptionDisplay;
        [SerializeField] private SkillTagDisplay skillTagPrefab;
        [SerializeField] private TextMeshProUGUI skillName;
        [SerializeField] private TextMeshProUGUI skillRank;
        private Skill _skill;

        private void Start()
        {
            _ = PoolProvider.Instance.InitializePool( skillTagPrefab, true, 4 );
            RefreshSkillDisplay();
        }

        private void OnEnable() => GameState.Player.OnSkillSlotsChanged += OnSkillSlotsChanged;
        private void OnDisable() => GameState.Player.OnSkillSlotsChanged -= OnSkillSlotsChanged;

        private void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            if( _skill != null )
                _skill.OnProficienciesChanged -= RefreshSkillDisplay;
            
            //if( skillSlots[slotIndex]._skillHashId != SkillId.None )
            //_skill = GameState.Player.GetSkill( skillSlots[slotIndex]._skillHashId );
            _skill = GameState.Player.GetSkillAtSlotIndex( slotIndex );

            if( _skill != null )
                _skill.OnProficienciesChanged += RefreshSkillDisplay;
            
            RefreshSkillDisplay();
        }

        private void RefreshSkillDisplay()
        {
            // TODO: skill stats
            // TODO: affected relics
            
            var showDetails = _skill != null && _skill.skillId != SkillId.None;
            skillLevelDisplay.ActivateStatParent( showDetails );
            skillLevelDisplay.gameObject.SetActive( showDetails );
            skillRank.transform.parent.gameObject.SetActive( showDetails );
            skillTagPrefab.transform.parent.gameObject.SetActive( showDetails );
            skillDescriptionDisplay.gameObject.SetActive( showDetails );
            
            if( !showDetails )
            {
                skillName.text = "No Skill Assigned";   
                icon.sprite = DataProvider.Instance.GetIconFromSkillId( SkillId.None );
                
                return;
            }
            
            icon.sprite = _skill.icon;
            skillLevelDisplay.SetSkill( _skill );
            skillDescriptionDisplay.SetSkill( _skill );
            skillName.text = _skill.skillId.ToDescription();
            skillRank.text = "Rank: " + (_skill.rank > 0 ? $"{_skill.rank}".Colored( Color.yellow) : "0" );

            PoolProvider.Instance.ReleaseAll( skillTagPrefab );
            foreach( var tagId in _skill.Tags )
            {
                var display = PoolProvider.Instance.GetObject( skillTagPrefab, false ) as SkillTagDisplay;
                display?.SetTag( tagId );
                display?.gameObject.SetActive(true);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate( transform.parent as RectTransform );
        }
    }
}