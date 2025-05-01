using System;
using System.IO;
using Code.Data;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Runtime.Serialisation
{
    [Serializable]
    public sealed class PlayerSave : MonoBehaviour
    {
        //[SerializeField] private int totalAscensions;
        //[SerializeField] private int cumulativeTotalRuns;
        //[SerializeField] private int totalRuns;
        //[SerializeField] private int cumulativeTotalDeaths;
        //[SerializeField] private DungeonData dungeonData;
        //[SerializeField] private completedDungeon[] completedDungeons;
        //[SerializeField] private bool hardcoreModeEnabled;
        //[SerializeField] private bool relaxedModeEnabled;
        //[SerializeField] private bool allowPause;
        [SerializeField, ReadOnly] private int soulStones;
        [SerializeField, ReadOnly] private int soulStonesOnLastVisit;
        [SerializeField, ReadOnly] private int ascendantShards;
        //[SerializeField] private bool disabled;
        //[SerializeField] private bool initialized;
        //[SerializeField] private int notifications;
        [SerializeField] private SkillSlotData[] skillSlots = new SkillSlotData[5];
        //[SerializeField] private NarrativeEntryData[] narrativeEntriesData;
        //[SerializeField] private flag[] flags;
        //[SerializeField] private PlayerInventorySaveData playerInventorySaveData;
        //[SerializeField] private ExternalInventorySaveData externalInventorySaveData;
        //[SerializeField] private BlessedGear blessedGearData;
        //[SerializeField] private int gameplayTime;
        //[SerializeField] private int bestTime;
        //[SerializeField] private QuestProgressData questProgressData;
        //[SerializeField] private GreatBellSkillTreeData greatBellSkillTreeData;
        //[SerializeField] private PastRunsData pastRunsData;
        [SerializeField, ReadOnly] private SkillHashId[] unlockedSkills;
        [SerializeField, ReadOnly] private SkillHashId[] availableSkills;
        //[SerializeField] private ConstellationsData constellationsData;
        //[SerializeField] private string[] completedOnboardingKeys;
        //[SerializeField] private int lastBlessedShopId;
        //[SerializeField] private MaxTierShownForGearSlot maxTierShownForGearSlot;
        //[SerializeField] private SkillUpgradeLevel skillUpgradeLevel;
        //[SerializeField] private int saveVersion;

        public event Action<PlayerSave> OnSaveLoaded;
        public SkillHashId GetSkillFromSlotIndex( int slotIndex ) => skillSlots[slotIndex]._skillHashId;
        public void SetSkillInSlotIndex( int slotIndex, SkillHashId skillHashId )
        {
            skillSlots[slotIndex]._skillHashId = skillHashId;
            OnSaveLoaded?.Invoke( this );
        }

        private void Start() => OnSaveLoaded?.Invoke( this );
        

        
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
            
            JsonUtility.FromJsonOverwrite( jsonFile.text, this );
            
            OnSaveLoaded?.Invoke( this );
        }
    }
}
