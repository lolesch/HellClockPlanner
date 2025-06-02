using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Inventories
{
    public abstract class AbstractSlotContainer<T> where T : Enum
    {
        public Dictionary<T, AbstractItem<T>> storedItems { get; protected set; } = new();
        public event Action<Dictionary<T, AbstractItem<T>>> OnContentChanged;
        
        public bool TryAddToContainer( AbstractItem<T> item )
        {
            if( item == null )
                return false;
            
            var slot = GetSlot( item );

            return TryAdd( slot, item );
        }

        private bool TrySwap( T slot, AbstractItem<T> item, out AbstractItem<T> previousItem )
        {
            previousItem = null;
            
            if( storedItems.Remove( slot, out previousItem ) )
                previousItem.Unequip();

            if( TryAdd( slot, item ) ) 
                return true;
            
            //if( previousItem != null)
            // send previousItem to drag provider if needed

            // If we failed to add the new item, we need to restore the previous item
            if( !TryAdd( slot, previousItem ) ) 
                Debug.LogError( $"Item loss! Failed to restore {previousItem} in {slot} after swap failure!" );
            
            return false;
        }

        private bool TryAdd( T slot, AbstractItem<T> item )
        {
            if( item == null )
                return false;
            
            if( !storedItems.TryAdd( slot, item ) ) 
                return false;
            
            item.Equip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }
        
        /*public bool TryRemoveFromContainer( AbstractItem<T> item )
        {
            if( item == null )
                return false;
            
            var slot = GetSlot( item );

            return TryRemove( slot );
        }*/
        
        private bool TryRemove( T slot )
        {
            if( !storedItems.Remove( slot, out var item ) ) 
                return false;
            
            item.Unequip();
            OnContentChanged?.Invoke( storedItems );
            return true;
        }
        
        private static T GetSlot( AbstractItem<T> item )
        {
            if( item == null )
                throw new ArgumentNullException(nameof(item), "Item cannot be null");
            
            foreach( var slotType in (T[]) Enum.GetValues( typeof(T) ) )
                if( slotType.Equals( item.SlotType ) ) 
                    return slotType;
            
            throw new Exception( $"Type {item.SlotType} not found" );
        }


    }
}