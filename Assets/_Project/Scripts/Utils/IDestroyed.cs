using System;

namespace Utils
{
    public interface IDestroyed
    {
        event Action Destroyed;
        void Destroy();
    }
}