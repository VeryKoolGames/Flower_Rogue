using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "New OnEnemyDeathEvent", menuName = "ScriptableObjects/Events/OnEnemyDeathEvent")]
    public class OnEnemyDeathEvent : ScriptableObject
    {
        private List<OnEnemyDeathListener> listeners = new List<OnEnemyDeathListener>();
        
        public void Raise(Entity enemy) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(enemy);
            }
        }

        public void RegisterListener(OnEnemyDeathListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnEnemyDeathListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}