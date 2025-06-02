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
        [SerializeField] private DataContainer database;
        [SerializeField] private SkillIcons skillIcons;
        [SerializeField] private TagIcons tagIcons;
        [SerializeField] private SkillStatIcons skillStatIcons;
        [SerializeField] private Proficiency[] proficiencies;
        public TMP_Dropdown.OptionData defaultOption;

        //public List<SkillIcon> skillIconList => skillIcons.icons;
        //public List<ProficiencyIcon> proficiencyIconList => proficiencyIcons.icons;
        public Sprite GetIconFromSkillId( SkillId skillId ) => skillIcons.GetIconFromSkillId( skillId );
        public Sprite GetIconFromTagId( SkillTagId tag ) => tagIcons.GetIconFromTagId( tag );
        public Sprite GetIconFromTagId( DamageTypeId tag ) => tagIcons.GetIconFromTagId( tag );

        public Sprite GetIconFromSkillStatId( SkillStatId skillStatId ) =>
            skillStatIcons.GetIconFromSkillStatId( skillStatId );

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
            var newList = new List<Proficiency>();
            
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
            
            proficiencies = newList.OrderBy( x => x.skillId ).ThenBy( x=> x.skillStatId ).ToArray();
        }
        
        public List<CharacterStatImportData> GetBaseStatImports() => database.tables.characterStats;
        public List<SkillImportData> GetSkillImports() => database.tables.skills;
        public List<SkillTagImportData> GetSkillTagImports() => database.tables.skillTags;
        public List<GlobalBuffImportData> GetGlobalBuffImports() => database.tables.globalBuffs;
        public List<ProficiencyImportData> GetProficiencyImports() => database.tables.proficiencies;
        public List<ShrineImportData> GetShrineImports() => database.tables.shrines;
        
        public List<SkillTagId> GetSkillTagsForSkill( SkillId skillId ) => GetSkillTagImports().Where( x => x.skillId == skillId ).Select( x => x.skillTagId ).ToList();
        
        //public List<SkillImportData> GetUnassignedSkills()
        //{
        //    var skillIds = GameState.Player.SkillSlots.Select( x => x._skillHashId );
        //    return GetSkillImports().Where( x => !skillIds.Contains( x.skillId ) ).ToList();
        //}

        //public List<SkillImportData> GetAssignedSkills()
        //{
        //    var skillIds = GameState.Player.SkillSlots.Select( x => x._skillHashId );
        //    return GetSkillImports().Where( x => skillIds.Contains( x.skillId ) ).ToList();
        //}
        
        private Proficiency CreateProficiencyForRarity( ProficiencyImportData data, RarityId rarityId )
        {
            return new Proficiency( 
                data.skillId, 
                data.skillStatId,
                data.modDescription == string.Empty
                    ? data.skillStatId.ToDescription()
                    : data.modDescription, 
                data.GetValue( rarityId ), 
                rarityId,
                data.proficiencyName.Colored( rarityId.GetRarityColor() ),
                GetIconFromSkillStatId( data.skillStatId ), 
                data.modType );
        }

        private IEnumerable<Proficiency> GetSkillProficiencies( SkillId id )
            => proficiencies.Where( x => x.skillId == id );

        public IEnumerable<Proficiency> GetSkillProficiencies( SkillId id, RarityId rarityId ) 
            => GetSkillProficiencies( id ).Where( x => x.rarityId == rarityId )
                .OrderBy( x => x.skillStatId);
        
        public Proficiency GetSkillProficiency( SkillStatId id ) 
            => proficiencies.First( x => x.skillStatId == id );
    }
}