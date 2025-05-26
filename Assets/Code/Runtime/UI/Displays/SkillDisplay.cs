using Code.Data;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Runtime.UI.Displays
{
    public sealed class SkillDisplay : IndexDependentDisplay
    {
        [SerializeField] private SkillDescriptionDisplay skillDescriptionDisplay;
        [SerializeField] private GameObject body;
        [SerializeField] private SkillTagDisplay skillTagPrefab;
        [SerializeField] private TextMeshProUGUI skillName;
        private Skill _skill;
       
        private void Start()
        {
            _ = PoolProvider.Instance.InitializePool( skillTagPrefab, true, 4 );
            RefreshSkillDisplay();
        }

        protected override void OnSkillSlotsChanged( SkillSlotData[] skillSlots )
        {
            if( _skill != null )
                _skill.OnProficienciesChanged -= RefreshSkillDisplay;
            
            _skill = GameState.Player.skills[ slot.index ];

            if( _skill != null )
                _skill.OnProficienciesChanged += RefreshSkillDisplay;
            
            RefreshSkillDisplay();
        }

        private void RefreshSkillDisplay()
        {
            // TODO: skill stats
            // TODO: affected relics
            
            var showDetails = _skill != null && _skill.skillId != SkillId.None;
            body.SetActive( showDetails );
            //skillLevelDisplay.ActivateStatParent( showDetails );
            //skillLevelDisplay.gameObject.SetActive( showDetails );
            skillTagPrefab.transform.parent.gameObject.SetActive( showDetails );
            
            if( !showDetails )
            {
                skillName.text = "No Skill Assigned";   
                //icon.sprite = DataProvider.Instance.GetIconFromSkillId( SkillId.None );
                
                return;
            }
            
            skillName.text = _skill.skillId.ToDescription();

            PoolProvider.Instance.ReleaseAll( skillTagPrefab );
            foreach( var tagId in _skill.Tags )
            {
                var skillTag = PoolProvider.Instance.GetObject( skillTagPrefab, false ) as SkillTagDisplay;
                skillTag?.SetTag( tagId );
                skillTag?.gameObject.SetActive(true);
            }

            if( _skill.damageType != DamageTypeId.None )
            {
                var damageTypeTag = PoolProvider.Instance.GetObject( skillTagPrefab, false ) as SkillTagDisplay;
                damageTypeTag?.SetTag( _skill.damageType );
                damageTypeTag?.gameObject.SetActive(true);
            }
            
            LayoutRebuilder.ForceRebuildLayoutImmediate( transform.parent as RectTransform );
        }
    }
}