using System.Collections.Generic;
using Command;
using UnityEngine;

namespace DefaultNamespace.Events
{
    [CreateAssetMenu(fileName = "OnPetalSpawnEvent", menuName = "ScriptableObjects/Events/OnPetalSpawnEvent")]
    public class OnPetalSpawnEvent : ScriptableObject
    {
        private List<OnPetalSpawnListener> listeners = new List<OnPetalSpawnListener>();
        
        public void Raise(PetalDrag petal) {
            for(int i = listeners.Count - 1; i >= 0; i--) {
                listeners[i].OnEventRaised(petal);
            }
        }

        public void RegisterListener(OnPetalSpawnListener listener) {
            if (!listeners.Contains(listener))
                listeners.Add(listener);
        }

        public void UnregisterListener(OnPetalSpawnListener listener) {
            if (listeners.Contains(listener))
                listeners.Remove(listener);
        }
    }
}