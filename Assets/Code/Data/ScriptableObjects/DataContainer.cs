using System;
using System.Collections.Generic;
using Code.Data.Imports;
using Code.Data.Imports.Skills;
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
        [SpreadsheetPage( Const.DatabaseSkillTags )] 
        public List<SkillTagImportData> skillTags;
        
        [SpreadsheetPage( Const.DatabaseGlobalBuffs )] 
        public List<GlobalBuffImportData> globalBuffs;
    
        [SpreadsheetPage( Const.DatabaseProficiencies )] 
        public List<ProficiencyImportData> proficiencies;
    
        [SpreadsheetPage( Const.DatabaseShrines )] 
        public List<ShrineImportData> shrines;
    }
}