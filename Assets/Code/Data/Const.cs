using System;
using System.IO;
using System.Text;
using Code.Data.Enums;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Data
{
    public static class Const
    {
        // ScriptableObject hierarchy
        private const string Root = "SO/";
        public const string DataCollections = Root + "DataCollections/";
        
        private const string FileEnding = "json";
        public const string DatabaseSkills = "Skills";
        public const string DatabaseSkillTags = "SkillTags";
        public const string DatabaseProficiencies = "Proficiencies";
        public const string DatabaseCharacterStats = "CharacterStats";
        public const string DatabaseGlobalBuffs = "GlobalBuffs";
        public const string DatabaseShrines = "Shrines";
        //public const string DatabaseStatusEffects = "StatusEffects";
        
        public const float TooltipDelay = .5f;
        public const float TooltipDelayAfterInteraction = 2f;
        private const float TextPunchScale = .2f;
        private const float TextPunchDuration = .2f;

        public static string GetFileName( PlayerSaveId id ) => $"{id}.{FileEnding}";
        
        public static string GetSaveDirectory()
        {
            var persistentPath = Application.persistentDataPath;
            var subPaths = persistentPath.Split( Path.DirectorySeparatorChar, Path.PathSeparator, Path.AltDirectorySeparatorChar );
            subPaths[^1] = "Hell Clock";
            subPaths[^2] = "Rogue Snail";
            
            var sb = new StringBuilder();
            sb.AppendJoin( "/", subPaths );
            
            return sb.ToString();
        }

        public static Color GetRarityColor( RarityId rarity ) => rarity switch
        {
            RarityId.Common => new Color( 0.3490196f, 0.3254902f, 0.3254902f ),
            RarityId.Magic =>new Color( 0.2901961f, 0.4901961f, 1f ),
            RarityId.Rare => new Color( 0.8901961f, 0.7490196f, 0.3215686f ),
            RarityId.Epic => new Color( 1f, 0.3254902f, 0.1921569f ),
            _ => Color.clear,
        };

        public enum PlayerSaveId
        {
            PlayerSave0,
            PlayerSave1,
            PlayerSave2
        }

        public static Color GetDamageTypeColor( DamageTypeId tagId )=> tagId switch
        {
            DamageTypeId.Physical => new Color( 0.5f, 0.4f, 0.2f ),
            DamageTypeId.Fire => new Color( 0.5f, 0.2f, 0.2f ),
            DamageTypeId.Lightning => new Color( .2f, 0.4f, 0.4f ),
            DamageTypeId.Plague => new Color( 0.2f, 0.5f, 0.2f ),
            _ => Color.clear,
        };

        public static void DoPunch( this Graphic text )
        {
            text.rectTransform.localScale = Vector3.one;
            text.rectTransform.pivot = Vector2.one * .5f;
            text.rectTransform.DOPunchScale( Vector3.one * Const.TextPunchScale, Const.TextPunchDuration, 0, 0 );
                //.OnComplete( () => text.rectTransform.localScale = Vector3.one );
        }
    }
}