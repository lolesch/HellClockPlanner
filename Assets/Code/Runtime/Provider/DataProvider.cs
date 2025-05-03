using System;
using System.Collections;
using System.IO;
using System.Linq;
using Code.Data;
using Code.Data.ScriptableObjects;
using Code.Runtime.Serialisation;
using Code.Utility.Extensions;
using UnityEngine;

namespace Code.Runtime.Provider
{
    public sealed class DataProvider : AbstractProvider<DataProvider>
    {
        [field: SerializeField] public DataContainer database { get; private set; }
        [field: SerializeField] public SkillIcons skillIcons { get; private set; }
        [field: SerializeField] public PlayerSave playerSave { get; private set; } = new PlayerSave();

        private void OnValidate()
        {
            database = EnumerableExtensions.GetScriptableObjectsOfType<DataContainer>().First();
            skillIcons = EnumerableExtensions.GetScriptableObjectsOfType<SkillIcons>().First();

            //SetSkillIcons();
        }

        private void Start()
        {
            Debug.Assert(database != null);
            Debug.Assert(skillIcons != null);
            //SetSkillIcons();
        }

        //[ContextMenu("SetSkillIcons")]
        //private void SetSkillIcons()
        //{
        //    foreach( var skillData in database.content.skills )
        //        if( skillData.Icon == null )
        //            skillData.Icon = skillIcons.GetIconFromSkillHashId( skillData.Id );
        //}
        [ContextMenu("LoadSlot0")]
        private void LoadSlot0() => LoadJson( Const.PlayerSaveId.PlayerSave0 );
        [ContextMenu("LoadSlot1")]
        private void LoadSlot1() => LoadJson( Const.PlayerSaveId.PlayerSave1 );
        [ContextMenu("LoadSlot2")]
        private void LoadSlot2() => LoadJson( Const.PlayerSaveId.PlayerSave2 );
        
        public void LoadJson( Const.PlayerSaveId id )
        {
            var directory = Const.GetSaveDirectory();

            if( !Directory.Exists( directory ) )
            {
                Debug.Log( $"{directory} does not exist" );
                return;
            }
            
            var files = Directory.GetFiles(directory, $"*{Const.GetFileName(id)}");
            
            if ( files.Length == 0 )
            {
                Debug.Log( $"File {Const.GetFileName(id)} does not exist" );
                return;
            }
            
            var jsonFile = new TextAsset( File.ReadAllText( files[0] ) );
            
            JsonUtility.FromJsonOverwrite( jsonFile.text, playerSave );
            //playerSave = JsonUtility.FromJson<PlayerSave>( jsonFile.text );
            
            //OnSaveLoaded?.Invoke( playerSave );
            var skillId = playerSave.GetSkillIdAtSlotIndex(0);
            playerSave.SetSkillIdAtSlotIndex(0, skillId);;
        }
    }
}