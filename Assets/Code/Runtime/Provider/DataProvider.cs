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
        [SerializeField] private DataContainer database;
        [SerializeField] private SkillIcons skillIcons;
        [SerializeField] private TagIcons tagIcons;
        [SerializeField] private SkillStatIcons skillStatIcons;
        [SerializeField] private Proficiency[] proficiencies;
        public TMP_Dropdown.OptionData defaultOption;
        
        [SerializeField] private SkillDefinitionsImportData skillImportData;
        [SerializeField] private List<SkillData> skillDataTest;
        
        [ContextMenu("loadSkillDataTest")]
        private void LoadSkillDataTest()
        {
            skillDataTest.Clear();
            
            foreach( var id in Enum.GetValues( typeof( SkillHashId ) ) as SkillHashId[] )
            {
                if( id == SkillHashId.None )
                    continue;

                skillDataTest.Add( GetSkillData( id ) );
            }
        }

        [ContextMenu("LoadSlot0")]
        private void LoadSlot0() => GameState.LoadSaveFile( Const.PlayerSaveId.PlayerSave0 );
        [ContextMenu("LoadSlot1")]
        private void LoadSlot1() => GameState.LoadSaveFile( Const.PlayerSaveId.PlayerSave1 );
        [ContextMenu("LoadSlot2")]
        private void LoadSlot2() => GameState.LoadSaveFile( Const.PlayerSaveId.PlayerSave2 );
        
        [ContextMenu("ImportSkills")]
        public void ImportSkills()
        {
            var fileName = "Skills";
            var output = GameState.LoadJson( fileName, Const.GetDataImportDirectory() );
            
            skillImportData = JsonUtility.FromJson<SkillDefinitionsImportData>( output );
            //JsonUtility.FromJsonOverwrite( output, skillImportData );
        }

        //public List<SkillIcon> skillIconList => skillIcons.icons;
        //public List<ProficiencyIcon> proficiencyIconList => proficiencyIcons.icons;
        public Sprite GetIconFromSkillId( SkillHashId skillHashId ) => skillIcons.GetIconFromSkillId( skillHashId );
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
        private SkillImportData GetSkillImport( SkillHashId id ) => 
            database.tables.skills.FirstOrDefault( x => x.skillId == id );
        private SkillDefinitionImportData GetSkillDefinitionImport( SkillHashId id ) => 
            skillImportData.Skills.FirstOrDefault( x => x.id == id );

        public List<SkillImportData> GetSkillImports() => database.tables.skills;

        public SkillData GetSkillData( SkillHashId id ) =>
            new ( GetSkillDefinitionImport( id ), GetSkillImport( id ) );
        public List<SkillTagImportData> GetSkillTagImports() => database.tables.skillTags;
        public List<GlobalBuffImportData> GetGlobalBuffImports() => database.tables.globalBuffs;
        public List<ProficiencyImportData> GetProficiencyImports() => database.tables.proficiencies;
        public List<ShrineImportData> GetShrineImports() => database.tables.shrines;
        
        public List<SkillTagId> GetSkillTagsForSkill( SkillHashId skillHashId ) => GetSkillTagImports().Where( x => x.skillHashId == skillHashId ).Select( x => x.skillTagId ).ToList();
        
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

        private IEnumerable<Proficiency> GetSkillProficiencies( SkillHashId hashId )
            => proficiencies.Where( x => x.skillId == hashId );

        public IEnumerable<Proficiency> GetSkillProficiencies( SkillHashId hashId, RarityId rarityId ) 
            => GetSkillProficiencies( hashId ).Where( x => x.rarityId == rarityId )
                .OrderBy( x => x.skillStatId);
        
        public Proficiency GetSkillProficiency( SkillStatId id ) 
            => proficiencies.First( x => x.skillStatId == id );
    }
}