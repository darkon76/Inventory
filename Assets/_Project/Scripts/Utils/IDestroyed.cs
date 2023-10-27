using System;

namespace _Project.Scripts.Utils
{
    public interface IDestroyed
    {
        event Action Destroyed;
        void Destroy();
    }
}