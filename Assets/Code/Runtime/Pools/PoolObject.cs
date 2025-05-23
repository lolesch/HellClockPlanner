using UnityEngine;

namespace Code.Runtime.Pools
{
    public abstract class PoolObject : MonoBehaviour, IPoolObject
    {
        public virtual void Dispose() => gameObject.SetActive( false );

        public virtual void Initialize()
        {
        }
    }
}