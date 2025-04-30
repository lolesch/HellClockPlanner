using System.IO;
using System.Text;
using UnityEngine;
    
namespace Code.Data
{
    public static class Constants
    {
        private const string FileEnding = "json";
        public const string DatabaseSkills = "Skills";
        public const string DatabaseAffixes = "Affixes";
        
        public const float TooltipDelayAfterInteraction = 2;
        public const float TooltipDelay = 2;

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

        public enum PlayerSaveId
        {
            PlayerSave0,
            PlayerSave1,
            PlayerSave2
        }
    }
}