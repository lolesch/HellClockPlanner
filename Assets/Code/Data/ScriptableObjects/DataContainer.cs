using System;
using System.Collections.Generic;
using NorskaLib.Spreadsheets;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "DataContainer", menuName = Const.DataCollections + "SpreadsheetImport" )]
    public sealed class DataContainer : SpreadsheetsContainerBase
    {
        [field: SpreadsheetContent, SerializeField] public SpreadsheetTables tables { get; private set; }
    }

    [Serializable]
    public sealed class SpreadsheetTables
    {
        [SpreadsheetPage( Const.DatabaseCharacterStats )] 
        public List<CharacterStatImportData> characterStats;
        
        [SpreadsheetPage( Const.DatabaseSkills )] 
        public List<SkillImportData> skills;
    
        [SpreadsheetPage( Const.DatabaseSkillTags )] 
        public List<SkillTagImportData> skillTags;
        
        [SpreadsheetPage( Const.DatabaseGlobalBuffs )] 
        public List<GlobalBuffImportData> globalBuffs;
        
        //[SpreadsheetPage( Const.DatabaseStatusEffects )] 
        //public List<StatusEffectImportData> statusEffects;
    
        [SpreadsheetPage( Const.DatabaseProficiencies )] 
        public List<ProficiencyImportData> proficiencies;
    
        [SpreadsheetPage( Const.DatabaseShrines )] 
        public List<ShrineImportData> shrines;
    }
}