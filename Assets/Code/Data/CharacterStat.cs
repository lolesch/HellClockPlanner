using System;
using Code.Runtime.Statistics;
using Code.Utility.Tools.Statistics;

namespace Code.Data
{
    [Serializable]
    public struct CharacterStat
    {
        public readonly StatType Stat;
        public readonly MutableFloat TotalValue;
        public readonly ModifierType ModType;
            
        public CharacterStat( CharacterStatImportData data )
        {
            Stat = (StatType) data.Id;
            TotalValue = new MutableFloat( data.BaseValue );
            ModType = data.modType;
        }
    }
}