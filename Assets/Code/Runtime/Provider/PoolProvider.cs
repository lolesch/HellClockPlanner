using System.Collections.Generic;
using Code.Runtime.Pools;
using UnityEngine;

namespace Code.Runtime.Provider
{
    internal sealed class PoolProvider : AbstractProvider<PoolProvider>
    {
        private readonly Dictionary<PoolObject, Pool<PoolObject>> _pools = new();

        public Pool<PoolObject> InitializePool( PoolObject type, bool usePrefabParent, uint initialAmount = 1 ) =>
            InitializePool( type, usePrefabParent ? type.transform.parent : null, initialAmount );

        private Pool<PoolObject> InitializePool( PoolObject type, Transform parent = null, uint initialAmount = 1 )
        {
            if( _pools.TryGetValue( type, out var pool ) )
                return pool;

            if( parent == null )
            {
                parent = new GameObject( type.GetType().Name + "Pool" ).transform;
                parent.parent = transform;
            }

            pool = new Pool<PoolObject>( type, parent, initialAmount );
            _pools.Add( type, pool );
            return pool;
        }

        public PoolObject GetObject( PoolObject type, bool activated = true )
        {
            if( !_pools.TryGetValue( type, out var pool ) )
                pool = InitializePool( type );

            return pool.GetObject( activated );
        }
        
        public void ReleaseAll( PoolObject type )
        {
            _pools.TryGetValue( type, out var pool );
            pool?.ReleaseAll();
        }
    }
}