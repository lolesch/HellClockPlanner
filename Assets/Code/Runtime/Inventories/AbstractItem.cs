using System;
using System.Collections.Generic;
using Code.Data.Enums;
using Code.Runtime.Statistics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Runtime.Inventories
{
    public abstract class AbstractItem<T> : IModifierSource where T : Enum
    {
        public readonly T SlotType;

        protected AbstractItem( T slotType )
        {
            SlotType = slotType;
        }

        [field: SerializeField] public Sprite icon { get; protected set; } = null; // = DataProvider.Instance.GetIconFromItemType( T );
        [field: SerializeField] public RarityId rarityId { get; protected set; } = RarityId.Common;
        [field: SerializeField] public TierId tier { get; protected set; } = TierId.I;
        [field: SerializeField] public List<CharacterStatModifier> affixes { get; protected set; } = new List<CharacterStatModifier>();

        // TODO: implement distribution
        protected RarityId GetRandomRarity() => (RarityId) Random.Range(0, Enum.GetValues( typeof(RarityId) ).Length);
        public Guid guid { get; } = Guid.NewGuid();

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void Unequip()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode() => HashCode.Combine( SlotType, guid );
    }

    public sealed class Gear : AbstractItem<GearType>
    {
        
        public Gear( GearType gearType, TierId tier ) : base( gearType )
        {
            this.tier = tier;
            rarityId = RarityId.Rare;
            
            //var stat = DataProvider.Instance.GetGearStat( _gearId );
            //_range = DataProvider.Instance.GetTierStatRange( stat, tier );

           // var mappedRoll = randomRoll.MapFrom01( _range.x, _range.y );
           // var mod = new Modifier( mappedRoll, this );
            //StatMod = new StatModifier( stat, mod );
            
            affixes = GetRandomAffixes( rarityId );
            
            //Affixes = CombineAffixesOfSameTypeAndMod(Affixes);

            List<CharacterStatModifier> GetRandomAffixes( RarityId rarity )
            {
                var affixAmount = GetAffixAmount(rarity);

                var affixList = new List<CharacterStatModifier>();

                var allowedAffixes = new List<StatRange>();//ItemProvider.Instance.ItemTypeData.GetPossibleStats(equipmentType).ToList();

                /// selects item properties
                for (var i = 0; i < affixAmount; i++)
                {
                    if (allowedAffixes.Count <= 0)
                        break;

                    var randomRoll = UnityEngine.Random.Range(0, allowedAffixes.Count);
                    var randomStat = allowedAffixes[randomRoll];
                    allowedAffixes.RemoveAt(randomRoll); // => exclude double rolls

                    // var lootLevel = LocalPlayer.CharacterLevel; // could modify min/max stat range

                    var rangeRoll = randomStat.GetRandomRoll(rarity); //, statModTypeOverride, lootLevel*/);

                    var itemStat = new CharacterStatModifier(randomStat.StatName, rangeRoll);

                    affixList.Add(itemStat);
                }

                affixList.Sort();

                return affixList;

                uint GetAffixAmount(RarityId rarity) => rarity switch    // TODO: itemType sensitive?
                {
                    RarityId.None => 0,
                    RarityId.Common => 1,     // plus item specific stat
                    RarityId.Magic => 2,
                    RarityId.Rare => 3,
                    RarityId.Epic => 3,     // plus unique stats
                    _ => 0,

                    //ItemRarity.Crafted => 0,
                    //ItemRarity.Uncommon => 0,
                    //ItemRarity.Set => 2,      // plus set stats
                };
            }

            // List<PlayerStatModifier> GetStats(ItemRarity rarity, List<PlayerStatModifier> randomAffixes) => randomAffixes;
        
        }
    }
    
   // public sealed class Trinket : AbstractItem<TrinketType>
   // {
   //     public Trinket( TierId tier )
   //     {
   //         this.tier = tier;
   //         rarityId = GetRandomRarity();
   //         
   //         //var stat = DataProvider.Instance.GetGearStat( _gearId );
   //         //_range = DataProvider.Instance.GetTierStatRange( stat, tier );
   //         
   //         // var mappedRoll = randomRoll.MapFrom01( _range.x, _range.y );
   //         // var mod = new Modifier( mappedRoll, this );
   //         //StatMod = new StatModifier( stat, mod );
   //     }
   // }
}