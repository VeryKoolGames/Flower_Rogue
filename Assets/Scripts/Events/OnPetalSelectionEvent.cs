using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "New OnPetalSelectionEvent", menuName = "ScriptableObjects/Events/OnPetalSelectionEvent")]
    public class OnPetalSelectionEvent : ScriptableObject
    {
        private List<OnPetalSelectionListener> listeners = new List<OnPetalSelectionListener>();
        
        public bool Raise(int cost) {
            for (int i = listeners.Count - 1; i >= 0; i--) {
                bool success = listeners[i].OnEventRaised(cost);
                if (!success) {
                    return false;
                }
            }

            return true;
        }

        public void RegisterListener(OnPetalSelectionListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPetalSelectionListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}