using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Data.ScriptableObjects;
using Code.Utility.Extensions;
using TMPro;
using UnityEngine;

namespace Code.Runtime.Provider
{
    public sealed class DataProvider : AbstractProvider<DataProvider>
    {
        [field: SerializeField] private DataContainer database;
        [field: SerializeField] private SkillIcons skillIcons;
        [field: SerializeField] private ProficiencyIcons proficiencyIcons;
        [field: SerializeField] private SkillProficiency[] proficiencies;
        [field: SerializeField] private TMP_Dropdown.OptionData defaultOption;

        //public List<SkillIcon> skillIconList => skillIcons.icons;
        //public List<ProficiencyIcon> proficiencyIconList => proficiencyIcons.icons;
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
                if( data.common != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Common ) );
                if( data.magic != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Magic ) );
                if( data.rare != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Rare ) );
                if( data.epic != 0 )
                    newList.Add( CreateProficiencyForRarity( data, RarityId.Epic ) );
            }
            
            proficiencies = newList.OrderBy( x => x.id ).ThenBy( x=> x.proficiencyId ).ToArray();
        }
        
        public List<SkillImportData> GetSkills() => database.tables.skills;
        public List<CharacterStatImportData> GetBaseStats() => database.tables.characterStats;
        public List<ProficiencyImportData> GetProficiencies() => database.tables.proficiencies;
        public List<GlobalBuffImportData> GetGlobalBuffs() => database.tables.globalBuffs;
        
        private SkillProficiency CreateProficiencyForRarity( ProficiencyImportData data, RarityId rarity )
        {
            return new SkillProficiency
            {
                id = data.id,
                proficiencyId = data.proficiency,
                value = data.GetValue( rarity ),
                rarity = rarity,
                name = data.title.Colored( Const.GetRarityColor( rarity ) ),
                icon = GetIconFromProficiencyId( data.proficiency ),
                modType = data.modType,
            };
        }

        private IEnumerable<SkillProficiency> GetSkillProficiencies( SkillId id )
            => proficiencies.Where( x => x.id == id );

        public IEnumerable<SkillProficiency> GetSkillProficiencies( SkillId id, RarityId rarity ) 
            => GetSkillProficiencies( id ).Where( x => x.rarity == rarity )
                .OrderBy( x => x.proficiencyId);
        
        public SkillProficiency GetSkillProficiency( ProficiencyId id ) 
            => proficiencies.First( x => x.proficiencyId == id );

        public TMP_Dropdown.OptionData GetDefaultDropdownOption() => defaultOption; 
    }
}