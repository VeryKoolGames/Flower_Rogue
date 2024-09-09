using System.Collections.Generic;
using Command;
using Events;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnBoostEvent", menuName = "ScriptableObjects/Events/OnBoostEvent")]
    public class OnBoostEvent : ScriptableObject
    {
        private List<OnBoostListener> listeners = new List<OnBoostListener>();
        
        public void Raise() {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(OnBoostListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnBoostListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}