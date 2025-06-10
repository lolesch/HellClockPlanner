using System;
using System.Linq;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.Imports.Skills
{
    [Serializable]
    public class StatData
    {
        public string name;
        public int id;
        public StatId type;
        public LocalizedStringData[] localizedName;
        [PreviewIcon] public Sprite icon;
        public ModType modType;
        public float baseValue;
        public bool willClamp;
        public Vector2 range;

        public StatData( StatDefinition definition )
        {
            name = definition.name;
            id = definition.id;
            type = (StatId)definition.id;
            localizedName = definition.localizedName;
            icon = Resources.Load<Sprite>( Const.GetIconPath( $"{definition.icon}" ) );
            modType = TryGetModTypeFromString( definition.eStatFormat );
            baseValue = definition.baseValue;
            
            willClamp = definition.willClamp;
            range = new Vector2( definition.minimumValue, definition.maximumValue );
        }
        
        public string GetLocaName(  ) => localizedName?.FirstOrDefault( x => x.langCode == Const.CurrentLocale.ToDescription() ).langTranslation;

        private static ModType TryGetModTypeFromString( string eStatFormat )
        {
            return eStatFormat switch
            {
                "DEFAULT" => ModType.Flat,
                "PERCENTAGE" => ModType.Percent,
                _ => ModType.Flat, // Default to Flat if unknown
            };
        }
    }
    
    [Serializable]
    public struct StatDefinitions
    {
        public StatDefinition[] Stats;
    }

    [Serializable]
    public struct StatDefinition
    {
        public string name;
        public int id;
        //public string type; // always "StatDefinition"
        public LocalizedStringData[] localizedName;
        public string icon;
        public string eStatFormat;
        public float baseValue;
        //public bool IsPlayerStat;
        public bool willClamp;
        public float minimumValue;
        public float maximumValue;
    }
}