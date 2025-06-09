using System.IO;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Data.Imports;
using UnityEngine;

namespace Code.Runtime
{
    public static class GameState
    {
        private static PlayerSaveData PlayerSaveData = new(); 
        public static readonly Player Player = new();
        
        public static SkillHashIdActAssignment[] ActAvailableSkills = {
            new ( skillTypeId: SkillTypeId.Attack, act: 1 ),
            new ( skillTypeId: SkillTypeId.SplitShot, act: 1 ),
        };
        
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
        
        public static SkillTypeId[] GetAvailableSkills( uint act) => ActAvailableSkills.Where( x => x.act == act ).Select( x => x.SkillTypeId ).ToArray();
    }

    // consider moving the act assignment into the skill import data
    public sealed class SkillHashIdActAssignment
    {
        public SkillTypeId SkillTypeId;
        public uint act;

        public SkillHashIdActAssignment( SkillTypeId skillTypeId, uint act )
        {
            this.SkillTypeId = skillTypeId;
            this.act = act;
        }
    }
}