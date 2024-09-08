using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnCombatWinEvent", menuName = "ScriptableObjects/Events/OnCombatWinEvent")]
    public class OnCombatWinEvent : ScriptableObject
    {
        private List<OnCombatWinListener> listeners = new List<OnCombatWinListener>();
        
        public void Raise() {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(OnCombatWinListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnCombatWinListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}