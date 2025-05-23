using System;
using System.Collections.Generic;
using System.Linq;
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

        [ContextMenu( "ResetList" )]
        private void UpdateSkills()
        {
            icons ??= new List<SkillStatIcon>();
            //icons.Clear();
            var ids = Enum.GetValues( typeof( SkillStatId ) ) as SkillStatId[];
            var missing = ids.Where( x => icons.All( y => y.skillStatId != x ) ).ToArray();
            foreach( var id in missing )
                icons.Add( new SkillStatIcon( id ) );
            
            icons = icons.OrderBy( x => x.skillStatId ).ToList();
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