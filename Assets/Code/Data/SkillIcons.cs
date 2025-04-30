using System;
using System.Collections.Generic;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu( fileName = "SkillIcons", menuName = "ScriptableObjects/SkillIcons" )]
    public sealed class SkillIcons : ScriptableObject
    {
        [SerializeField] private List<SkillIcon> _skillIcons;
        
        public Sprite GetIconFromSkillHashId( SkillHashId skillHashId ) => _skillIcons.Find( x => x.skillHashId == skillHashId ).icon;
        
        [ContextMenu("ResetList")]
        private void UpdateSkills()
        {
            _skillIcons??= new List<SkillIcon>();
            _skillIcons.Clear();
            var ids = Enum.GetValues( typeof( SkillHashId ) ) as SkillHashId[];
            foreach( var id in ids )
                _skillIcons.Add( new SkillIcon( id ) );
        }
    }

    [Serializable]
    public sealed class SkillIcon: ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public SkillHashId skillHashId { get; private set; }
        [field: SerializeField] public Sprite icon { get; private set; }
        
        public void OnBeforeSerialize() => name = skillHashId.ToDescription();
        public void OnAfterDeserialize() => name = skillHashId.ToDescription();
        
        public SkillIcon(SkillHashId skillHashId) => this.skillHashId = skillHashId;
    }
}