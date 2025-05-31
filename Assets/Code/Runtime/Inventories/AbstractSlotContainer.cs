using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Inventories
{
    public abstract class AbstractSlotContainer<T> where T : Enum
    {
        public Dictionary<T, AbstractItem<T>> storedItems { get; protected set; } = new();
        public event Action<Dictionary<T, AbstractItem<T>>> OnContentChanged;
        
        private static T GetSlot( AbstractItem<T> item )
        {
            if( item == null )
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            
            foreach( var slotType in (T[]) Enum.GetValues( typeof(T) ) )
                if( slotType.Equals( item.SlotType ) ) 
                    return slotType;
            
            throw new Exception( $"Type {item.SlotType} not found" );
        }
        
        public bool TryAddToContainer( AbstractItem<T> item, out AbstractItem<T> previousItem )
        {
            previousItem = null;
            
            if( item == null )
                return false;
            
            var slot = GetSlot( item );
            
            return TrySwap( slot, item, out previousItem );
        }

        private bool TrySwap( T slot, AbstractItem<T> item, out AbstractItem<T> previousItem )
        {
            previousItem = null;
            
            if( storedItems.Remove( slot, out previousItem ) )
                previousItem.Unequip();

            if( TryAdd( item, slot ) ) 
                return true;
            
            //if( previousItem != null)
            // send previousItem to drag provider if needed

            // If we failed to add the new item, we need to restore the previous item
            if( !TryAdd( previousItem, slot ) ) 
                Debug.LogError( $"Item loss! Failed to restore {previousItem} in {slot} after swap failure!" );
            
            return false;
        }

        private bool TryAdd( AbstractItem<T> item, T slot )
        {
            if( item == null )
                return false;
            
            if( !storedItems.TryAdd( slot, item ) ) 
                return false;
            
            item.Equip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }
        
        public bool TryRemoveFromContainer( AbstractItem<T> item )
        {
            if( item == null )
                return false;
            
            var slot = GetSlot( item );

            return TryRemove( slot, out _ );
        }
        
        private bool TryRemove( T slot, out AbstractItem<T> item )
        {
            if( !storedItems.Remove( slot, out item ) ) 
                return false;
            
            item.Unequip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }
    }
}