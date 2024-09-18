using System.Collections.Generic;
using Command;
using Entities;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "New OnEnemySpawnEvent", menuName = "ScriptableObjects/Events/OnEnemySpawnEvent")]
    public class OnEnemySpawnEvent : ScriptableObject
    {
        private List<OnEnemySpawnListener> listeners = new List<OnEnemySpawnListener>();
        
        public void Raise(Entity enemy) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(enemy);
            }
        }

        public void RegisterListener(OnEnemySpawnListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnEnemySpawnListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}