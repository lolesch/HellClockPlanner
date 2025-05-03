using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Code.Data;
using Code.Data.Enums;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Runtime.Serialisation
{
    [Serializable]
    public sealed class PlayerSave
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
        public SkillHashId GetSkillIdAtSlotIndex( int slotIndex ) => skillSlots[slotIndex]._skillHashId;
        public void SetSkillIdAtSlotIndex( int slotIndex, SkillHashId skillHashId )
        {
            skillSlots[slotIndex]._skillHashId = skillHashId;
            OnSaveLoaded?.Invoke( this );
        }
        public List<SkillHashId> GetAssignedSkillIds() => skillSlots.Where( x => x._skillHashId != SkillHashId.None ).Select( x => x._skillHashId ).ToList();

        public void ForceInvokeOnSaveLoaded() => OnSaveLoaded?.Invoke( this );
    }
}
