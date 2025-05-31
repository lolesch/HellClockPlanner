using System;
using System.Collections.Generic;
using System.Linq;
using Code.Data;
using UnityEngine;

namespace Code.Runtime.Inventories
{
    [Serializable]
    public abstract class Abstract2dContainer
    {
        protected Abstract2dContainer( Vector2Int dimensions ) => Dimensions = dimensions;

        public readonly Vector2Int Dimensions;
        //public int capacity => Dimensions.x * Dimensions.y;
        public event Action<Dictionary<Vector2Int, Abstract2dItem>> OnContentChanged;
        public Dictionary<Vector2Int, Abstract2dItem> storedItems { get; protected set; } = new();

        public virtual bool TryAddToContainer( Abstract2dItem item)
        {
            if ( item == null )
                return false;

            var success = TryAddAtEmpty( item );

            OnContentChanged?.Invoke( storedItems );

            return success;
        }

        protected virtual bool TryAddAtEmpty( Abstract2dItem item)
        {
            if ( item == null )
                return false;

            var dimensions = item.relicSizeId.ToDimension();

            for (var x = 0; x < Dimensions.x; x++)
                for (var y = 0; y < Dimensions.y; y++)
                    if ( IsEmptySpace( new Vector2Int(x, y), dimensions, out _))
                        return TryAddAtPosition( new Vector2Int(x, y), item );

            return false;
        }

        // TODO: review this method
        public bool TryAddAtPosition( Vector2Int position, Abstract2dItem item )
        {
            if ( item == null )
                return false;

            var dimensions = item.relicSizeId.ToDimension();

            if (IsEmptySpace(position, dimensions, out var otherItems))
                TryAdd( position, item);
            else if (1 == otherItems.Count)
                if (storedItems.TryGetValue( otherItems[0], out var storedPackage))
                    return TrySwap( storedPackage, otherItems[0] );

            return false;
        }

        private bool TryAdd( Vector2Int position, Abstract2dItem item )
        { 
            // TODO: check for uniqueness - if epic relic is present, block adding or swap with existing
            
            if( item == null )
                return false;
        
            if( !storedItems.TryAdd( position, item ) ) 
                return false;
        
            item.Equip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }

        // TODO: review this method
        private bool TrySwap(Abstract2dItem storedPackage, Vector2Int storedPosition)
        {
            _ = TryRemoveAtPosition( storedPosition );
            return TryAdd( storedPosition, storedPackage );
        }

        // TODO: review this method
        protected List<Vector2Int> CalculateRequiredPositions( Vector2Int position, Vector2Int dimension )
        {
            var requiredPositions = new List<Vector2Int>();

            for (var x = position.x; x < position.x + dimension.x; x++)
                for (var y = position.y; y < position.y + dimension.y; y++)
                    requiredPositions.Add( new Vector2Int(x, y) );

            return requiredPositions;
        }

        // TODO: review this method
        public List<Vector2Int> GetStoredItemsAt( Vector2Int position, Vector2Int dimension )
        {
            List<Vector2Int> otherPackagePositions = new();
            var requiredPositions = CalculateRequiredPositions(position, dimension);

            foreach (var package in storedItems)
                for (var x = package.Key.x; x < package.Key.x + package.Value.relicSizeId.ToDimension().x; x++)
                for (var y = package.Key.y; y < package.Key.y + package.Value.relicSizeId.ToDimension().y; y++)
                    foreach (var requiredPosition in requiredPositions)
                        if (new Vector2Int(x, y) == requiredPosition)
                            otherPackagePositions.Add(package.Key);

            return otherPackagePositions.Distinct().ToList();
        }

        // TODO: review this method
        public bool TryGetItemAt( ref Vector2Int position, out Abstract2dItem storedItem)
        {
            var positions = GetStoredItemsAt( position, Vector2Int.one );

            if (positions.Any())
                position = positions.First();

            return storedItems.TryGetValue( position, out storedItem );
        }

        public bool TryRemoveFromContainer( Abstract2dItem item )
        {
            FindAllEqualItems(item, out var positions);

            for (var i = positions.Count; i --> 0;)
                return TryRemoveAtPosition( positions[i] );

            return false;

            void FindAllEqualItems( Abstract2dItem compareTo, out List<Vector2Int> positions )
            {
                positions = new List<Vector2Int>();

                foreach (var storedItem in storedItems)
                    if (storedItem.Value == compareTo)
                        positions.Add(storedItem.Key);
            }
        }

        public bool TryRemoveAtPosition( Vector2Int position )
        {
            return TryGetItemAt( ref position, out _ ) && TryRemove( position, out _ );
        }
        
        private bool TryRemove( Vector2Int position, out Abstract2dItem item )
        {
            if( !storedItems.Remove( position, out item ) ) 
                return false;
            
            item.Unequip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }

        public bool IsEmptySpace( Vector2Int position, Vector2Int dimension, out List<Vector2Int> otherRelics )
        {
            otherRelics = new List<Vector2Int>();

            if( !IsValidPosition( position, dimension ) ) 
                return false;
            
            otherRelics = GetStoredItemsAt( position, dimension );
            
            return otherRelics.Count <= 0;

        }
        
        private bool IsValidPosition( Vector2Int position, Vector2Int dimension ) => 
            CalculateRequiredPositions( position, dimension ).All( IsWithinDimensions );
        private bool IsWithinDimensions( Vector2Int position ) => 
            -1 < position.x && position.x < Dimensions.x && 
            -1 < position.y && position.y < Dimensions.y;
        

        public bool TryGetRelicAt( Vector2Int position, out Abstract2dItem relic ) => storedItems.TryGetValue( position, out relic );

        // TODO package should implement IComparable
        public void Sort()
        {
            var sortedValues = storedItems.Values
                .OrderByDescending(x => x.relicSizeId)
                .ThenByDescending(x => x.rarityId)
                .ThenBy(x => x.ToString())
                .ToList();

            foreach (var package in sortedValues)
                _ = TryRemoveFromContainer(package);

            foreach (var package in sortedValues)
            {
                var packageRef = package;
                _ = TryAddToContainer( packageRef);
            }
        }
    }
}
