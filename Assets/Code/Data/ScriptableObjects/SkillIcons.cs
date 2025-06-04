using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "SkillIcons", menuName = Const.DataCollections + "SkillIcons" )]
    public sealed class SkillIcons : ScriptableObject
    {
        [field: SerializeField] public List<SkillIcon> icons { get; private set; }

        public Sprite GetIconFromSkillId( SkillHashId skillHashId ) => icons
            .Find( x => x.skillHashId == skillHashId ).icon;

        [ContextMenu( "UpdateList" )]
        private void UpdateList()
        {
            icons ??= new List<SkillIcon>();
            //icons.Clear();
            var ids = Enum.GetValues( typeof( SkillHashId ) ) as SkillHashId[];
            var missing = ids.Where( x => icons.All( y => y.skillHashId != x ) ).ToArray();
            foreach( var id in missing )
                icons.Add( new SkillIcon( id ) );
            
            icons = icons.OrderBy( x => x.skillHashId ).ToList();
        }
    }

    [Serializable]
    public struct SkillIcon : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public SkillHashId skillHashId { get; private set; }
        [field: SerializeField, /*PreviewIcon(32)*/] public Sprite icon { get; private set; }

        public void OnBeforeSerialize() => name = skillHashId.ToDescription();
        public void OnAfterDeserialize() => name = skillHashId.ToDescription();

        public SkillIcon( SkillHashId skillHashId )
        {
            name = skillHashId.ToDescription();;
            this.skillHashId = skillHashId;
            icon = null;
        }
    }
}