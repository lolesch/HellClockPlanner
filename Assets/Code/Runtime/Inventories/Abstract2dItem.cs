using System;
using UnityEngine;
using Code.Data.Enums;
using Code.Runtime.Statistics;

namespace Code.Runtime.Inventories
{
    [Serializable]
    public abstract class Abstract2dItem : IModifierSource
    {
        [field: SerializeField] public RelicSizeId relicSizeId { get; protected set; } = RelicSizeId.OneByOne;
        public Guid guid { get; } = Guid.NewGuid();
        [field: SerializeField] public RarityId rarityId { get; protected set; } = RarityId.Common;

        public void Unequip()
        {
            throw new NotImplementedException();
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class Relic : Abstract2dItem
    {
        
    }
}