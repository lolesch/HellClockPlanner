using System;
using ZLinq;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Data.Imports.Skills
{
    [Serializable]
    public class StatData
    {
        public StatId id;
        public LocalizedStringData[] localizedName;
        [PreviewIcon] public Sprite icon;
        public StatValueType valueType;
        public float baseValue;
        public bool willClamp;
        public float minimumValue;
        public float maximumValue;

        public StatData( StatDefinition definition )
        {
            id = (StatId)definition.id;
            localizedName = definition.localizedName;
            icon = Resources.Load<Sprite>( Const.GetIconPath( $"{definition.icon}" ) );
            valueType = definition.eStatFormat.ToEnum<StatValueType>();
            baseValue = definition.baseValue;
            
            willClamp = definition.willClamp;
            minimumValue = definition.minimumValue; 
            maximumValue = definition.maximumValue;
        }
        
        public string GetLocaName(  ) => localizedName?.AsValueEnumerable()
            .FirstOrDefault( x => x.langCode == Const.CurrentLocale.ToDescription() ).langTranslation;
    }
    
    [Serializable]
    public struct StatDefinitions
    {
        public StatDefinition[] Stats;
    }

    [Serializable]
    public struct StatDefinition
    {
        //public string name;
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