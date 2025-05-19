using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data.Enums;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "ProficiencyIcons", menuName = Const.DataCollections + "ProficiencyIcons" )]
    public sealed class ProficiencyIcons : ScriptableObject
    {
        [field: SerializeField] public List<ProficiencyIcon> icons { get; private set; }

        public Sprite GetIconFromProficiencyId( ProficiencyId proficiencyId ) =>
            icons.Find( x => x.proficiencyId == proficiencyId ).icon;

        [ContextMenu( "ResetList" )]
        private void UpdateSkills()
        {
            icons ??= new List<ProficiencyIcon>();
            //icons.Clear();
            var ids = Enum.GetValues( typeof( ProficiencyId ) ) as ProficiencyId[];
            var missing = ids.Where( x => icons.All( y => y.proficiencyId != x ) ).ToArray();
            foreach( var id in missing )
                icons.Add( new ProficiencyIcon( id ) );
            
            icons = icons.OrderBy( x => x.proficiencyId ).ToList();
        }
    }

    [Serializable]
    public struct ProficiencyIcon : ISerializationCallbackReceiver
    {
        [HideInInspector] public string name;
        [field: SerializeField] public ProficiencyId proficiencyId { get; private set; }
        [field: SerializeField] public Sprite icon { get; private set; }

        public void OnBeforeSerialize() => name = proficiencyId.ToDescription();
        public void OnAfterDeserialize() => name = proficiencyId.ToDescription();

        public ProficiencyIcon( ProficiencyId proficiencyId )
        {
            name = proficiencyId.ToDescription();
            this.proficiencyId = proficiencyId;
            icon = null;
        }
    }
}