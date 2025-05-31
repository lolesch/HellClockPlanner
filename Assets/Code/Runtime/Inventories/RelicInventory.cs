using System;
using UnityEngine;

namespace Code.Runtime.Inventories
{
    [Serializable]
    public class RelicInventory : Abstract2dContainer
    {
        public RelicInventory(Vector2Int dimensions) : base(dimensions) { }
    }
}
