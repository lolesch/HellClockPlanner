using System.Collections.Generic;
using System.Linq;
using ZLinq;
using UnityEngine;
using ZLinq.Linq;

namespace Code.Runtime.Pools
{
    internal sealed class Pool<T> where T : MonoBehaviour, IPoolObject
    {
        private readonly T _prefab;
        private readonly Transform _parent;
        private readonly List<T> _pool = new();

        public ValueEnumerable<ListWhere<T>, T> InUse => _pool.AsValueEnumerable().Where( x => x.gameObject.activeSelf );
        public ValueEnumerable<Except<FromList<T>, ListWhere<T>, T>, T> Available => _pool.AsValueEnumerable().Except( InUse );

        public Pool( T prefab, Transform parent = null, uint initialAmount = 1 )
        {
            if( !prefab )
            {
                Debug.LogWarning( "The pool object is invalid!" );
                return;
            }

            prefab.gameObject.SetActive( false );

            _prefab = prefab;
            _parent = parent != null
                ? parent
                : _prefab.transform.parent;

            _ = ExtendPool( initialAmount );
        }

        public T GetObject( bool activated = true )
        {
            var prefab = Available.FirstOrDefault() ?? ExtendPool();

            prefab.Initialize();
            prefab.gameObject.SetActive( activated );

            return prefab;
        }

        private T ExtendPool( uint amount = 0 )
        {
            if( amount == 0 )
                amount = (uint) _pool.Count; // doubles the poos current size

            var newPrefabs = new T[amount];

            for( var i = 0; i < newPrefabs.Length; i++ )
            {
                newPrefabs[i] = Object.Instantiate( _prefab, _parent );
                ReleaseObject( newPrefabs[i] );
            }

            _pool.AddRange( newPrefabs );

            return newPrefabs[0];
        }

        public void ReleaseObject( T prefab )
        {
            prefab.Dispose();
            prefab.gameObject.SetActive( false );
        }

        // Constructor with autoCull ticker?
        public void Cull()
        {
            foreach( var clutter in Available )
            {
                _pool.Remove( clutter );
#if UNITY_EDITOR
                Object.DestroyImmediate( clutter.gameObject );
#else
                Object.Destroy( clutter.gameObject );
#endif
            }
        }

        public void ReleaseAll()
        {
            foreach( var poolObject in InUse )
                ReleaseObject(poolObject);
        }
    }
}