using System;
using System.Collections.Generic;
using NorskaLib.Spreadsheets;
using UnityEngine;

namespace Code.Data.ScriptableObjects
{
    [CreateAssetMenu( fileName = "DataContainer", menuName = Const.DataCollections + "SpreadsheetImport" )]
    public sealed class DataContainer : SpreadsheetsContainerBase
    {
        [field: SpreadsheetContent, SerializeField] public SpreadsheetContent content { get; private set; }
    }

    [Serializable]
    public sealed class SpreadsheetContent
    {
        [SpreadsheetPage( Const.DatabaseSkills )] 
        public List<SkillImportData> skills;
        
        [SpreadsheetPage( Const.DatabaseCharacterStats )] 
        public List<CharacterStatImportData> characterStats;
    }
}