using System;
using System.Collections.Generic;
using ZLinq;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "SkillStatIcons", menuName = Const.DataCollections + "SkillStatIcons" )]
    public sealed class SkillStatIcons : ScriptableObject
    {
        [field: SerializeField] public List<SkillStatIcon> icons { get; private set; }

        public Sprite GetIconFromSkillStatId( SkillStatId skillStatId ) =>
            icons.Find( x => x.skillStatId == skillStatId ).icon;

        [ContextMenu( "UpdateList" )]
        private void UpdateList()
        {
            icons ??= new List<SkillStatIcon>();
            var ids = (SkillStatId[])Enum.GetValues( typeof( SkillStatId ) );
            var missing = ids.AsValueEnumerable().Where( x => icons.AsValueEnumerable().All( y => y.skillStatId != x ) ).ToArray();
            foreach( var id in missing )
                icons.Add( new SkillStatIcon( id ) );
            
            icons = icons.AsValueEnumerable().OrderBy( x => x.skillStatId ).ToList();
        }
    }

    [Serializable]
    public struct SkillStatIcon : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public SkillStatId skillStatId { get; private set; }
        [field: SerializeField] public Sprite icon { get; private set; }

        public void OnBeforeSerialize() => name = skillStatId.ToDescription();
        public void OnAfterDeserialize() => name = skillStatId.ToDescription();

        public SkillStatIcon( SkillStatId skillStatId )
        {
            name = skillStatId.ToDescription();
            this.skillStatId = skillStatId;
            icon = null;
        }
    }
}