using System;
using System.Collections.Generic;
using ZLinq;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "TagIcons", menuName = Const.DataCollections + "TagIcons" )]
    public sealed class TagIcons : ScriptableObject
    {
        [field: SerializeField] public List<TagIcon> tagIcons { get; private set; }
        [field: SerializeField] public List<DamageTagIcon> damageIcons { get; private set; }

        public Sprite GetIconFromTagId( SkillTagId tagId ) => tagIcons.Find( x => x.tagId == tagId ).icon;
        public Sprite GetIconFromTagId( DamageTypeId tagId ) => damageIcons.Find( x => x.tagId == tagId ).icon;

        [ContextMenu( "UpdateList" )]
        private void UpdateList()
        {
            tagIcons ??= new List<TagIcon>();
            var ids = Enum.GetValues( typeof( SkillTagId ) ) as SkillTagId[];
            var missing = ids.AsValueEnumerable().Where( x => tagIcons.AsValueEnumerable().All( y => y.tagId != x ) ).ToArray();
            foreach( var id in missing )
                tagIcons.Add( new TagIcon( id ) );
            
            damageIcons ??= new List<DamageTagIcon>();
            var damageTypeIds = Enum.GetValues( typeof( DamageTypeId ) ) as DamageTypeId[];
            var missingDamageTypeIds = damageTypeIds.AsValueEnumerable().Where( x => damageIcons.AsValueEnumerable().All( y => y.tagId != x ) ).ToArray();
            foreach( var id in missingDamageTypeIds )
                damageIcons.Add( new DamageTagIcon( id ) );
        }
    }

    [Serializable]
    public struct TagIcon : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public SkillTagId tagId { get; private set; }
        [field: SerializeField, /*PreviewIcon(32)*/] public Sprite icon { get; private set; }

        public void OnBeforeSerialize() => name = tagId.ToDescription();
        public void OnAfterDeserialize() => name = tagId.ToDescription();

        public TagIcon( SkillTagId tagId )
        {
            name = tagId.ToDescription();;
            this.tagId = tagId;
            icon = null;
        }
    }

    [Serializable]
    public struct DamageTagIcon : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public DamageTypeId tagId { get; private set; }
        [field: SerializeField, /*PreviewIcon(32)*/] public Sprite icon { get; private set; }

        public void OnBeforeSerialize() => name = tagId.ToDescription();
        public void OnAfterDeserialize() => name = tagId.ToDescription();

        public DamageTagIcon( DamageTypeId tagId )
        {
            name = tagId.ToDescription();;
            this.tagId = tagId;
            icon = null;
        }
    }
}