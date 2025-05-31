using System.IO;
using Code.Data;
using UnityEngine;

namespace Code.Runtime
{
    public static class GameState
    {
        private static PlayerSaveData PlayerSaveData = new(); 
        public static readonly Player Player = new();
        
        [ContextMenu("LoadSlot0")]
        private static void LoadSlot0() => LoadJson( Const.PlayerSaveId.PlayerSave0 );
        [ContextMenu("LoadSlot1")]
        private static void LoadSlot1() => LoadJson( Const.PlayerSaveId.PlayerSave1 );
        [ContextMenu("LoadSlot2")]
        private static void LoadSlot2() => LoadJson( Const.PlayerSaveId.PlayerSave2 );
        
        public static void LoadJson( Const.PlayerSaveId id )
        {
            var directory = Const.GetSaveDirectory();

            if( !Directory.Exists( directory ) )
            {
                Debug.Log( $"{directory} does not exist" );
                return;
            }
            
            var files = Directory.GetFiles(directory, $"*{id.GetFileName()}");
            
            if ( files.Length == 0 )
            {
                Debug.Log( $"File {id.GetFileName()} does not exist" );
                return;
            }
            
            //var bytes = File.ReadAllBytes( files[0] );
            //var serializer = new DataContractJsonSerializer( typeof(PlayerSaveData) );
            //var stream = new MemoryStream(bytes);
            // try catch...
            //PlayerSaveData = (PlayerSaveData)serializer.ReadObject(stream);
            
            var jsonFile = new TextAsset( File.ReadAllText( files[0] ) );
            //JsonUtility.FromJsonOverwrite( jsonFile.text, PlayerSaveData );
            PlayerSaveData = JsonUtility.FromJson<PlayerSaveData>( jsonFile.text );
            
            Player.UpdateData( PlayerSaveData );
        }
    }
}