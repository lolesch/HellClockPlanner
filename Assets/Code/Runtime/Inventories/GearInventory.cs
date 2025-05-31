using System;
using Code.Data.Enums;

namespace Code.Runtime.Inventories
{
    [Serializable]
    public sealed class GearInventory : AbstractSlotContainer<GearType> {}
}
