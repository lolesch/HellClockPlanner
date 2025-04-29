using System;
using System.Collections.Generic;
using NorskaLib.Spreadsheets;
using UnityEngine;

namespace Code.Data
{
    [CreateAssetMenu( fileName = "DataContainer", menuName = "ScriptableObjects/DataContainer" )]
    public sealed class DataContainer : SpreadsheetsContainerBase
    {
        [SpreadsheetContent, SerializeField] private SpreadsheetContent content;
        public SpreadsheetContent Content => content;
    }

    [Serializable]
    public sealed class SpreadsheetContent
    {
        [SpreadsheetPage( Constants.DatabaseSkills )] 
        public List<SkillData> skills;
    }
}