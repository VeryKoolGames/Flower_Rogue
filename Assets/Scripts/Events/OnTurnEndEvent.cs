using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnTurnEndEvent", menuName = "ScriptableObjects/Events/OnTurnEndEvent")]
    public class OnTurnEndEvent : ScriptableObject
    {
        private List<OnTurnEndListener> listeners = new List<OnTurnEndListener>();
        
        public void Raise() {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(OnTurnEndListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnTurnEndListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}