using System.Collections.Generic;
using Command;
using Events;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnCombatLoseEvent", menuName = "ScriptableObjects/Events/OnCombatLoseEvent")]
    public class OnCombatLoseEvent : ScriptableObject
    {
        private List<OnCombatLoseListener> listeners = new List<OnCombatLoseListener>();
        
        public void Raise() {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(OnCombatLoseListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnCombatLoseListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}