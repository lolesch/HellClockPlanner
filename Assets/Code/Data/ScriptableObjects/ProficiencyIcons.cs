using System;
using System.Collections.Generic;
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
            icons.Clear();
            var ids = Enum.GetValues( typeof( ProficiencyId ) ) as ProficiencyId[];
            foreach( var id in ids )
                icons.Add( new ProficiencyIcon( id ) );
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