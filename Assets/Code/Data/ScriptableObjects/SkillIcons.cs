using System;
using System.Collections.Generic;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "SkillIcons", menuName = Const.DataCollections + "SkillIcons" )]
    public sealed class SkillIcons : ScriptableObject
    {
        [field: SerializeField] public List<SkillIcon> icons { get; private set; }

        public Sprite GetIconFromSkillId( SkillId skillId ) => icons.Find( x => x.skillId == skillId ).icon;

        [ContextMenu( "ResetList" )]
        private void UpdateSkills()
        {
            icons ??= new List<SkillIcon>();
            icons.Clear();
            var ids = Enum.GetValues( typeof( SkillId ) ) as SkillId[];
            foreach( var id in ids )
                icons.Add( new SkillIcon( id ) );
        }
    }

    [Serializable]
    public struct SkillIcon : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public SkillId skillId { get; private set; }
        [field: SerializeField, /*PreviewIcon(32)*/] public Sprite icon { get; private set; }

        public void OnBeforeSerialize() => name = skillId.ToDescription();
        public void OnAfterDeserialize() => name = skillId.ToDescription();

        public SkillIcon( SkillId skillId )
        {
            name = skillId.ToDescription();;
            this.skillId = skillId;
            icon = null;
        }
    }
}