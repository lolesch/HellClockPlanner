using System.IO;
using System.Text;
using UnityEngine;
    
namespace Code.Data
{
    public static class Constants
    {
        private const string FileEnding = "json";

        public static string GetFileName( PlayerSaveId id ) => $"{id}.{FileEnding}";
        public static string GetSaveDirectory()
        {
            var persistentPath = Application.persistentDataPath;
            var subPaths = persistentPath.Split( Path.DirectorySeparatorChar, Path.PathSeparator, Path.AltDirectorySeparatorChar );
            subPaths[^1] = "Hell Clock";
            subPaths[^2] = "Rogue Snail";
            
            //return Path.Combine( subPaths );
            
            //var localLow = subPaths[..^2];
            var sb = new StringBuilder();
            sb.AppendJoin( "/", subPaths );
            //sb.AppendJoin( "/", localLow );
            //sb.Append( "/Rogue Snail/Hell Clock" );
            
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