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
        [SpreadsheetPage( Const.DatabaseSkills )] 
        public List<SkillImportData> skills;
        
        [SpreadsheetPage( Const.DatabaseCharacterStats )] 
        public List<CharacterStatImportData> characterStats;
    
        [SpreadsheetPage( Const.DatabaseProficiencies )] 
        public List<ProficiencyImportData> proficiencies;
    
        [SpreadsheetPage( Const.DatabaseGlobalBuffs )] 
        public List<GlobalBuffImportData> globalBuffs;
    }
}