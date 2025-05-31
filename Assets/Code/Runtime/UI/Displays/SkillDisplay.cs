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
        [SerializeField] private TextMeshProUGUI skillName;
        //[SerializeField] private GameObject[] toggleObjects;
        [SerializeField] private SkillTagDisplay skillTagPrefab;
        [SerializeField] private Image frame;
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
            
            //foreach( var toggleObject in toggleObjects )
            //    toggleObject.SetActive( showDetails );
            
            skillName.text = showDetails ? _skill.skillId.ToDescription() : "No Skill Assigned";
            
            PoolProvider.Instance.ReleaseAll( skillTagPrefab );
            if( showDetails )
                CreateTags();
            
            frame.color = (_skill?.damageType ?? DamageTypeId.None).GetDamageTypeColor() * Color.gray;
            
            LayoutRebuilder.ForceRebuildLayoutImmediate( transform.parent as RectTransform );
        }

        private void CreateTags()
        {
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
        }
    }
}