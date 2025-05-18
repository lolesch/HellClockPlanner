using System;
using Code.Data.Enums;
using Code.Runtime.Provider;
using Code.Runtime.Statistics;
using Code.Utility.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Runtime
{
    public abstract class Item
    {
        [SerializeField] protected RarityId Rarity;
        [SerializeField] protected int Tier;
        [field: SerializeField] protected float randomRoll { get; } = Random.value;
        [SerializeField] protected Vector2 _range;
        [SerializeField] protected StatModifier StatMod;

        // TODO: implement distribution
        protected RarityId GetRandomRarity() => (RarityId) Random.Range(0, Enum.GetValues( typeof(RarityId) ).Length);
    }
    
    public sealed class Gear : Item
    {
        public enum GearId : byte
        {
            Jacket,Shoes,Bracers,Pants,RingLeft,RingRight,Keepsake,Revolver,Cap
        }
        
        [SerializeField] private readonly GearId _gearId;
        
        public Gear( GearId gearId, int tier )
        {
            _gearId = gearId;
            Tier = tier;
            Rarity = RarityId.Rare;
            
            //var stat = DataProvider.Instance.GetGearStat( _gearId );
            //_range = DataProvider.Instance.GetTierStatRange( stat, tier );

            
            var mappedRoll = randomRoll.MapFrom01( _range.x, _range.y );
            var mod = new Modifier( mappedRoll, this );
            //StatMod = new StatModifier( stat, mod );
        }
    }
    
    public sealed class Trinket : Item
    {
        public Trinket( int tier )
        {
            Tier = tier;
            Rarity = GetRandomRarity();
            
            //var stat = DataProvider.Instance.GetGearStat( _gearId );
            //_range = DataProvider.Instance.GetTierStatRange( stat, tier );
            
            var mappedRoll = randomRoll.MapFrom01( _range.x, _range.y );
            var mod = new Modifier( mappedRoll, this );
            //StatMod = new StatModifier( stat, mod );
        }
        
    }
}