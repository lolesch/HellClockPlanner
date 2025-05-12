using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Data.ScriptableObjects;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.Provider
{
    public sealed class DataProvider : AbstractProvider<DataProvider>
    {
        [field: SerializeField] private DataContainer database;
        [field: SerializeField] private SkillIcons skillIcons;
        [field: SerializeField] private ProficiencyIcons proficiencyIcons;
        [field: SerializeField] private SkillProficiency[] proficiencies;

        public List<SkillIcon> skillIconList => skillIcons.icons;
        public List<ProficiencyIcon> proficiencyIconList => proficiencyIcons.icons;
        public Sprite GetIconFromSkillId( SkillId skillId ) => skillIcons.GetIconFromSkillId( skillId );

        public Sprite GetIconFromProficiencyId( ProficiencyId skillId ) =>
            proficiencyIcons.GetIconFromProficiencyId( skillId );

        private void OnValidate()
        {
            database = EnumerableExtensions.GetScriptableObjectsOfType<DataContainer>().First();
            skillIcons = EnumerableExtensions.GetScriptableObjectsOfType<SkillIcons>().First();
        }

        private void Start()
        {
            if( !database )
                Debug.LogError( "No database found!" );

            if( !skillIcons )
                Debug.LogError( "No skillIcons found!" );

            CreateAllProficienciesFromImportData();
        }

        [ContextMenu("CreateAllProficienciesFromImportData")]
        private void CreateAllProficienciesFromImportData()
        {
            var newList = new List<SkillProficiency>();
            
            foreach( var data in  database.tables.proficiencies )
            {
                if( data.Common != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Common ) );
                if( data.Magic != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Magic ) );
                if( data.Rare != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Rare ) );
                if( data.Epic != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Epic ) );
            }
            proficiencies = newList.ToArray();
        }
        
        public List<CharacterStatImportData> GetBaseStats() => database.tables.characterStats;
        
        private SkillProficiency CreateProficiencyForRarity( ProficiencyImportData data, RarityId rarity )
        {
            return new SkillProficiency
            {
                id = data.Id,
                proficiency = data.proficiency,
                value = data.GetValue( rarity ),
                rarity = rarity,
                name = data.Name.Colored( Const.GetRarityColor( rarity ) ),
                icon = GetIconFromProficiencyId( data.proficiency ),
            };
        }

        private IEnumerable<SkillProficiency> GetProficiencies( SkillId id )
            => proficiencies.Where( x => x.id == id );

        public IEnumerable<SkillProficiency> GetDropdownProficiencies( SkillId id, RarityId rarity ) 
            => GetProficiencies( id ).Where( x => x.rarity == rarity );
    }
}