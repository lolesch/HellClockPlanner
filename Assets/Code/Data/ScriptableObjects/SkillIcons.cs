using System;
using System.Collections.Generic;
using ZLinq;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "SkillIcons", menuName = Const.DataCollections + "SkillIcons" )]
    public sealed class SkillIcons : ScriptableObject
    {
        [field: SerializeField] public List<SkillIcon> icons { get; private set; }

        public Sprite GetIconFromSkillId( SkillTypeId skillTypeId ) => icons
            .Find( x => x.skillTypeId == skillTypeId ).icon;

        [ContextMenu( "UpdateList" )]
        private void UpdateList()
        {
            icons ??= new List<SkillIcon>();
            //icons.Clear();
            var ids = (SkillTypeId[])Enum.GetValues( typeof( SkillTypeId ) );
            var missing = ids.AsValueEnumerable().Where( x => icons.AsValueEnumerable().All( y => y.skillTypeId != x ) ).ToArray();
            foreach( var id in missing )
                icons.Add( new SkillIcon( id ) );
            
            icons = icons.AsValueEnumerable().OrderBy( x => x.skillTypeId ).ToList();
        }
    }

    [Serializable]
    public struct SkillIcon : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public SkillTypeId skillTypeId { get; private set; }
        [field: SerializeField, /*PreviewIcon(32)*/] public Sprite icon { get; private set; }

        public void OnBeforeSerialize() => name = skillTypeId.ToDescription();
        public void OnAfterDeserialize() => name = skillTypeId.ToDescription();

        public SkillIcon( SkillTypeId skillTypeId )
        {
            name = skillTypeId.ToDescription();;
            this.skillTypeId = skillTypeId;
            icon = null;
        }
    }
}