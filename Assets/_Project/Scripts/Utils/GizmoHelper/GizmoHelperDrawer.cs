using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.GizmoHelper
{
    public sealed class GizmoHelperDrawer : MonoBehaviour
    {
        private readonly List<Action> _actions = new ();

        public void Add(Action action)
        {
            _actions.Add(action);
        }
        void OnDrawGizmos()
        {
            foreach (var action in _actions)
            {
                action?.Invoke();
            }
            _actions.Clear();
        }
    }
}