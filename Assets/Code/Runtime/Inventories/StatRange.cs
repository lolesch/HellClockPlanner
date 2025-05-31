using System;
using Code.Utility.Extensions;
using UnityEngine;
using UnityEngine.Serialization;
using Code.Data.Enums;
using Code.Runtime.Statistics;

namespace Code.Runtime.Inventories
{
    [Serializable]
    public class StatRange : ISerializationCallbackReceiver
    {
        [SerializeField, HideInInspector] public string name;
        [SerializeField] public CharacterStatId StatName;
        [FormerlySerializedAs("MinMax")]
        [SerializeField] private Vector2Int Range;
        [SerializeField] private ModType DefaultModType = ModType.Flat;

        [Tooltip("Modifies the likleyness to roll values within the given range")]
        [SerializeField] public AnimationCurve Distribution;

        public StatRange(CharacterStatId statName, Vector2Int range, ModType defaultModType)
        {
            StatName = statName;
            Range = range;
            DefaultModType = defaultModType;

            name = StatName.ToString();
        }

        public Modifier GetRandomRoll( RarityId rarity)
        {
            // ITEM AFFIX DESIGN BY RARITY:
            // 
            // the lesser the rarity, the higher the max range => Common items can roll the highest stats => good base for crafting
            // the higher the rarity, the higher the min range => Unique items roll with useful affix values

            var modifier = rarity switch
            {
                RarityId.None => 0f,
                RarityId.Common => 1f,
                RarityId.Magic => 1.1f,
                RarityId.Rare => 1.2f,
                RarityId.Epic => 1.3f,

                //ItemRarity.Crafted => 1f,
                //ItemRarity.Uncommon => 1f,
                //ItemRarity.Set => .8f,
                _ => 0f,
            };

            var randomRoll = UnityEngine.Random.Range(0f, 1f);
            var weightedRoll = Distribution.Evaluate(randomRoll);

            /// the higher the rarity, the higher the min range => Unique items roll with usefull affix values
            var min = Mathf.CeilToInt(Range.x * modifier);

            /// the lesser the rarity, the higher the max range => Common items can roll the highest stats => good base for crafting
            var max = Mathf.CeilToInt(Range.y * (1f + (1f - modifier)));

            /// reorder before setting the range
            if (max < min)
                (min, max) = (max, min);

            var mappedValue = weightedRoll.MapFrom01(min - 1, max);
            var value = Mathf.Max(min, Mathf.CeilToInt(mappedValue));

            return new Modifier( /*new Vector2Int(min, max),*/ value, null );
        }

        public void OnBeforeSerialize() => name = $"{StatName}\t{Range}\t{DefaultModType}";

        public void OnAfterDeserialize() { }
    }
}