using System.IO;
using System.Linq;
using Code.Data;
using UnityEngine;

namespace Code.Runtime
{
    public static class GameState
    {
        private static PlayerSaveData PlayerSaveData = new(); 
        public static readonly Player Player = new();
        
        public static void LoadSaveFile( Const.PlayerSaveId id )
        {
            var jsonString = LoadJson( id.ToString(), Const.GetSaveFileDirectory() );
            
            //JsonUtility.FromJsonOverwrite( jsonString, PlayerSaveData );
            PlayerSaveData = JsonUtility.FromJson<PlayerSaveData>( jsonString );
            
            Player.UpdateData( PlayerSaveData );
        }
        
        public static string LoadJson( string fileName, string directory )
        {
            if( !Directory.Exists( directory ) )
            {
                Debug.Log( $"{directory} does not exist" );
                return null;
            }
            
            var file = Directory.GetFiles(directory, $"*{fileName}{Const.FileTypeJson}").FirstOrDefault();
            
            if ( file == null )
            {
                Debug.Log( $"File {fileName}{Const.FileTypeJson} does not exist" );
                return null;
            }
            
            return new TextAsset( File.ReadAllText( file ) ).text;
        }
    }
}