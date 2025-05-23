using System;

namespace Code.Runtime.Pools
{
    internal interface IPoolObject : IDisposable
    {
        void Initialize();
    }
}