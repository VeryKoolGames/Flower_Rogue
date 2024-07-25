using System.Collections.Generic;
using Command;
using DefaultNamespace.Events;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(fileName = "New OnPetalDeathEvent", menuName = "ScriptableObjects/Events/OnPetalDeathEvent")]
    public class OnPetalDeathEvent : ScriptableObject
    {
        private List<OnPetalDeathListener> listeners = new List<OnPetalDeathListener>();
        
        public void Raise(GameObject petal) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(petal);
            }
        }

        public void RegisterListener(OnPetalDeathListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPetalDeathListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}