using System;
using Code.Utility.AttributeRef.Attributes;
using UnityEngine;

namespace Code.Data.Imports
{
    [Serializable]
    public struct PlayerSaveData
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
        [field: SerializeField, ReadOnly] public SkillSlotData[] skillSlots;
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
        //[SerializeField, ReadOnly] public SkillId[] unlockedSkills { get; private set; }
        //[SerializeField, ReadOnly] public SkillId[] availableSkills { get; private set; }
        //[SerializeField] private ConstellationsData constellationsData;
        //[SerializeField] private string[] completedOnboardingKeys;
        //[SerializeField] private int lastBlessedShopId;
        //[SerializeField] private MaxTierShownForGearSlot maxTierShownForGearSlot;
        //[SerializeField] private SkillUpgradeLevel skillUpgradeLevel;
        //[SerializeField] private int saveVersion;
    }
}
