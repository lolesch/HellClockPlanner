using System.IO;
using System.Text;
using Code.Data.Enums;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Data
{
    public static class Const
    {
        /// ScriptableObject hierarchy
        private const string Root = "SO/";
        public const string DataCollections = Root + "DataCollections/";
        
        public const string FileTypeJson = ".json";
        public const string DatabaseSkills = "Skills";
        public const string DatabaseSkillTags = "SkillTags";
        public const string DatabaseProficiencies = "Proficiencies";
        public const string DatabaseCharacterStats = "CharacterStats";
        public const string DatabaseGlobalBuffs = "GlobalBuffs";
        public const string DatabaseShrines = "Shrines";
        //public const string DatabaseStatusEffects = "StatusEffects";
        
        public const float TooltipDelay = .7f;
        public const float TooltipDelayAfterInteraction = 2f;
        
        private const float TextPunchScale = .25f;
        private const float TextPunchDuration = .25f;
        
        //private const int UnlockedAct = 1;
        public const int MaxSkillLevel = 6;
        
        /// Localization
        public const LocaleId CurrentLocale = LocaleId.En;
        public const string LocaleIdEn = "en";
        public const string LocaleIdPtBr = "pt-br";
        public const string LocaleIdZhCn = "zh-cn";
        
        public static string GetSaveFileDirectory()
        {
            var persistentPath = Application.persistentDataPath;
            var subPaths = persistentPath.Split( Path.DirectorySeparatorChar, Path.PathSeparator, Path.AltDirectorySeparatorChar );
            subPaths[^1] = "Hell Clock";
            subPaths[^2] = "Rogue Snail";
            
            var sb = new StringBuilder();
            sb.AppendJoin( "/", subPaths );
            
            return sb.ToString();
        }
        
        public static string GetIconPath(  string fileName ) => GetResourceFilePath( new []{"DataImport","icons"}, fileName );
        private static string GetResourceFilePath( string[] subFolder, string fileName )
        {
            var sb = new StringBuilder();
            sb.AppendJoin( "/", subFolder );
            if( !string.IsNullOrEmpty( fileName ) )
                sb.Append( $"/{fileName}" );
            //Debug.Log( sb.ToString() );
            return sb.ToString();
        }
        
        public static string GetImportDirectory( string fileName = null)
        {
            var sb = new StringBuilder();
            sb.AppendJoin( "/", Application.dataPath, "Resources", "DataImport", "data" );
            if( !string.IsNullOrEmpty( fileName ) )
                sb.Append( $"/{fileName}" );
            //Debug.Log( sb.ToString() );
            return sb.ToString();
        }
        
        public static Vector2Int ToDimension( this RelicSizeId relicSizeId) => relicSizeId switch
        {
            RelicSizeId.None => Vector2Int.zero,
            RelicSizeId.OneByOne => new Vector2Int(1, 1),
            RelicSizeId.OneByTwo => new Vector2Int(1, 2),
            RelicSizeId.OneByFour => new Vector2Int(1, 4),
            RelicSizeId.TwoByTwo => new Vector2Int(2, 2),
            _ => Vector2Int.zero,
        };

        public enum PlayerSaveId
        {
            PlayerSave0,
            PlayerSave1,
            PlayerSave2
        }

        public static Color GetRarityColor( this RarityId rarityId ) => rarityId switch
        {
            RarityId.Common => new Color( 0.3490196f, 0.3254902f, 0.3254902f ),
            RarityId.Magic =>new Color( 0.2901961f, 0.4901961f, 1f ),
            RarityId.Rare => new Color( 0.8901961f, 0.7490196f, 0.3215686f ),
            RarityId.Epic => new Color( 1f, 0.3254902f, 0.1921569f ),
            _ => Color.clear,
        };

        //public static Color GetDamageTypeColor( this DamageTypeId tagId ) => tagId switch
        //{
        //    DamageTypeId.Physical => new Color( 0.7f, 0.6f, 0.4f ),
        //    DamageTypeId.Fire => new Color( 0.5f, 0.2f, 0.15f ),
        //    DamageTypeId.Lightning => new Color( .2f, 0.3f, 0.4f ),
        //    DamageTypeId.Plague => new Color( 0.2f, 0.5f, 0.3f ),
        //    _ => new Color( 0.33f, 0.33f, 0.33f ),
        //};

        public static Color GetStatusEffectColor( this DamageTypeId tagId ) => tagId switch
        {
            //TODO: use status enum instead
            DamageTypeId.Physical => new Color( 0.7f, 0f, 0.12f ),
            DamageTypeId.Fire => new Color( 0.84f, 0.4f, 0.1f ),
            DamageTypeId.Lightning => new Color( .9f, 0.8f, 0.1f ),
            DamageTypeId.Plague => new Color( 0.33f, 0.62f, 0.13f ),
            _ => new Color( 0.33f, 0.33f, 0.33f ),
        };

        public static void DoPunch( this Graphic target )
        {
            target.rectTransform.pivot = Vector2.one * .5f;
            
            LMotion.Punch.Create( Vector3.one, Vector3.one * TextPunchScale, TextPunchDuration )
                .WithEase( Ease.OutCubic )
                .WithFrequency(1) // Specify oscillation count
                //.WithDampingRatio(0f) // Specify damping ratio
                .BindToLocalScale( target.rectTransform );
        }
    }
}