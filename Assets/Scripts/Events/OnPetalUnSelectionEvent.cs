using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "New OnPetalUnSelectionEvent", menuName = "ScriptableObjects/Events/OnPetalUnSelectionEvent")]
    public class OnPetalUnSelectionEvent : ScriptableObject
    {
        private List<OnPetalUnSelectionListener> listeners = new List<OnPetalUnSelectionListener>();
        
        public void Raise(int cost) {
            for (int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(cost);
            }
        }

        public void RegisterListener(OnPetalUnSelectionListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPetalUnSelectionListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}