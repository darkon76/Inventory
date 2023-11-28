using System;
using UnityEngine;

namespace Managers
{
    public class GameMB : MonoBehaviour
    {
        private IGameCoordinator _gameCoordinator = new GameCoordinator();

        private void Awake()
        {
            _gameCoordinator.Awake();
        }

        private void Update()
        {
            _gameCoordinator.Update(Time.deltaTime);
        }
    }
}