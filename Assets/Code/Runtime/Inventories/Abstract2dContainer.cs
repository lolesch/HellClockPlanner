using System;
using System.Collections.Generic;
using ZLinq;
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
        private Dictionary<Vector2Int, Vector2Int> _gridPointer = new();
        private List<Vector2Int> _availablePositions = new();

        public virtual bool TryAddToContainer( Abstract2dItem item )
        {
            var position = new Vector2Int();

            for( var x = 0; x < Dimensions.x; x++ )
            {
                position.x = x;
                for( var y = 0; y < Dimensions.y; y++ )
                {
                    position.y = y;
                    if ( TryAdd( position, item ) )
                        return true;
                }
            }

            return false;
        }

        private bool TrySwap( Vector2Int position, Abstract2dItem item, out Abstract2dItem previousItem )
        {
            previousItem = null;
            
            if( !IsEmpty( position, item, out var itemPositions) || 1 < itemPositions.Count )
                //Debug.LogError( $"Failed to swap at {position} - {itemPositions.Count} items overlapping!" );
                return false;

            if( storedItems.Remove( itemPositions[0], out previousItem ) )
            {
                var pointers = GetRequiredPointers( position, previousItem );
                foreach( var pointer in pointers )
                    _gridPointer.Remove( pointer );
    
                previousItem.Unequip();
            }
            
            if( TryAdd( position, item ) )
                return true;
            
            // send previousItem back (to drag provider if needed)
            
            // restore the previous item
            if( !TryAdd( itemPositions[0], previousItem ) ) 
                Debug.LogError( $"Item loss! Failed to restore {previousItem} at {itemPositions[0]} after swap failure!" );
            // send item back (to drag provider if needed)

            return false;
        }

        private bool TryAdd( Vector2Int position, Abstract2dItem item )
        { 
            if( item == null )
                return false;
            
            // move this inside the drag provider -> if rarity is epic, gray out invalid positions
            //// TODO: check for uniqueness - if epic relic is present, block adding or swap with existing
            //if( item.rarityId == RarityId.Epic )
            //{
            //    var equivalent = storedItems.FirstOrDefault( x => x.Value.relicId == item.relicId ); 
            //    if ( equivalent.Value != null && equivalent.Key == position)
            //        return TrySwap( position, item, out _ );        
            //}

            if( !IsEmpty( position, item, out _ ) )
                return false;
        
            storedItems[position] = item;
            
            var pointers = GetRequiredPointers( position, item );
            foreach( var p in pointers )
                _gridPointer[p] = position;
            
            item.Equip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }

        /*public bool TryRemoveFromContainer( Abstract2dItem item )
        {
            FindAllEqualItems(item, out var positions);

            for (var i = positions.Count; i --> 0;)
                return TryRemove( positions[i] );

            return false;

            void FindAllEqualItems( Abstract2dItem compareTo, out List<Vector2Int> positions )
            {
                positions = new List<Vector2Int>();

                foreach (var storedItem in storedItems)
                    if (storedItem.Value == compareTo)
                        positions.Add(storedItem.Key);
            }
        }*/
        
        private bool TryRemove( Vector2Int position )
        {
            if( !TryGetItemAt( ref position ) ) 
                return false;
                
            if( !storedItems.Remove( position, out var item ) ) 
                return false;
            
            var pointers = GetRequiredPointers( position, item );
            foreach( var pointer in pointers )
                _gridPointer.Remove( pointer );
            
            item.Unequip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }
        
        protected List<Vector2Int> GetRequiredPointers( Vector2Int position, Abstract2dItem item )
        {
            var pointers = new List<Vector2Int>();
            var maxExclusive = position + item.relicSizeId.ToDimension();;

            for (var x = position.x; x < maxExclusive.x; x++)
            for (var y = position.y; y < maxExclusive.y; y++)
                pointers.Add( new Vector2Int(x, y) );

            return pointers;
        }

        public List<Vector2Int> GetOverlappingItems( Vector2Int position, Abstract2dItem item )
        {
            var itemPositions = new List<Vector2Int>();
            
            var pointers = GetRequiredPointers( position, item );

            foreach (var p in pointers)
                if( _gridPointer.TryGetValue(p, out var itemPosition ) )
                    itemPositions.Add( itemPosition );

            return itemPositions.AsValueEnumerable().Distinct().ToList();
        }

        public bool TryGetItemAt( ref Vector2Int position/*, out Abstract2dItem storedItem*/ )
        {
            //storedItem = null;
            return _gridPointer.TryGetValue( position, out position );
            //&& storedItems.TryGetValue( position, out storedItem );
        }

        public bool IsEmpty( Vector2Int position, Abstract2dItem item, out List<Vector2Int> itemPositions )
        {
            itemPositions = null;
            
            if( !IsAvailable( position, item ) )
                return false;
            
            itemPositions = GetOverlappingItems( position, item );
            
            return itemPositions.Count == 0;
        }
        
        private bool IsAvailable( Vector2Int position, Abstract2dItem item ) => 
            GetRequiredPointers( position, item ).AsValueEnumerable().All( x => _availablePositions.Contains( x ) );
        
        // TODO: move the IsWithinDimensions check to the _availablePositions setter
        private bool IsWithinDimensions( Vector2Int position ) => 
            0 <= position.x && position.x < Dimensions.x && 
            0 <= position.y && position.y < Dimensions.y;

        public void Sort()
        {
            var sortedItems = storedItems
                .AsValueEnumerable()
                .OrderByDescending(x => x.Value.relicSizeId)
                .ThenByDescending(x => x.Value.rarityId)
                //.ThenBy(x => x.ToString())
                .ToList();

            foreach (var entry in sortedItems)
                _ = TryRemove( entry.Key );

            foreach (var entry in sortedItems)
                _ = TryAddToContainer( entry.Value );
        }
    }
}
